using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotingSystemEntities;
using VotingSystemEntities.Enums;
using VotingSystemEntities.ViewModels;
using VotingSystemRepositoryAccess.Interfaces;
using VotingSystemServices.Interfaces;

namespace VotingSystemServices
{
    public class CountingServices : ICountingServices
    {
        private IElectionRepository _electionServices;
        private IVoteRepository _voteRepository;
        private IPartyServices _partyServices;

        public CountingServices(IElectionRepository electionServices, 
            IVoteRepository voteRepository,
            IPartyServices partyServices)
        {
            _electionServices = electionServices;
            _voteRepository = voteRepository;
            _partyServices = partyServices;
        }

        /// <summary>
        /// Determine winning party, count votes for candidate in area
        /// </summary>
        /// <param name="electionId">Unique Election Id</param>
        /// <returns></returns>
        public ResultsViewModel GetElectionResult(int electionId)
        {
            // get Election to be calculated
            Election election = _electionServices.GetElection(electionId);
            
            // get list of votes cast for election
            ICollection<Vote> allVotes = _voteRepository.GetVotesWithElectionId(electionId);

            // instantialise list of votes that have not been spoilt
            ICollection<Vote> unSpoiltVotes = new List<Vote>();
            int spoiltNumber = 0;
            
            // if vote has been spoilt, add to count
            // else add to list
            foreach (var vote in allVotes)
            {
                if (vote.Candidate != null)
                {
                    unSpoiltVotes.Add(vote);
                }
                else
                {
                    spoiltNumber = spoiltNumber + 1;
                }
            }

            // group votes by Area
            var groupedVotes = unSpoiltVotes.GroupBy(x => x.Candidate.Area);

            // get list of all votes
            ICollection<Area> areasInElection = groupedVotes.Select(x => x.Key).ToList();

            ICollection<AreaResultsViewModel> results = new List<AreaResultsViewModel>();

            // calc election result depending on Election Type
            switch (election.ElectionType)
            {
                case ElectionType.FirstPastThePost:
                    foreach(Area area in areasInElection)
                    {
                        if (area != null)
                        {
                            results.Add(CalcFirstPastThePostResult(groupedVotes.Where(x => x.Key == area).Single().ToList(), area));
                        }
                    }
                    break;
                case ElectionType.Preference:
                    foreach (Area area in areasInElection)
                    {
                        results.Add(CalcPreferenceVoteResult(groupedVotes.Where(x => x.Key == area).Single().ToList()));
                    }
                    break;
                default:
                    throw new Exception("Invalid ElectionType for Election in the GetElectionResult method");
            }

            // empty list of candidates
            var winningCandidateList = new List<CandidateViewModel>();

            foreach (var area in results)
            {
                // candidates must have majority of votes
                if (area.CandidatesInArea.First().Votes != area.CandidatesInArea.ElementAt(1).Votes)
                {
                    // get first candidate in each area, as they have top votes
                    winningCandidateList.Add(area.CandidatesInArea.First());
                }
            }

            // group by party(id) and orderby number of winning candidates 
            var partiesByCandidateCount = winningCandidateList.GroupBy(x => x.PartyId).OrderByDescending(x => x.Count());

            List<PartyViewModel> partyList = new List<PartyViewModel>();

            // iterate over all the parties and count total number of seats each party has 
            foreach (var candidate in winningCandidateList)
            {
                if (partyList.Any(x => x.PartyId == candidate.PartyId))
                {
                    foreach (var item in partyList.Where(w => w.PartyId == candidate.PartyId))
                    {
                        item.Seats = item.Seats + 1;
                    }
                }
                else
                {
                    candidate.Party.Seats = 1;
                    partyList.Add(candidate.Party);
                }
            }

            partyList = partyList.OrderByDescending(x => x.Seats).ToList();

            PartyViewModel party = new PartyViewModel();

            // check if there's more than 1 party
            if (partiesByCandidateCount.Count() > 1)
            {
                // if the top 2 parties have the same number of seats
                if (partiesByCandidateCount.First().Count() == partiesByCandidateCount.ElementAt(1).Count())
                {
                    // if 1st n 2nd have same number of votes, then there's no majority
                    party = new PartyViewModel
                    {
                        PartyName = "No majority!"
                    };
                }
                else
                {
                    // get winning party
                    party = partyList.FirstOrDefault();
                }
            }
            else if (partiesByCandidateCount.Count() == 0)
            {
                // if there's no majority in each area, then there's no goverment
                party = new PartyViewModel
                {
                    PartyName = "No majority!"
                };
            }
            else
            {
                // get winning party
                party = partyList.FirstOrDefault();
            }



            var toReturn = new ResultsViewModel()
            {
                Areas = results,
                SpoiltVoteCount = spoiltNumber,
                LeadingParty = party,
                Parties = partyList
            };

            return toReturn;
        }

        /// <summary>
        /// Return ViewModel containing Area information and list of all candidates with Vote count
        /// </summary>
        /// <param name="votes"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        private AreaResultsViewModel CalcFirstPastThePostResult(ICollection<Vote> votes, Area area)
        {
            // group votes by candidate
            IEnumerable<IGrouping<Candidate, Vote>> candidatesVotes = votes.GroupBy(x => x.Candidate);

            // order candidates by total vote cast for them
            IOrderedEnumerable<IGrouping<Candidate, Vote>> results = candidatesVotes.OrderByDescending(x => x.Count());

            // convert list to CandidateViewModel
            var candidateList = results.Select(x => CandidateViewModel.ToViewModel(x.Key, x.Count())).ToList();

            // return AreaResultsViewModel, order by descending as top will have largest number of votes
            var toReturn = new AreaResultsViewModel
            {
                AreaId = area.Id,
                AreaName = area.AreaName,
                CandidatesInArea = candidateList.OrderByDescending(x => x.Votes).ToList()
            };

            return toReturn;
        }

        private AreaResultsViewModel CalcPreferenceVoteResult(ICollection<Vote> votes)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemEntities.Enums;
using VotingSystemRepositoryAccess.Interfaces;
namespace UnitTests.FakeRepositories
{
    class FakeVoteRepository : IVoteRepository
    {
        public IList<Vote> _fakeRepository = new List<Vote>(GenerateFakeVote());

        public static Vote[] GenerateFakeVote()
        {
            Party fakeParty1 = new Party
            {
                PartyName = "Red Party"
            };

            Party fakeParty2 = new Party
            {
                PartyName = "Blue Party"
            };

            Area fakeArea1 = new Area
            {
                AreaName = "Reddich"         
            };

            fakeArea1.Id = 2;

            Candidate fakeCandidate1 = new Candidate
            {
                CandidateName = "John Smith",
                Party = fakeParty1,
                Area = fakeArea1
            };
            fakeCandidate1.CandidateId = 8;

            Candidate fakeCandidate2 = new Candidate
            {
                CandidateName = "Alan Jones",
                Party = fakeParty2,
                Area = fakeArea1
            };
            fakeCandidate1.CandidateId = 9;

            List<Candidate> candidateList = new List<Candidate>(new Candidate[] { fakeCandidate1, fakeCandidate2 });
            Election newElection = new Election
            {
                ElectionName = "Test Election",
                ElectionType = ElectionType.FirstPastThePost,
                StartDate = new DateTime(1551890188580),
                EndDate = new DateTime(1551890288580),
                Country = "United Kingdom",
                Candidates = candidateList
            };
            newElection.ElectionId = 1;

            Vote newVote = new Vote
            {
               
            };
            newVote.AreaId = 2;
            newVote.CandidateId = 8;
            newVote.ElectionId = 1;
            newVote.VoteId = 123456;
            return new Vote[] { newVote };
        }

        public Vote GenerateNewVote()
        {
            Vote voteToCast = new Vote
            {

            };
            voteToCast.AreaId = 2;
            voteToCast.CandidateId = 9;
            voteToCast.ElectionId = 1;
            voteToCast.VoteId = 654321;
            return voteToCast;
        }

        public async Task<int> AddVote(Vote vote)
        {
            _fakeRepository.Add(vote);
            return vote.VoteId;
        }

        public Vote GetVote(int voteId)
        {
            return _fakeRepository.Where(x => x.VoteId == voteId && !x.Dormant).FirstOrDefault();
        }

        public ICollection<Vote> GetVotesWithElectionId(int electionId)
        {
            return _fakeRepository.Where(x => x.ElectionId == electionId && !x.Dormant).ToList(); ;
        }

        public Task UpdateVote(Vote vote)
        {
            throw new NotImplementedException();
        }

        Task IVoteRepository.DeleteVote(int voteId)
        {
            throw new NotImplementedException();
        }
    }
}

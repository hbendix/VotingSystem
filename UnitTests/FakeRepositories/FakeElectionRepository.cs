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
    public class FakeElectionRepository : IElectionRepository
    {
        public IList<Election> _fakeRepository = new List<Election>(GenerateFakeElection());

        public static Election[] GenerateFakeElection()
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

            Candidate fakeCandidate1 = new Candidate
            { 
                CandidateName = "John Smith",
                Party = fakeParty1, 
                Area = fakeArea1
            };

            Candidate fakeCandidate2 = new Candidate
            {
                CandidateName = "Alan Jones",
                Party = fakeParty2,
                Area = fakeArea1
            };

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


            newElection.ElectionId = 123456;
            return new Election[] { newElection };
        }

        public Election GenerateAdditionalElection()
        {
            Party fakeParty1 = new Party
            {
                PartyName = "Pink Party"
            };

            Party fakeParty2 = new Party
            {
                PartyName = "Orange Party"
            };
            fakeParty2.PartyId = 1;

            Area fakeArea = new Area
            {
                AreaName = "Portsmouth"
            };

            Candidate fakeCandidate1 = new Candidate
            {
                CandidateName = "Jack Smith",
                Party = fakeParty1,
                Area = fakeArea
            };

            Candidate fakeCandidate2 = new Candidate
            {
                CandidateName = "Alton Jones",
                Party = fakeParty2,
                Area = fakeArea
            };
            fakeCandidate2.CandidateId = 1;

            List<Candidate> candidateList = new List<Candidate>(new Candidate[] { fakeCandidate1, fakeCandidate2 });
            Election additionalElection = new Election
            {
                ElectionName = "Updated Election",
                ElectionType = ElectionType.FirstPastThePost,
                StartDate = new DateTime(1551890188580),
                EndDate = new DateTime(1551890288580),
                Country = "United Kingdom",
                Candidates = candidateList
            };
            additionalElection.ElectionId = 654321;
            return additionalElection;
        }

        public Election GenerateUpdatedElection()
        {
            Party fakeParty1 = new Party
            {
                PartyName = "Green Party"
            };

            Party fakeParty2 = new Party
            {
                PartyName = "Yellow Party"
            };

            Area fakeArea = new Area
            {
                AreaName = "Sheffield"
            };

            Candidate fakeCandidate1 = new Candidate
            {
                CandidateName = "Jane Smith",
                Party = fakeParty1,
                Area = fakeArea
            };

            Candidate fakeCandidate2 = new Candidate
            {
                CandidateName = "Alice Jones",
                Party = fakeParty2,
                Area = fakeArea
            };

            List<Candidate> candidateList = new List<Candidate>(new Candidate[] { fakeCandidate1, fakeCandidate2 });
            Election updatedElection = new Election
            {
                ElectionName = "Updated Election",
                ElectionType = ElectionType.FirstPastThePost,
                StartDate = new DateTime(1551890188580),
                EndDate = new DateTime(1551890288580),
                Country = "United Kingdom",
                Candidates = candidateList
            };
            updatedElection.ElectionId = 123456;
            return updatedElection;
        }

        public Election GetElection(int electionId)
        {
            return _fakeRepository.Where(x => x.ElectionId.Equals(electionId)).Single();
        }

        public async Task UpdateElection(Election updatedElection)
        {
            int indexOfElectionToUpdate = _fakeRepository.IndexOf(GetElection(updatedElection.ElectionId));
            _fakeRepository[indexOfElectionToUpdate] = updatedElection;
        }

        public async Task AddElection(Election election)
        {
            _fakeRepository.Add(election);
        }

        public async Task DeleteElection(int electionId)
        {
            Election electionToDelete = GetElection(electionId);
            electionToDelete.Dormant = true;
        }

        public ICollection<Election> GetActiveElections(int userId)
        {
            throw new NotImplementedException();
        }

        public Task AddUserToElection(HasVoted hasVoted)
        {
            throw new NotImplementedException();
        }

        public ICollection<Election> GetAllElections()
        {
            return _fakeRepository.Where(x => !x.Dormant).ToList();
        }
    }
}

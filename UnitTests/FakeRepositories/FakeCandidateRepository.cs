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
    public class FakeCandidateRepository : ICandidateRepository
    {
        public IList<Candidate> _fakeRepository = new List<Candidate>(GenerateFakeCandidate());

        public static Candidate[] GenerateFakeCandidate()
        {
            Party fakeParty = new Party
            {
                PartyName = "Red Party"
            };

            Area fakeArea = new Area
            {
                AreaName = "Reddich"
            };

            Candidate newCandidate = new Candidate
            {
                CandidateName = "John Smith",
                Party = fakeParty,
                Area = fakeArea
            };

            newCandidate.CandidateId = 123456;
            return new Candidate[] { newCandidate };
        }

        public Candidate GetCandidate(int candidateId)
        {
            return _fakeRepository.Where(x => x.CandidateId.Equals(candidateId)).Single();
        }

        public async Task AddCandidate(Candidate candidate)
        {
            _fakeRepository.Add(candidate);
        }

        public async Task UpdateCandidate(Candidate updatedCandidate)
        {
            int indexOfCandidateToUpdate = _fakeRepository.IndexOf(GetCandidate(updatedCandidate.CandidateId));
            _fakeRepository[indexOfCandidateToUpdate] = updatedCandidate;
        }

        public async Task DeleteCandidate(int candidateId)
        {
            Candidate candidateToDelete = GetCandidate(candidateId);
            candidateToDelete.Dormant = true;
        }

        public ICollection<Candidate> GetCandidateList(int electionId, int areaId)
        {
            throw new NotImplementedException();
        }
    }
}

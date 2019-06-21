using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystemEntities;

namespace VotingSystemRepositoryAccess.Interfaces
{
    public interface ICandidateRepository
    {
        Task AddCandidate(Candidate candidate);
        Candidate GetCandidate(int candidateId);
        Task UpdateCandidate(Candidate candidate);
        Task DeleteCandidate(int candidateId);
        ICollection<Candidate> GetCandidateList(int electionId, int areaId);
    }
}

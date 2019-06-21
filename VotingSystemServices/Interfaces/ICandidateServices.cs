using System.Collections.Generic;
using VotingSystemEntities;

namespace VotingSystemServices.Interfaces
{
    public interface ICandidateServices
    {
        ICollection<CandidateViewModel> GetCandidateListForElection(int electionId, int userId);
    }
}

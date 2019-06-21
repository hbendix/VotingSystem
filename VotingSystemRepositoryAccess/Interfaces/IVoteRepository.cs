using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystemEntities;

namespace VotingSystemRepositoryAccess.Interfaces
{
    public interface IVoteRepository
    {
        Task<int> AddVote(Vote vote);
        Vote GetVote(int voteId);
        ICollection<Vote> GetVotesWithElectionId(int electionId);
        Task UpdateVote(Vote vote);
        Task DeleteVote(int voteId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystemEntities.ViewModels;

namespace VotingSystemServices.Interfaces
{
    public interface IVotingServices
    {
        Task<VoteViewModel> CastVote(VoteViewModel vote);
        ICollection<VoteViewModel> GetVotesForElection(int electionId);
    }
}

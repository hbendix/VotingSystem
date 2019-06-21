using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemEntities.ViewModels;

namespace VotingSystemApi.Hub
{
    /// <summary>
    /// Interface used when initialising VoteHub
    /// As it's an interface, you can use DI to inject into the ApiControllers
    /// </summary>
    public interface IVoteHub
    {
        Task BroadcastVote(VoteViewModel vote);
    }
}

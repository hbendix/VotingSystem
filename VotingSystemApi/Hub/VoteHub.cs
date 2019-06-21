using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.Hub
{
    /// <summary>
    /// VoteHub, inherits from Hub which 
    /// takes the interface face as a parameter.
    /// This is to allow DI into the ApiControllers
    /// </summary>
    public class VoteHub : Hub<IVoteHub>
    {
    }
}

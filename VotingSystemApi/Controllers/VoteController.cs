using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using VotingSystemApi.Hub;
using VotingSystemEntities;
using VotingSystemEntities.ViewModels;
using VotingSystemServices.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VotingSystemApi.Controllers
{
    /// <summary>
    /// Endpoint for casting a vote in an election
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private IVotingServices _votingServices;
        private IHubContext<VoteHub, IVoteHub> _hubContext;

        public VoteController(IVotingServices votingServices,
            IHubContext<VoteHub, IVoteHub> hubContext)
        {
            _votingServices = votingServices;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Cast a vote from a user
        /// </summary>
        /// <param name="vote">Vote model</param>
        /// <returns type="statusCodes(200)">200 status code</returns>
        [HttpPost]
        [Route("cast")]
        public async Task<IActionResult> CastVote(VoteViewModel vote)
        {
            try
            {
                VoteViewModel v = await _votingServices.CastVote(vote);

                // once vote has successfully been added to the Database, broadcast the VoteViewModel to all clients
                await _hubContext.Clients.All.BroadcastVote(v);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            };
        }

        /// <summary>
        /// Get's all votes for specific election
        /// </summary>
        /// <param name="electionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/votes")]
        public IActionResult GetAllVotesForElection (int electionId)
        {
            try
            {
                return Ok(_votingServices.GetVotesForElection(electionId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

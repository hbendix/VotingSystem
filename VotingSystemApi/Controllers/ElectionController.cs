using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VotingSystemEntities;
using VotingSystemEntities.Enums;
using VotingSystemEntities.ViewModels;
using VotingSystemServices.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VotingSystemApi.Controllers
{
    /// <summary>
    /// Endpoint for getting list of active elections
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ElectionController : Controller
    {
        private IElectionServices _electionServices;
        private ICountingServices _countingService;

        public ElectionController(IElectionServices electionService,
            ICountingServices countingService)
        {
            _electionServices = electionService;
            _countingService = countingService;
        }

        /// <summary>
        /// Get list of all active elections
        /// </summary>
        /// <returns type="ElectionListViewModel"></returns>
        [Route("get/list")]
        [HttpGet]
        public IActionResult GetElectionList (int userId)
        {
            try
            {
                return Ok(_electionServices.GetElectionList(userId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Get list of all Elections not marked as dormant
        /// </summary>
        /// <returns></returns>
        [Route("Admin/get/list")]
        [HttpGet]
        public IActionResult GetAllElections ()
        {
            try
            {
                return Ok(_electionServices.GetAllElections());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("Admin/results")]
        [HttpGet]
        public IActionResult GetElectionResult (int electionId)
        {
            try
            {
                return Ok(_countingService.GetElectionResult(electionId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Add User and Eleciton to joining table HasVoted, to determine is User has already voted in election
        /// </summary>
        /// <param name="hasVoted"></param>
        /// <returns></returns>
        [Route("post")]
        [HttpPost]
        public async Task<IActionResult> AddUserToHasVoted(HasVotedViewModel hasVoted)
        {
            try
            {
                await _electionServices.AddUserToElection(hasVoted);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Add an election
        /// </summary>
        /// <param name="election"></param>
        /// <returns></returns>
        [Route("Admin/Add")]
        [HttpPost]
        public async Task<IActionResult> AddElection(ElectionViewModel election)
        {
            try
            {
                await _electionServices.AddElection(election);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Update an election
        /// </summary>
        /// <param name="election"></param>
        /// <returns></returns>
        [Route("Admin/Update")]
        [HttpPost]
        public async Task<IActionResult> UpdateElection(ElectionViewModel election)
        {
            try
            {
                await _electionServices.UpdateElection(election);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Delete an election
        /// </summary>
        /// <param name="electionId"></param>
        /// <returns></returns>
        [Route("Admin/Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteElection(int electionId)
        {
            try
            {
                await _electionServices.DeleteElection(electionId);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

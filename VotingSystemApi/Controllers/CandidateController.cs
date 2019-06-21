using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VotingSystemEntities;
using VotingSystemServices.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VotingSystemApi.Controllers
{
    /// <summary>
    /// Endpoint for getting list of candidates for selected election
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : Controller
    {
        private ICandidateServices _candidateServices;

        public CandidateController(ICandidateServices candidateServices)
        {
            _candidateServices = candidateServices;
        }

        /// <summary>
        /// Gets list of candidates for Election and Area
        /// </summary>
        /// <param name="electionId">The Election in question</param>
        /// <param name="userId">User accessing the list</param>
        /// <returns type="CandidateViewModel[]">List of CandidateViewModel for Election and User</returns>
        [Route("get/list/{electionId}/user/{userId}")]
        [HttpGet]
        public IActionResult GetCandidateList (int electionId, int userId)
        {
            try
            {
                return Ok(_candidateServices.GetCandidateListForElection(electionId, userId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

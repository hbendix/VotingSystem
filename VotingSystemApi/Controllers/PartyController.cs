using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemServices.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VotingSystemApi.Controllers
{
    /// <summary>
    /// Party endpoint
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : Controller
    {
        IPartyServices _partyServices;

        public PartyController(IPartyServices partyServices)
        {
            _partyServices = partyServices;
        }

        /// <summary>
        /// Get all parties
        /// </summary>
        /// <returns>List of Parties</returns>
        [Route("Get")]
        [HttpGet]
        public IActionResult GetAllParties()
        {
            try
            {
                return Ok(_partyServices.GetAllParties());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Add a party
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        [Route("Admin/Add")]
        [HttpPost]
        public async Task<IActionResult> AddParty(PartyViewModel party)
        {
            try
            {
                await _partyServices.AddParty(party);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Update a party
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        [Route("Admin/Update")]
        [HttpPost]
        public async Task<IActionResult> UpdateParty(PartyViewModel party)
        {
            try
            {
                await _partyServices.UpdateParty(party);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Delete a party
        /// </summary>
        /// <param name="partyId"></param>
        /// <returns></returns>
        [Route("Admin/Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteParty(int partyId)
        {
            try
            {
                await _partyServices.DeleteParty(partyId);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

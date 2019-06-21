using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VotingSystemApi.Helpers;
using VotingSystemEntities;
using VotingSystemEntities.ViewModels;
using VotingSystemServices.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VotingSystemApi.Controllers
{
    /// <summary>
    /// Admin endpoint
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private IVotingServices _votingServices;
        private AppSettings _appSettings;

        public AdminController(IVotingServices votingServices,
            IOptions<AppSettings> appSettings)
        {
            _votingServices = votingServices;
            _appSettings = appSettings.Value;
        }  
        
        /// <summary>
        /// Get all current votes for Election
        /// </summary>
        /// <param name="electionId">Unique Election ID</param>
        /// <returns>List of Votes</returns>
        [Route("votes")]
        [HttpGet]
        public IActionResult GetVotesForElection (int electionId)
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

        /// <summary>
        /// Cast a 'Paper' Vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns></returns>
        [Route("castpapervote")]
        [HttpPost]
        public IActionResult CastPaperVote(VoteViewModel vote)
        {
            try
            {
                _votingServices.CastVote(vote);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        private static TokenValidationParameters GetValidationParameters(string key)
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // The same key as the one that generate the token
            };
        }
    }
}

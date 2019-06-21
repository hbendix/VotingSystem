using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VotingSystemEntities.ViewModels;
using VotingSystemServices;
using VotingSystemServices.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VotingSystemApi.Controllers
{
    /// <summary>
    /// Endpoint for creating a new user
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private IRegistrationServices _registrationServices;

        public RegistrationController(IRegistrationServices registrationServices)
        {
            _registrationServices = registrationServices;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user">User to be created</param>
        /// <returns type="statusCode(200)"></returns>
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegisterUser(UserViewModel user)
        {
            try
            {
                await _registrationServices.CreateUserAccount(user);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

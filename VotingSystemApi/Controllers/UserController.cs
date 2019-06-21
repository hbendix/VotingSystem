using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VotingSystemApi.Helpers;
using VotingSystemEntities;
using VotingSystemEntities.ViewModels;
using VotingSystemServices.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VotingSystemApi.Controllers
{
    /// <summary>
    /// Endpoint for logging in
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserServices _userServices;
        private AppSettings _appSettings;

        public UserController(IUserServices userServices,
            IOptions<AppSettings> appSettings)
        {
            _userServices = userServices;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Login user into the system and return JWT token
        /// </summary>
        /// <param name="user">Username and password</param>
        /// <returns type="JWTToken">JWT token to be stored on the client application</returns>
        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel user)
        {
            try
            {
                var loggedInUser = _userServices.AuthenticateUser(user);

                if (loggedInUser == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, loggedInUser.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return Ok(new
                {
                    User = loggedInUser,
                    Token = tokenString
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

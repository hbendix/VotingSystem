using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemEntities.Enums;
using VotingSystemEntities.ViewModels;
using VotingSystemRepositoryAccess.Interfaces;
using VotingSystemServices.Interfaces;

namespace VotingSystemServices
{
    /// <summary>
    /// Business logic for creating an account
    /// </summary>
    public class RegistrationServices : IRegistrationServices
    {
        private IAreaServices _areaServices;
        private IUserRepository _userRepository;

        public RegistrationServices(IAreaServices areaServices, IUserRepository userRepository)
        {
            _areaServices = areaServices;
            _userRepository = userRepository;
        }
        /// <summary>
        /// Creates a new user async
        /// </summary>
        /// <param name="user">User to be created</param>
        /// <returns type="Task.CompletedTask"></returns>
        public async Task CreateUserAccount(UserViewModel user)
        {

            // check that required fields are populated
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException("Password is required", "Password");
            }

            // check that required fields are populated
            if (string.IsNullOrWhiteSpace(user.FullName))
            {
                throw new ArgumentException("Password is required", "FullName");
            }

            // initialise empty byte arrays for storing password hash and salt
            byte[] passwordHash, passwordSalt;

            // generate encrypted password
            GeneratePassword(user.Password, out passwordHash, out passwordSalt);

            // find user's area
            Area newUsersArea = _areaServices.GetAreaFromAddress(user.Address);
            // generate unique username for user
            string uniqueName = GenerateUniqueUsername(user.FullName);

            var newUser = new User
            {
                Username = uniqueName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Area = newUsersArea,
                Email = user.Email.Encrypt(),
                FullName = user.FullName.Encrypt(),
                Address = user.Address.Encrypt(),
                NationalId = user.NationalId.Encrypt(),
                Role = RoleType.User
            };

            // create user
            await _userRepository.AddUser(newUser);
        }

        /// <summary>
        /// Hashes user's password
        /// </summary>
        /// <param name="password">Password to hash</param>
        /// <param name="passwordHash">Hashed password</param>
        /// <param name="passwordSalt">Salt of password</param>
        private static void GeneratePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // validation on password string
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }

            // generate password salt and hash
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        /// <summary>
        /// Generates a unique username of User's fullname
        /// </summary>
        /// <param name="username">User's name</param>
        /// <returns>Unique username for user</returns>
        private string GenerateUniqueUsername(string username)
        {
            // random generate username of supplied username
            // check that username doesn't already exist - or go again
            var _username = "";
            do
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(username));
                    var res = Convert.ToBase64String(hash);

                    _username = res.Substring(0, res.Length - 2);
                }
            } while (_userRepository.GetUserByUsername(_username) != null);

            return _username;
        }
    }
}

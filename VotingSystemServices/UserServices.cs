using System;
using System.Text;
using VotingSystemEntities;
using VotingSystemEntities.ViewModels;
using VotingSystemRepositoryAccess.Interfaces;
using VotingSystemServices.Interfaces;

namespace VotingSystemServices
{
    /// <summary>
    /// Business logic for logging in user
    /// </summary>
    public class UserServices : IUserServices
    {
        private IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUser(userId);
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        /// <summary>
        /// Business logic for logging user in
        /// </summary>
        /// <param name="login">Username and password</param>
        /// <returns type="UserViewModel">Logged in user details</returns>
        public UserViewModel AuthenticateUser(LoginViewModel login)
        {
            // validation on inputs
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return null;
            }

            // get user from DB
            var user = _userRepository.GetUserByUsername(login.Username);

            // if user doesn't exist, throw error
            if (user == null)
            {
                return null;
            }

            // verify password
            if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            // extension method can't be used in entities due to dependecies so have to do decrypt here
            var userVM = UserViewModel.ToViewModel(user);

            userVM.FullName = userVM.FullName.Decrypt();

            return userVM;
        }

        /// <summary>
        /// Verify user password with hash and salt of password
        /// </summary>
        /// <param name="password">User password</param>
        /// <param name="storedHash">User hash from DB</param>
        /// <param name="storedSalt">USer salt from DB</param>
        /// <returns type="bool">True if passes</returns>
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            // validation on password
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }
            if (storedHash.Length != 64)
            {
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            }
            if (storedSalt.Length != 128)
            {
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            }

            // generate HMAC of password salt and verfiy hashed password matches generated hashed password
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

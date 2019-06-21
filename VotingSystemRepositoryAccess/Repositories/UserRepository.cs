using System;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemRepositoryAccess.Interfaces;

namespace VotingSystemRepositoryAccess
{
    /// <summary>
    /// Data access alyer for users from the DB
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private VotingDbContext _context;

        public UserRepository(VotingDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add new user to the system
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        public async Task AddUser(User user)
        {
            _context.Add(user);

            await _context.SaveChangesAsync();
        }

        public User GetUser(int userId)
        {
            return _context.Users.Where(x => x.UserId == userId).FirstOrDefault();
        }

        /// <summary>
        /// Get user by unique username
        /// </summary>
        /// <param name="username">Unique username string</param>
        /// <returns>User entity</returns>
        public User GetUserByUsername(string username)
        {
            return _context.Users
                .Where(x => x.Username == username)
                .FirstOrDefault();
        }

        public async Task UpdateUser(User user)
        {
             _context.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var _toDelete = _context.Query<Admin>()
               .Where(x => x.UserId == userId)
               .FirstOrDefault();

            _toDelete.Dormant = true;
            _context.Update(_toDelete);

            await _context.SaveChangesAsync();
        }
    }
}

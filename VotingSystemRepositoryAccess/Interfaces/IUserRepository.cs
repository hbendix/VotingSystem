using System.Threading.Tasks;
using VotingSystemEntities;

namespace VotingSystemRepositoryAccess.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        User GetUser(int userId);
        User GetUserByUsername(string username);
        Task UpdateUser(User user);
        Task DeleteUser(int userId);
    }
}

using System.Threading.Tasks;
using VotingSystemEntities.ViewModels;

namespace VotingSystemServices.Interfaces
{
    public interface IRegistrationServices
    {
        Task CreateUserAccount(UserViewModel user);
    }
}

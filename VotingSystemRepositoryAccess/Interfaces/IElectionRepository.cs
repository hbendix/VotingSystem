using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemEntities.ViewModels;

namespace VotingSystemRepositoryAccess.Interfaces
{
    public interface IElectionRepository
    {
        Task AddElection(Election election);
        Election GetElection(int electionId);
        Task UpdateElection(Election updatedElection);
        Task DeleteElection(int electionId);
        ICollection<Election> GetActiveElections(int userId);
        Task AddUserToElection(HasVoted hasVoted);
        ICollection<Election> GetAllElections();
    }
}

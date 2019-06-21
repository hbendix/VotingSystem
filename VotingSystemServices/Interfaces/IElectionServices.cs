using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemEntities.ViewModels;

namespace VotingSystemServices.Interfaces
{
    public interface IElectionServices
    {
        ElectionViewModel GetElectionById(int electionId);
        ICollection<ElectionListViewModel> GetElectionList(int userId);
        Task AddUserToElection(HasVotedViewModel hasVoted);
        Task DeleteElection(int electionId);
        Task AddElection(ElectionViewModel election);
        Task UpdateElection(ElectionViewModel election);
        ICollection<ElectionViewModel> GetAllElections();
    }
}

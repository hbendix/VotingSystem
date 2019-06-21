using System.Collections.Generic;
using System.Linq;
using VotingSystemEntities;
using VotingSystemRepositoryAccess.Interfaces;
using VotingSystemServices.Interfaces;
using VotingSystemEntities.Enums;
using VotingSystemEntities.ViewModels;
using System.Threading.Tasks;

namespace VotingSystemServices
{
    public class ElectionServices : IElectionServices
    {
        private IElectionRepository _electionRepository;

        public ElectionServices(IElectionRepository electionRepository)
        {
            _electionRepository = electionRepository;
        }

        public async Task AddElection(ElectionViewModel election)
        {
            await _electionRepository.AddElection(ElectionViewModel.ToDataModel(election));
        }

        public async Task DeleteElection(int electionId)
        {
            await _electionRepository.DeleteElection(electionId);
        }

        /// <summary>
        /// Add User and Election to joining table
        /// </summary>
        /// <param name="hasVoted">Unique UserId and Unique ElectionId</param>
        /// <returns></returns>
        public async Task AddUserToElection(HasVotedViewModel hasVoted)
        {
            await _electionRepository.AddUserToElection(HasVotedViewModel.ToDataModel(hasVoted));
        }

        /// <summary>
        /// Get Election by unique Election ID
        /// </summary>
        /// <param name="electionId">Unique Election ID</param>
        /// <returns type="ElectionViewModel">Election</returns>
        public ElectionViewModel GetElectionById(int electionId)
        {
            Election toReturn = _electionRepository.GetElection(electionId);

            return ElectionViewModel.ToViewModel(toReturn);
        }

        /// <summary>
        /// Get list of all active elections on system
        /// </summary>
        /// <param name="userId">Unique UserId ID</param>
        /// <returns type="ElectionListViewModel[]">List of ElectionListViewModel</returns>
        public ICollection<ElectionListViewModel> GetElectionList(int userId)
        {
            ICollection<Election> _electionList = _electionRepository.GetActiveElections(userId);

            var toReturn = _electionList.Select(x => new ElectionListViewModel
            {
                ElectionId = x.ElectionId,
                ElectionName = x.ElectionName
            }).ToList();

            // iterate through each Election and convert to ElectionListViewModel
            foreach (var election in _electionList)
            {
                foreach (var user in election.UsersVotedIn)
                {
                    if (user.UserId == userId)
                    {
                        if (user.ElectionId == election.ElectionId)
                        {
                            toReturn = toReturn.Where(x => x.ElectionId != election.ElectionId).ToList();
                        }
                    }
                }
            }

            

            return toReturn;
        }

        public async Task UpdateElection(ElectionViewModel election)
        {
            await _electionRepository.UpdateElection(ElectionViewModel.ToDataModel(election));
        }

        /// <summary>
        /// Return a list of ElectionListViewModel with all elections not marked dormant
        /// </summary>
        /// <returns></returns>
        public ICollection<ElectionViewModel> GetAllElections()
        {
            ICollection<Election> elections = _electionRepository.GetAllElections();

            return elections.Select(x => ElectionViewModel.ToViewModel(x)).ToList();
        }
    }
}

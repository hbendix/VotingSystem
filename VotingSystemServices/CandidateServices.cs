using System.Collections.Generic;
using System.Linq;
using VotingSystemEntities;
using VotingSystemRepositoryAccess.Interfaces;
using VotingSystemServices.Interfaces;

namespace VotingSystemServices
{
    /// <summary>
    /// Business logic for getting candidate list 
    /// </summary>
    public class CandidateServices : ICandidateServices
    {
        private ICandidateRepository _candidateRepository;
        private IUserServices _userService;

        public CandidateServices(ICandidateRepository candidateRepository,
            IUserServices userService)
        {
            _candidateRepository = candidateRepository;
            _userService = userService;
        }

        /// <summary>
        /// Get list of all Candiates in Area and for Election
        /// </summary>
        /// <param name="electionId">Unique Election ID</param>
        /// <param name="userId">Unique User ID</param>
        /// <returns type="ICollection<CandidateViewModel>">List of CandidateViewModels</returns>
        public ICollection<CandidateViewModel> GetCandidateListForElection(int electionId, int userId)
        {
            // get user, to then get their AreaId
            User user = _userService.GetUserById(userId);
            // get list of candidates 
            ICollection<Candidate> candidates = _candidateRepository.GetCandidateList(electionId, user.AreaId);

            // convert ICollection<Candidate> to ICollection<CandidateViewModel> and return 
            return candidates.Select(x => CandidateViewModel.ToViewModel(x, null)).ToList();
        }

    }
}

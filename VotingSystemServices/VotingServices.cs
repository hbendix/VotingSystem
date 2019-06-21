using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemEntities.ViewModels;
using VotingSystemRepositoryAccess.Interfaces;
using VotingSystemServices.Interfaces;

namespace VotingSystemServices
{
    /// <summary>
    /// Business logic for casting a vote
    /// </summary>
    public class VotingServices : IVotingServices
    {
        private IVoteRepository _voteRepository;

        public VotingServices(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        /// <summary>
        /// Convert VoteViewModel to Vote and cast vote to the DB
        /// </summary>
        /// <param name="vote">VoteViewModel to be mapped and added</param>
        public async Task<VoteViewModel> CastVote(VoteViewModel vote)
        {
            var voteId = await _voteRepository.AddVote(VoteViewModel.ToDataModel(vote));

            Vote voteVM = _voteRepository.GetVote(voteId);

            return VoteViewModel.ToViewModel(voteVM);
        }

        /// <summary>
        /// Get all votes for election, including the Area and Candidate information
        /// </summary>
        /// <param name="electionId">Unique Election ID</param>
        /// <returns>List of VoteViewModels</returns>
        public ICollection<VoteViewModel> GetVotesForElection(int electionId)
        {
            ICollection<Vote> _votes = _voteRepository.GetVotesWithElectionId(electionId);

            return _votes.Select(x => VoteViewModel.ToViewModel(x)).ToList();
        }
    }

}

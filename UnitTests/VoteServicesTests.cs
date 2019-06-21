using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using UnitTests.FakeRepositories;
using VotingSystemEntities;
using VotingSystemRepositoryAccess.Interfaces;
using VotingSystemServices;
using VotingSystemServices.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using VotingSystemApi.Hub;
using VotingSystemEntities.ViewModels;
using System.Threading.Tasks;
using Moq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class VoteServicesTests
    {
        static FakeVoteRepository fakeVoteRepository = new FakeVoteRepository();
        IVotingServices _voteServices = new VotingServices(fakeVoteRepository);
        //private I
        [TestMethod]
        public void CanGetVotesWithElectionId()
        {
            // Expected Result
            ICollection<Vote> _votes = fakeVoteRepository.GetVotesWithElectionId(1);
            ICollection<VoteViewModel> expected = _votes.Select(x => VoteViewModel.ToViewModel(x)).ToList(); 
            // Actual Result
            ICollection<VoteViewModel> result = _voteServices.GetVotesForElection(1);
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanCastVote()
        {
            //Create vote to cast
            Vote voteToCast = fakeVoteRepository.GenerateNewVote();
            VoteViewModel _voteToCast = VoteViewModel.ToViewModel(voteToCast);
            //Cast vote
            _voteServices.CastVote(_voteToCast);
            ICollection<Vote> _votes = fakeVoteRepository.GetVotesWithElectionId(1);
            ICollection<VoteViewModel> expected = _votes.Select(x => VoteViewModel.ToViewModel(x)).ToList();
            // Actual Result
            ICollection<VoteViewModel> result = _voteServices.GetVotesForElection(1);
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

    }
}

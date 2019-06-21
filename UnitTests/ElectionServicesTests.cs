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
    public class ElectionServicesTests
    {
        static FakeElectionRepository fakeElectionRepository = new FakeElectionRepository();
        IElectionServices _electionServices = new ElectionServices(fakeElectionRepository);
        //private I
        [TestMethod]
        public void CanGetElectionById()
        {
            // Expected Result
            Election expected = fakeElectionRepository._fakeRepository.First();
            // Actual Result
            ElectionViewModel _vm = _electionServices.GetElectionById(123456);
            Election result = ElectionViewModel.ToDataModel(_vm);
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanAddElection()
        {
            //Create Additonal Election
            Election expected = fakeElectionRepository.GenerateAdditionalElection();
            ElectionViewModel _newElection = ElectionViewModel.ToViewModel(expected);
            //Add Election
            _electionServices.AddElection(_newElection);
            // Actual Result
            ElectionViewModel _vm = _electionServices.GetElectionById(654321);
            Election result = ElectionViewModel.ToDataModel(_vm);
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanUpdateElection()
        {
            //Create Updated Election
            Election updatedElection = fakeElectionRepository.GenerateUpdatedElection();
            ElectionViewModel _updatedElection = ElectionViewModel.ToViewModel(updatedElection);
            //Update Election
            _electionServices.UpdateElection(_updatedElection);
            // Expected Result
            Election expected = updatedElection;
            // Actual Result
            ElectionViewModel _vm = _electionServices.GetElectionById(123456);
            Election result = ElectionViewModel.ToDataModel(_vm);
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanGetAllElections()
        {
            // Expected Result
            ICollection<Election> elections = fakeElectionRepository._fakeRepository.Where(x => !x.Dormant).ToList();
            ICollection<ElectionViewModel> expected = elections.Select(x => ElectionViewModel.ToViewModel(x)).ToList();
            // Actual Result
            ICollection<ElectionViewModel> result = _electionServices.GetAllElections();
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanDeleteElection()
        {
            //Delete Election
            _electionServices.DeleteElection(123456);
            // Actual Result
            ElectionViewModel _vm = _electionServices.GetElectionById(123456);
            Election resultElection = ElectionViewModel.ToDataModel(_vm);
            bool result = resultElection.Dormant;
            // Assert
            result.Should().Equals(true);
        }
    }
}

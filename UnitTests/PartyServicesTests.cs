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
    public class PartyServicesTests
    {
        static FakePartyRepository fakePartyRepository = new FakePartyRepository();
        IPartyServices _partyServices = new PartyServices(fakePartyRepository);
        //private I
        [TestMethod]
        public void CanGetPartyById()
        {
            // Expected Result
            Party expected = fakePartyRepository._fakeRepository.First();
            // Actual Result
            PartyViewModel _vm = _partyServices.GetPartyById(123456);
            Party result = PartyViewModel.ToDataModel(_vm);
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanAddParty()
        {
            //Create Additonal Party
            Party expected = fakePartyRepository.GenerateAdditionalParty();
            PartyViewModel _newParty = PartyViewModel.ToViewModel(expected);
            //Add Party
            _partyServices.AddParty(_newParty);
            // Actual Result
            PartyViewModel _vm = _partyServices.GetPartyById(654321);
            Party result = PartyViewModel.ToDataModel(_vm);
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanUpdateParty()
        {
            //Create Updated Party
            Party updatedParty = fakePartyRepository.GenerateUpdatedParty();
            PartyViewModel _updatedParty = PartyViewModel.ToViewModel(updatedParty);
            //Update Party
            _partyServices.UpdateParty(_updatedParty);
            // Expected Result
            Party expected = updatedParty;
            // Actual Result
            PartyViewModel _vm = _partyServices.GetPartyById(123456);
            Party result = PartyViewModel.ToDataModel(_vm);
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanGetAllParties()
        {
            // Expected Result
            ICollection<Party> areas = fakePartyRepository._fakeRepository.Where(x => x.Dormant == false).ToList();
            ICollection<PartyViewModel> expected = areas.Select(x => PartyViewModel.ToViewModel(x)).ToList();
            // Actual Result
            ICollection<PartyViewModel> result = _partyServices.GetAllParties();
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanDeleteParty()
        {
            //Delete Party
            _partyServices.DeleteParty(123456);
            // Actual Result
            PartyViewModel _vm = _partyServices.GetPartyById(123456);
            Party resultParty = PartyViewModel.ToDataModel(_vm);
            bool result = resultParty.Dormant;
            // Assert
            result.Should().Equals(true);
        }
    }
}

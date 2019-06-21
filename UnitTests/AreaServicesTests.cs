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
    public class AreaServicesTests
    {
        static FakeAreaRepository fakeAreaRepository = new FakeAreaRepository();
        IAreaServices _areaServices = new AreaServices(fakeAreaRepository);

        [TestMethod]
        public void CanAddArea()
        {
            //Create Additonal Area
            Area expected = fakeAreaRepository.GenerateAdditionalArea();
            AreaViewModel _newArea = AreaViewModel.ToViewModel(expected);
            //Add Area
            _areaServices.AddArea(_newArea);
            // Actual Result
            Area result = fakeAreaRepository._fakeRepository.Last();
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanUpdateArea()
        {
            //Create Updated Area
            Area updatedArea = fakeAreaRepository.GenerateUpdatedArea();
            AreaViewModel _updatedArea = AreaViewModel.ToViewModel(updatedArea);
            //Update Area
            _areaServices.UpdateArea(_updatedArea);
            // Expected Result
            Area expected = updatedArea;
            // Actual Result
            Area result = fakeAreaRepository._fakeRepository.First();
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanGetAllAreas()
        {
            // Expected Result
            ICollection<Area> areas = fakeAreaRepository._fakeRepository.Where(x => x.Dormant == false).ToList();
            ICollection<AreaViewModel> expected = areas.Select(x => AreaViewModel.ToViewModel(x)).ToList();
            // Actual Result
            ICollection<AreaViewModel> result = _areaServices.GetAllAreas();
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanDeleteArea()
        {
            //Delete Area
            _areaServices.DeleteArea(123456);
            // Actual Result
            Area resultArea = fakeAreaRepository._fakeRepository.First();
            bool result = resultArea.Dormant;
            // Assert
            result.Should().Equals(true);
        }
    }
}

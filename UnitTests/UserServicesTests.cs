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

namespace UnitTests
{
    [TestClass]
    public class UserServicesTests
    {
        static FakeUserRepository fakeUserRepository = new FakeUserRepository();
        IUserServices _userServices = new UserServices(fakeUserRepository);
        //private I
        [TestMethod]
        public void CanGetUserById()
        {
            // Expected Result
            User expected = fakeUserRepository._fakeRepository.First();
            // Actual Result
            User result = _userServices.GetUserById(123456);
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void CanGetUserByUsername()
        {
            // Expected Result
            User expected = fakeUserRepository._fakeRepository.First();
            // Actual Result
            User result = _userServices.GetUserByUsername("myUser");
            // Assert
            expected.Should().BeEquivalentTo(result);
        }

    }
}

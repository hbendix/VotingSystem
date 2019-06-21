using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemEntities.Enums;
using VotingSystemRepositoryAccess.Interfaces;

namespace UnitTests.FakeRepositories
{
    public class FakeUserRepository : IUserRepository
    {
        public IList<User> _fakeRepository = new List<User>(GenerateFakeUser());

        public static User[] GenerateFakeUser()
        {
            Area fakeArea = new Area
            {
                AreaName = "Reddich"
            };
            fakeArea.Id = 654321;

            User newUser = new User
            {
                Email = "test@test.com",
                FullName = "John Smith",
                Address = "1 Test Road, T35T 1NG",
                NationalId = "T3571NG1D",
                AreaId = 654321
            };

            newUser.Username = "myUser";
            newUser.UserId = 123456;
            return new User[] { newUser };
        }

        public User GetUser(int userId)
        {
            return _fakeRepository.Where(x => x.UserId.Equals(userId)).Single();
        }

        public async Task AddUser(User user)
        {
            _fakeRepository.Add(user);
        }

        public async Task UpdateUser(User updatedUser)
        {
            int indexOfUserToUpdate = _fakeRepository.IndexOf(GetUser(updatedUser.UserId));
            _fakeRepository[indexOfUserToUpdate] = updatedUser;
        }

        public async Task DeleteUser(int userId)
        {
            User userToDelete = GetUser(userId);
            userToDelete.Dormant = true;
        }
        public User GetUserByUsername(string username)
        {
            return _fakeRepository.Where(x => x.Username == username).FirstOrDefault();
        }

        
    }
}

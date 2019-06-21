using System;
using System.Collections.Generic;
using System.Text;
using VotingSystemEntities;
using VotingSystemEntities.ViewModels;

namespace VotingSystemServices.Interfaces
{
    public interface IUserServices
    {
        User GetUserByUsername(string username);
        User GetUserById(int userId);
        UserViewModel AuthenticateUser(LoginViewModel user);
    }
}

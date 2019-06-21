using VotingSystemEntities.Enums;

namespace VotingSystemEntities 
{
    public class AdminViewModel
    {
        protected string _UserId;
        public string _Username;
        public string _Password;
        protected RoleType _Role = RoleType.Admin;

        public AdminViewModel(string username, string password)
        {
            _Username = username;
            _Password = password;
        }
    }
}
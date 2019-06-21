namespace VotingSystemEntities.ViewModels
{
    public class UserViewModel
    {
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string NationalId { get; set; }
        public int? AreaId { get; set; }

        public AreaViewModel Area { get; set; }

        public static UserViewModel ToViewModel(User user)
        {
            return new UserViewModel
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                AreaId = user.AreaId
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace VotingSystemEntities.ViewModels
{
    public class HasVotedViewModel
    {
        public int UserId { get; set; }
        public UserViewModel User { get; set; }

        public int ElectionId { get; set; }
        public ElectionViewModel Election { get; set; }

        public static HasVoted ToDataModel (HasVotedViewModel vm)
        {
            return new HasVoted
            {
                ElectionId = vm.ElectionId,
                UserId = vm.UserId
            };
        }
    }
}

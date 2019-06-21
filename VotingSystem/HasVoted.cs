using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VotingSystemEntities
{
    public class HasVoted
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ElectionId { get; set; }
        public virtual Election Election { get; set; }
    }
}

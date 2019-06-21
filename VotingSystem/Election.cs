using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VotingSystemEntities.Enums;

namespace VotingSystemEntities
{
    public class Election
    {
        [Key]
        public int ElectionId { get; set; }
        [Required]
        public string ElectionName { get; set; }
        [Required]
        public string Country { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public ElectionType ElectionType { get; set; }
        public bool Dormant { get; set; }

        public virtual ICollection<HasVoted> UsersVotedIn { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}

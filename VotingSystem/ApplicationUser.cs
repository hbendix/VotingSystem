using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VotingSystemEntities.Enums;

namespace VotingSystemEntities
{
    public class ApplicationUser 
    {
        [Required]
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public RoleType Role { get; set; }

        [Required]
        public bool Dormant { get; set; }

    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotingSystemEntities.Enums;

namespace VotingSystemEntities
{
    public class User : ApplicationUser
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string NationalId { get; set; }

        [ForeignKey("AreaId")]
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }

        public virtual ICollection<HasVoted> ElectionsVotedIn { get; set; }
    }
}

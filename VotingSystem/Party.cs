using System.ComponentModel.DataAnnotations;

namespace VotingSystemEntities
{
    public class Party
    {
        [Key]
        public int PartyId { get; set; }
        [Required]
        public string PartyName { get; set; }
        public bool Dormant { get; set; }
    }
}

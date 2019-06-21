using System.ComponentModel.DataAnnotations;

namespace VotingSystemEntities
{
    public class Area
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AreaName { get; set; }
        public bool Dormant { get; set; }
    }
}

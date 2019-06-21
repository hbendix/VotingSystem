using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSystemEntities
{
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }
        [Required]
        public string CandidateName { get; set; }
        public bool Dormant { get; set; }

        [ForeignKey("PartyId")]
        public int PartyId { get; set; }
        public virtual Party Party { get; set; }

        [ForeignKey("AreaId")]
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
    }
}

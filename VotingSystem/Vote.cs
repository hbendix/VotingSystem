using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSystemEntities
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }
        public int Priority { get; set; }
        public bool Dormant { get; set; }
        public string Comment { get; set; }

        [ForeignKey("ElectionId")]
        public int ElectionId { get; set; }
        public virtual Election Election { get; set; }

        [ForeignKey("AreaId")]
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }

        public int? CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }

    }
}

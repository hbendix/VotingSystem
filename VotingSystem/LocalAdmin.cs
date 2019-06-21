using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotingSystemEntities.Enums;

namespace VotingSystemEntities
{
    public class LocalAdmin : ApplicationUser
    {        
        [ForeignKey("AreaId")]
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
    }
}

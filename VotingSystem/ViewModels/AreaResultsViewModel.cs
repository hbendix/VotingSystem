using System.Collections.Generic;

namespace VotingSystemEntities.ViewModels
{
    public class AreaResultsViewModel
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public ICollection<CandidateViewModel> CandidatesInArea { get; set; }
    }
}
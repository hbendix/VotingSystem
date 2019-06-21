using System;
using System.Collections.Generic;
using System.Text;

namespace VotingSystemEntities.ViewModels
{
    public class ResultsViewModel
    {
        public PartyViewModel LeadingParty { get; set; }
        public ICollection<AreaResultsViewModel> Areas { get; set; }
        public ICollection<PartyViewModel> Parties { get; set; }
        public int SpoiltVoteCount { get; set; }
    }
}

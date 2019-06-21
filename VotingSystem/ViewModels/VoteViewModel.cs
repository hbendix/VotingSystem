using System;
using System.Collections.Generic;
using System.Text;

namespace VotingSystemEntities.ViewModels
{
    public class VoteViewModel
    {
        public int VoteId { get; set; }
        public int Priority { get; set; }
        public bool Dormant { get; set; }
        public string Comment { get; set; }

        public int ElectionId { get; set; }
        public ElectionViewModel Election { get; set; }

        public int AreaId { get; set; }
        public AreaViewModel Area { get; set; }

        public int? CandidateId { get; set; }
        public CandidateViewModel Candidate { get; set; }

        public static Vote ToDataModel (VoteViewModel vm)
        {
            return new Vote
            {
                AreaId = vm.AreaId,
                CandidateId = vm.CandidateId,
                Dormant = vm.Dormant,
                ElectionId = vm.ElectionId,
                Priority = vm.Priority,
                Comment = vm.Comment
            };
        }

        public static VoteViewModel ToViewModel(Vote x)
        {
            return new VoteViewModel
            {
                ElectionId = x.ElectionId,
                AreaId = x.AreaId,
                CandidateId = x.CandidateId ?? null,
                Comment = x.Comment,
                Area = x.Area != null ? AreaViewModel.ToViewModel(x.Area) : null,
                Candidate = x.Candidate != null ? CandidateViewModel.ToViewModel(x.Candidate, null) : null
            };
        }
    }
}

using System;
using System.Collections.Generic;

namespace VotingSystemEntities
{
    public class CandidateViewModel
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public bool Dormant { get; set; }
        public int? Votes { get; set; }

        public int PartyId { get; set; }
        public PartyViewModel Party { get; set; }

        public int AreaId { get; set; }
        public AreaViewModel Area { get; set; }

        public static CandidateViewModel ToViewModel(Candidate candidate, int? count)
        {
            return new CandidateViewModel
            {
                CandidateName = candidate.CandidateName,
                Dormant = candidate.Dormant,

                AreaId = candidate.AreaId,
                CandidateId = candidate.CandidateId,
                PartyId = candidate.PartyId,
                Votes = count,

                Area = candidate.Area != null ? AreaViewModel.ToViewModel(candidate.Area) : null,
                Party = candidate.Party != null ?  PartyViewModel.ToViewModel(candidate.Party) : null
            };
        }

        internal static Candidate ToDataModel(CandidateViewModel candidate)
        {
            return new Candidate
            {
                CandidateName = candidate.CandidateName,
                Dormant = candidate.Dormant,

                AreaId = candidate.AreaId,
                CandidateId = candidate.CandidateId,
                PartyId = candidate.PartyId,

                Area = candidate.Area != null ? AreaViewModel.ToDataModel(candidate.Area) : null,
                Party = candidate.Party != null ? PartyViewModel.ToDataModel(candidate.Party) : null
            };
        }
    }
}

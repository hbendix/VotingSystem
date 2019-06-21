using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystemEntities.Enums;

namespace VotingSystemEntities
{
    public class ElectionViewModel
    {
        public int ElectionId { get; set; }
        public string ElectionName { get; set; }
        public string Country { get; set; }
        public bool Dormant { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ElectionType ElectionType { get; set; }
        public ICollection<CandidateViewModel> Candidates { get; set; }

        public static ElectionViewModel ToViewModel(Election election)
        {
            return new ElectionViewModel
            {
                ElectionId = election.ElectionId,
                ElectionName = election.ElectionName,
                ElectionType = election.ElectionType,
                Country = election.Country,
                EndDate = election.EndDate,
                StartDate = election.StartDate,
                Dormant = election.Dormant,
                Candidates = election.Candidates != null ? election.Candidates.Select(x => CandidateViewModel.ToViewModel(x, null)).ToList(): null
            };
        }

        public static Election ToDataModel(ElectionViewModel election)
        {
            return new Election
            {
                ElectionId = election.ElectionId,
                ElectionName = election.ElectionName,
                ElectionType = election.ElectionType,
                Country = election.Country,
                EndDate = election.EndDate,
                StartDate = election.StartDate,
                Dormant = election.Dormant,
                Candidates = election.Candidates.Select(x => CandidateViewModel.ToDataModel(x)).ToList()
            };
        }
    }
}

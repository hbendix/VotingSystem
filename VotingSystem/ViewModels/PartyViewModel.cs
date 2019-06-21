using System;

namespace VotingSystemEntities
{
    public class PartyViewModel
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public bool Dormant { get; set; }
        public int? Seats { get; set; }

        public static PartyViewModel ToViewModel(Party party)
        {
            return new PartyViewModel
            {
                Dormant = party.Dormant,
                PartyId = party.PartyId,
                PartyName = party.PartyName
            };
        }

        public static Party ToDataModel(PartyViewModel party)
        {
            return new Party
            {
                Dormant = party.Dormant,
                PartyId = party.PartyId,
                PartyName = party.PartyName
            };
        }
    }
}

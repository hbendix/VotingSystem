using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemEntities.Enums;
using VotingSystemRepositoryAccess.Interfaces;

namespace UnitTests.FakeRepositories
{
    public class FakePartyRepository :IPartyRepository
    {
        public IList<Party> _fakeRepository = new List<Party>(GenerateFakeParty());

        public static Party[] GenerateFakeParty()
        {
            Party newParty = new Party
            {
                PartyName = "Red Party"
            };

            newParty.PartyId = 123456;
            return new Party[] { newParty };
        }

        public Party GenerateAdditionalParty()
        {
            Party additonalParty = new Party
            {
                PartyName = "Green Party"
            };

            additonalParty.PartyId = 654321;
            return additonalParty;
        }

        public Party GenerateUpdatedParty()
        {
            Party updatedParty = new Party
            {
                PartyName = "Blue Party"
            };

            updatedParty.PartyId = 123456;
            return updatedParty;
        }

        public Party GetParty(int partyId)
        {
            return _fakeRepository.Where(x => x.PartyId.Equals(partyId)).Single();
        }

        public async Task AddParty(Party party)
        {
            _fakeRepository.Add(party);
        }

        public async Task UpdateParty(Party updatedParty)
        {
            int indexOfPartyToUpdate = _fakeRepository.IndexOf(GetParty(updatedParty.PartyId));
            _fakeRepository[indexOfPartyToUpdate] = updatedParty;
        }

        public async Task DeleteParty(int partyId)
        {
            Party partyToDelete = GetParty(partyId);
            partyToDelete.Dormant = true;
        }

        public ICollection<Party> GetAllParties()
        {
            return _fakeRepository.Where(x => x.Dormant == false).ToList();
        }
    }
}

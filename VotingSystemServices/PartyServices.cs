using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemRepositoryAccess.Interfaces;
using VotingSystemServices.Interfaces;

namespace VotingSystemServices
{
    public class PartyServices : IPartyServices
    {
        private IPartyRepository _partyRepository;

        public PartyServices(IPartyRepository partyRepository)
        {
            _partyRepository = partyRepository;
        }

        public async Task AddParty(PartyViewModel party)
        {
            await _partyRepository.AddParty(PartyViewModel.ToDataModel(party));
        }

        public async Task DeleteParty(int partyId)
        {
            await _partyRepository.DeleteParty(partyId);
        }

        public ICollection<PartyViewModel> GetAllParties()
        {
            ICollection<Party> parties = _partyRepository.GetAllParties();
            return parties.Select(x => PartyViewModel.ToViewModel(x)).ToList();
        }

        public async Task UpdateParty(PartyViewModel party)
        {
            await _partyRepository.UpdateParty(PartyViewModel.ToDataModel(party));
        }

        public PartyViewModel GetPartyById (int partyId)
        {
            return PartyViewModel.ToViewModel(_partyRepository.GetParty(partyId));
        }
    }
}

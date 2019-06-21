using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VotingSystemEntities;

namespace VotingSystemServices.Interfaces
{
    public interface IPartyServices
    {
        ICollection<PartyViewModel> GetAllParties();
        Task AddParty(PartyViewModel party);
        Task UpdateParty(PartyViewModel party);
        Task DeleteParty(int partyId);
        PartyViewModel GetPartyById(int partyId);
    }
}

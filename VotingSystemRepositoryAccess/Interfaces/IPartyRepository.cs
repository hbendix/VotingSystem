using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystemEntities;

namespace VotingSystemRepositoryAccess.Interfaces
{
    public interface IPartyRepository
    {
        Task AddParty(Party party);
        Party GetParty(int partyId);
        ICollection<Party> GetAllParties();
        Task UpdateParty(Party party);
        Task DeleteParty(int partyId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemRepositoryAccess.Interfaces;

namespace VotingSystemRepositoryAccess
{
    public class PartyRepository : IPartyRepository
    {
        private VotingDbContext _context;

        public PartyRepository(VotingDbContext context)
        {
            _context = context;
        }

        public async Task AddParty(Party party)
        {
            _context.Add(party);

            await _context.SaveChangesAsync();
        }

        public Party GetParty(int partyId)
        {
            return _context.Parties
                .Where(x => x.PartyId == partyId)
                .FirstOrDefault();
        }

        public ICollection<Party> GetAllParties()
        {
            return _context.Parties.Where(x => x.Dormant == false).ToList();
        }

        public async Task UpdateParty(Party party)
        {
            _context.Update(party);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteParty(int partyId)
        {
            var _toDelete = _context.Parties
                .Where(x => x.PartyId == partyId)
                .FirstOrDefault();

            _toDelete.Dormant = true;
            _context.Update(_toDelete);

            await _context.SaveChangesAsync();
        }
    }
}

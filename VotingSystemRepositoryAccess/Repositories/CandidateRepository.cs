using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemRepositoryAccess.Interfaces;

namespace VotingSystemRepositoryAccess
{
    /// <summary>
    /// Data access layer for getting candidates from DB
    /// </summary>
    public class CandidateRepository : ICandidateRepository
    {
        private VotingDbContext _context;

        public CandidateRepository(VotingDbContext context)
        {
            _context = context;
        }

        public async Task AddCandidate(Candidate candidate)
        {
            _context.Add(candidate);

            await _context.SaveChangesAsync();
        }

        public Candidate GetCandidate(int candidateId)
        {
            return _context.Query<Candidate>()
                .Where(x => x.CandidateId == candidateId)
                .FirstOrDefault();
        }

        public async Task UpdateCandidate(Candidate candidate)
        {
            _context.Update(candidate);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCandidate(int candidateId)
        {
            var _toDelete = _context.Query<Candidate>()
                .Where(x => x.CandidateId == candidateId)
                .FirstOrDefault();

            _toDelete.Dormant = true;
            _context.Update(_toDelete);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get list of candidates from Election ID and Area ID
        /// </summary>
        /// <param name="electionId">Unique Election ID</param>
        /// <param name="areaId">Unique Area ID</param>
        /// <returns type="ICollection<Candidate>">List of Candidate entities</returns>
        public ICollection<Candidate> GetCandidateList(int electionId, int areaId)
        {
            // get all candidates in Election
            var toReturn = _context.Elections
                .Where(x => x.ElectionId == electionId && !x.Dormant)
                .Include(x => x.Candidates)
                    .ThenInclude(x => x.Party)
                .FirstOrDefault();

            // return only those in Area
            return toReturn.Candidates.Where(x => x.AreaId == areaId).ToList();
        }
    }
}

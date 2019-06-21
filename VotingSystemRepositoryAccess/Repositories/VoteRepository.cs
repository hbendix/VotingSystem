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
    /// Data access layer for votes from the DB
    /// </summary>
    public class VoteRepository : IVoteRepository
    {
        private VotingDbContext _context;

        public VoteRepository(VotingDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new vote to the DB
        /// </summary>
        /// <param name="vote">Vote model to be added</param>
        /// <returns>async Task Completed</returns>
        public async Task<int> AddVote(Vote vote)
        {
            _context.Add(vote);

            await _context.SaveChangesAsync();

            return vote.VoteId;
        }

        public Vote GetVote(int voteId)
        {
            return _context.Votes
                .Include(x => x.Election)
                .Include(x => x.Area)
                .Include(x => x.Candidate)
                .ThenInclude(x => x.Party)
                .Where(x => x.VoteId == voteId && !x.Dormant)
                .FirstOrDefault();
        }

        public ICollection<Vote> GetVotesWithElectionId(int electionId)
        {
            return _context.Votes
                .Include(x => x.Area)
                .Include(x => x.Candidate)
                .ThenInclude(x => x.Party)
                .Where(x => x.ElectionId == electionId && !x.Dormant)
                .ToList();
        }

        public async Task UpdateVote(Vote vote)
        {
            _context.Update(vote);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteVote(int voteId)
        {
            var _toDelete = _context.Query<Vote>()
                .Where(x => x.VoteId == voteId)
                .FirstOrDefault();

            _toDelete.Dormant = true;
            _context.Update(_toDelete);

            await _context.SaveChangesAsync();
        }
    }
}

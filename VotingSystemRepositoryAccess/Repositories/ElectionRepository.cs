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
    /// Data access layer for elections from the DB
    /// </summary>
    public class ElectionRepository : IElectionRepository
    {
        private VotingDbContext _context;

        public ElectionRepository(VotingDbContext context)
        {
            _context = context;
        }

        public async Task AddElection(Election election)
        {
            _context.Add(election);

            await _context.SaveChangesAsync();
        }

        public Election GetElection(int electionId)
        {
            return _context.Elections
                .Include(x => x.Candidates)
                    .ThenInclude(x => x.Party)
                .Include(x => x.Candidates)
                    .ThenInclude(x => x.Area)
                .Where(x => x.ElectionId == electionId && !x.Dormant)
                .FirstOrDefault();
        }

        public async Task UpdateElection(Election updatedElection)
        {
            _context.Update(updatedElection);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Mark election as dormant by Election ID
        /// </summary>
        /// <param name="electionId">Unique Election ID</param>
        /// <returns type="Task.CompletedTask"></returns>
        public async Task DeleteElection(int electionId)
        {
            // get Election in question
            var _toDelete = _context.Elections
                .Where(x => x.ElectionId == electionId)
                .FirstOrDefault();

            // update Dormant flag and update
            _toDelete.Dormant = true;
            _context.Update(_toDelete);

            // save changes to the DB
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get list of all active elections in the DB
        /// </summary>
        /// <returns type="ICollection<Election>">List of Election entity</returns>
        public ICollection<Election> GetActiveElections(int userId)
        {
            var elections = _context.Elections
                .Include(x => x.UsersVotedIn)
                .Where(x => x.StartDate < DateTime.Now && !x.Dormant)
                .ToList();

            return elections;
        }

        /// <summary>
        /// Add UserId and ElectionId to HasVoted joining table
        /// </summary>
        /// <param name="hasVoted">Unique UserId and Unique ElectionId</param>
        /// <returns></returns>
        public async Task AddUserToElection(HasVoted hasVoted)
        {
            _context.HasVoted.Add(hasVoted);

            await _context.SaveChangesAsync();
        }

        public ICollection<Election> GetAllElections()
        {
            return _context.Elections
                    .Include(x => x.Candidates)
                    .Where(x => !x.Dormant)
                    .ToList();
        }
    }
}

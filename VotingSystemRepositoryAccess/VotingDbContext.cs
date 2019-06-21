using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VotingSystemEntities;

namespace VotingSystemRepositoryAccess
{
    public class VotingDbContext : DbContext
    {
        /// <summary>
        /// Inherits from .NET DbContext, allows us to map our entities to SQL tables.
        /// </summary>
        /// <param name="options">DbContextOptions allows use to to DbSet, used for ORM.</param>
        public VotingDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HasVoted>().HasKey(x => new { x.ElectionId, x.UserId });
        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Election> Elections { get; set; }

        public DbSet<Email> Emails { get; set; }

        public DbSet<Party> Parties { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<LocalAdmin> LocalAdmins { get; set; }

        public DbSet<HasVoted> HasVoted { get; set; }
    }
}

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Pidp.Models;

namespace Pidp.Data
{
    public class PidpDbContext : DbContext
    {
        public PidpDbContext(DbContextOptions<PidpDbContext> options) : base(options) { }

        public DbSet<Party> Parties => Set<Party>();

        public override int SaveChanges()
        {
            // ApplyAudits();

            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ApplyAudits();

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PidpDbContext).Assembly);
        }
    }
}

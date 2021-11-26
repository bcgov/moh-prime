using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Pidp.Models;

namespace Pidp
{
    // public class PidpDbContextFactory : IDesignTimeDbContextFactory<PidpDbContext>
    // {
    //     public PidpDbContext CreateDbContext(string[] args)
    //     {
    //         var connectionString = new ConfigurationBuilder()
    //             .SetBasePath(Directory.GetCurrentDirectory())
    //             .AddJsonFile("appsettings.json")
    //             .Build()
    //             .GetConnectionString("PidpDatabase");

    //         var optionsBuilder = new DbContextOptionsBuilder<PidpDbContext>();
    //         optionsBuilder.UseNpgsql(connectionString);
    //         optionsBuilder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);

    //         return new PidpDbContext(optionsBuilder.Options);
    //     }
    // }

    public class PidpDbContext : DbContext
    {
        public PidpDbContext(DbContextOptions<PidpDbContext> options) : base(options) { }

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

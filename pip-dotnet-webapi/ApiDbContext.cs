using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Pip.Configuration.Environment;
using Pip.Models;

namespace Pip
{
    // Allow for design time creation of the ApiDbContext
    public class ApiDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
    {
        public ApiDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Add(new PipEnvironmentVariablesConfigurationSource())
                .Build();

            var connectionString = config.GetConnectionString("PipDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);

            return new ApiDbContext(optionsBuilder.Options);
        }
    }

    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<FirstModel?>? FirstModel { get; set; }
        public DbSet<NewModel?>? NewModel { get; set; }

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

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
        }
    }
}

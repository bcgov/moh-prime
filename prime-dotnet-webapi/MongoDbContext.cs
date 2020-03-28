using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Prime.Models;
using Prime.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using MongoDB.Driver;

namespace Prime
{
    // Allow for design time creation of the ApiDbContext
    public class MongoDbContextFactory : IDesignTimeDbContextFactory<MongoDbContext>
    {
        public MongoDbContext CreateDbContext(string[] args)
        {
            // Connect to database
            var connectionString = System.Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if (connectionString == null)
            {
                // Build the configuration
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                connectionString = config.GetConnectionString("MongoDatabase");
            }

            var client = new MongoClient(connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<MongoDbContext>();
            // optionsBuilder.UseMongoDb(client);
            // optionsBuilder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);

            return new MongoDbContext(optionsBuilder.Options, null);
        }
    }

    public class MongoDbContext : DbContext
    {
        private readonly DateTimeOffset SEEDING_DATE = DateTimeOffset.Now;

        private readonly IHttpContextAccessor _context;

        public MongoDbContext(
            DbContextOptions<MongoDbContext> options,
            IHttpContextAccessor context
            ) : base(options)
        {
            _context = context;
        }



        public override int SaveChanges()
        {
            this.ApplyAudits();

            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.ApplyAudits();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAudits()
        {
            ChangeTracker.DetectChanges();

            var created = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            var modified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            var currentUser = _context?.HttpContext?.User.GetPrimeUserId() ?? Guid.Empty;
            var currentDateTime = SEEDING_DATE;

            foreach (var item in created)
            {
                if (item.Entity is IAuditable entity)
                {
                    item.CurrentValues[nameof(IAuditable.CreatedUserId)] = currentUser;
                    item.CurrentValues[nameof(IAuditable.CreatedTimeStamp)] = currentDateTime;
                    item.CurrentValues[nameof(IAuditable.UpdatedUserId)] = currentUser;
                    item.CurrentValues[nameof(IAuditable.UpdatedTimeStamp)] = currentDateTime;
                }
            }

            foreach (var item in modified)
            {
                if (item.Entity is IAuditable entity)
                {
                    item.CurrentValues[nameof(IAuditable.UpdatedUserId)] = currentUser;
                    item.CurrentValues[nameof(IAuditable.UpdatedTimeStamp)] = currentDateTime;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        // Uncomment for DB logging
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //     => optionsBuilder.UseLoggerFactory(DbLoggerFactory);

        // public static readonly Microsoft.Extensions.Logging.ILoggerFactory DbLoggerFactory
        //     = new Microsoft.Extensions.Logging.LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });
    }
}

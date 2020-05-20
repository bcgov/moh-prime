using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

using Prime;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.Utils
{
    public class InMemoryDbTest : IDisposable
    {
        protected ApiDbContext TestDb;

        protected InMemoryDbTest()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Only used for the CreatedBy and UpdatedBy fields
            var httpContext = new HttpContextAccessor()
            {
                HttpContext = new DefaultHttpContext()
            };

            TestDb = new ApiDbContext(options, httpContext);
            TestDb.Database.EnsureCreated();

            Seed();
        }

        private void Seed()
        {
            if (TestDb.Statuses.Any())
            {
                return;
            }

            TestDb.AddRange(new BusinessEventTypeConfiguration().SeedData);
            TestDb.AddRange(new CollegeConfiguration().SeedData);
            TestDb.AddRange(new CollegeLicenseConfiguration().SeedData);
            TestDb.AddRange(new CollegePracticeConfiguration().SeedData);
            TestDb.AddRange(new CountryConfiguration().SeedData);
            TestDb.AddRange(new DefaultPrivilegeConfiguration().SeedData);
            TestDb.AddRange(new GlobalClauseConfiguration().SeedData);
            TestDb.AddRange(new JobNameConfiguration().SeedData);
            TestDb.AddRange(new LicenseConfiguration().SeedData);
            TestDb.AddRange(new LicenseClassClauseConfiguration().SeedData);
            TestDb.AddRange(new LicenseClassClauseMappingConfiguration().SeedData);
            TestDb.AddRange(new OrganizationTypeConfiguration().SeedData);
            TestDb.AddRange(new PracticeConfiguration().SeedData);
            TestDb.AddRange(new PrivilegeConfiguration().SeedData);
            TestDb.AddRange(new PrivilegeGroupConfiguration().SeedData);
            TestDb.AddRange(new PrivilegeTypeConfiguration().SeedData);
            TestDb.AddRange(new ProvinceConfiguration().SeedData);
            TestDb.AddRange(new StatusConfiguration().SeedData);
            TestDb.AddRange(new StatusReasonConfiguration().SeedData);
            TestDb.AddRange(new VendorConfiguration().SeedData);

            // Dont import the user class clause resources
            DateTime SEEDING_DATE = new DateTime(2019, 9, 16);
            TestDb.AddRange(new[]{
                new UserClause { Id = 1, Clause = "oboClause1", EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new UserClause { Id = 2, Clause = "ruClause1", EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new UserClause { Id = 3, Clause = "oboClause2", EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = DateTimeOffset.Parse("2020-03-05 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new UserClause { Id = 4, Clause = "ruClause2", EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = DateTimeOffset.Parse("2020-03-05 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
            });

            TestDb.SaveChanges();
        }

        public void Dispose()
        {
            TestDb.Database.EnsureDeleted();
            TestDb.Dispose();
        }
    }
}

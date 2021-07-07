using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

using Prime;
using Prime.ViewModels;
using Prime.Configuration;
using Prime.Configuration.Agreements;

using AutoMapper;

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
            if (TestDb.Vendors.Any())
            {
                return;
            }

            TestDb.AddRange(new BusinessEventTypeConfiguration().SeedData);
            TestDb.AddRange(new CollegeConfiguration().SeedData);
            TestDb.AddRange(new CollegeLicenseConfiguration().SeedData);
            TestDb.AddRange(new CollegePracticeConfiguration().SeedData);
            TestDb.AddRange(new CountryConfiguration().SeedData);
            TestDb.AddRange(new DefaultPrivilegeConfiguration().SeedData);
            TestDb.AddRange(new JobNameConfiguration().SeedData);
            TestDb.AddRange(new LicenseConfiguration().SeedData);
            TestDb.AddRange(new CareSettingConfiguration().SeedData);
            TestDb.AddRange(new PracticeConfiguration().SeedData);
            TestDb.AddRange(new PrivilegeConfiguration().SeedData);
            TestDb.AddRange(new PrivilegeGroupConfiguration().SeedData);
            TestDb.AddRange(new PrivilegeTypeConfiguration().SeedData);
            TestDb.AddRange(new ProvinceConfiguration().SeedData);
            TestDb.AddRange(new StatusConfiguration().SeedData);
            TestDb.AddRange(new StatusReasonConfiguration().SeedData);
            TestDb.AddRange(new VendorConfiguration().SeedData);
            TestDb.AddRange(new HealthAuthorityConfiguration().SeedData);
            TestDb.AddRange(new FacilityConfiguration().SeedData);

            TestDb.AddRange(new AgreementVersionConfiguration().SeedData);

            TestDb.SaveChanges();
        }

        public void Dispose()
        {
            TestDb.Database.EnsureDeleted();
            TestDb.Dispose();
        }

        public IMapper DefaultMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            }).CreateMapper();
        }
    }
}

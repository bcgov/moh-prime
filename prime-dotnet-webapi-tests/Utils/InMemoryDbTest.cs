using AutoMapper;
using FakeItEasy;
using FakeItEasy.Sdk;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Prime;
using Prime.Configuration.Database;
using Prime.Services;
using Prime.ViewModels.Profiles;

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
                cfg.AddMaps(Assembly.GetAssembly(typeof(CommonMappingProfile)));
            }).CreateMapper();
        }

        public TService MockDependenciesFor<TService>(params object[] dependencyOverrides) where TService : BaseService
        {
            // Services derived from BaseService should have exactly one constructor.
            var ctor = typeof(TService).GetConstructors().Single();

            var parameters = new List<object>();
            foreach (var parameter in ctor.GetParameters())
            {
                var targetType = parameter.ParameterType;
                var defaultParameter = targetType == typeof(ApiDbContext)
                    ? TestDb
                    : Create.Fake(targetType);

                parameters.Add(
                    dependencyOverrides.SingleOrDefault(x => x.GetType().GetInterfaces().Contains(targetType))
                    ?? defaultParameter
                );
            }

            return (TService)ctor.Invoke(parameters.ToArray());
        }
    }
}

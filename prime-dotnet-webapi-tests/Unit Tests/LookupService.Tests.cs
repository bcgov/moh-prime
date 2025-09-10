using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using PrimeTests.Utils;
using Prime.Configuration.Database;

namespace PrimeTests.UnitTests
{
    public class LookupServiceTests : InMemoryDbTest
    {
        [Fact(Skip = "This test does not work as intended")]
        public async void TestLookupService_ReturnsAllLookpTypes()
        {
            var service = MockDependenciesFor<LookupService>();
            var lookupTypes = System.Reflection.Assembly
                .GetAssembly(typeof(LookupEntity))
                .GetTypes()
                .Where(type => type.GetInterfaces().Contains(typeof(ILookup<,>)));

            var result = await service.GetLookupsAsync();
            var resultTypes = result
                .GetType()
                .GetProperties()
                .Select(p => p.PropertyType);

            foreach (Type type in lookupTypes)
            {
                Assert.Contains(type, resultTypes);
            }
        }

        [Fact]
        public async void TestGetLookups()
        {
            var service = MockDependenciesFor<LookupService>(DefaultMapper());

            var results = await service.GetLookupsAsync();
            Assert.NotNull(results);

            Assert.NotEmpty(results.Colleges);
            Assert.NotEmpty(results.JobNames);
            Assert.NotEmpty(results.Licenses);
            Assert.NotEmpty(results.CareSettings);
            Assert.NotEmpty(results.Practices);
            Assert.NotEmpty(results.Statuses);
            Assert.NotEmpty(results.Countries);
            Assert.NotEmpty(results.Provinces);
            Assert.NotEmpty(results.StatusReasons);
            Assert.NotEmpty(results.Vendors);
            Assert.NotEmpty(results.HealthAuthorities);
            Assert.NotEmpty(results.Facilities);
        }

        [Fact]
        public async void TestLookupsLicenseFilter()
        {
            var service = MockDependenciesFor<LookupService>(DefaultMapper());

            var results = await service.GetLookupsAsync();
            Assert.NotNull(results);

            Assert.NotEmpty(results.Licenses);
            Assert.Equal(new LicenseConfiguration().SeedData.Count(), results.Licenses.Count);


            var future = new License { Code = 1000, Weight = 1, Name = "Future License" };
            var futureDetail = new LicenseDetail { Id = 1000, LicenseCode = 1000, EffectiveDate = DateTime.UtcNow.AddDays(1), Prefix = "91", Manual = false, Validate = true, NamedInImReg = true, LicensedToProvideCare = true };

            var past = new License { Code = 2000, Weight = 2, Name = "Past License" };
            var pastDetail = new LicenseDetail { Id = 2000, LicenseCode = 2000, EffectiveDate = DateTime.UtcNow, Prefix = "91", Manual = false, Validate = true, NamedInImReg = true, LicensedToProvideCare = true };
            var pastOlderDetail = new LicenseDetail { Id = 1999, LicenseCode = 2000, EffectiveDate = DateTime.UtcNow.AddYears(-1), Prefix = "91", Manual = true, Validate = true, NamedInImReg = true, LicensedToProvideCare = true };

            // Load Test Data
            TestDb.Add(future);
            TestDb.Add(futureDetail);
            TestDb.Add(past);
            TestDb.Add(pastDetail);
            TestDb.Add(pastOlderDetail);

            TestDb.SaveChanges();

            var newResults = await service.GetLookupsAsync();

            Assert.NotNull(newResults);
            Assert.NotEmpty(newResults.Licenses);
            Assert.Equal(new LicenseConfiguration().SeedData.Count() + 1, newResults.Licenses.Count);
            Assert.Empty(newResults.Licenses.Where(l => l.Code == future.Code));
            var pastCodeLicenses = newResults.Licenses.Where(l => l.Code == past.Code);
            Assert.NotEmpty(pastCodeLicenses);
            Assert.Single(pastCodeLicenses);
            Assert.False(pastCodeLicenses.Single().Manual);
        }
    }
}

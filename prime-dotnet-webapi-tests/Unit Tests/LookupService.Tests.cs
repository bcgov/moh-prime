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
            var service = MockDependenciesFor<LookupService>();

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
    }
}

using System.Linq;
using System.Collections.Generic;
using Xunit;

using Prime.Models.HealthAuthorities;
using Prime.Services;
using PrimeTests.Utils;

namespace PrimeTests.UnitTests
{
    public class HealthAuthorityServiceTests : InMemoryDbTest
    {
        [Fact]
        public async void TestGetVendorsByCareTypeAsync()
        {
            var service = MockDependenciesFor<HealthAuthorityService>(DefaultMapper());

            var careTypeA_vancouverCoastalHA = new HealthAuthorityCareType { Id = 10, HealthAuthorityOrganization = TestDb.HealthAuthorities.Find(3) };
            var careTypeB_vancouverCoastalHA = new HealthAuthorityCareType { Id = 20, HealthAuthorityOrganization = TestDb.HealthAuthorities.Find(3) };

            var careTypeA_Plexia = new HealthAuthorityVendor { HealthAuthorityCareType = careTypeA_vancouverCoastalHA, VendorCode = 25 };
            var careTypeB_Plexia = new HealthAuthorityVendor { HealthAuthorityCareType = careTypeB_vancouverCoastalHA, VendorCode = 25 };
            var careTypeB_Cerner = new HealthAuthorityVendor { HealthAuthorityCareType = careTypeB_vancouverCoastalHA, VendorCode = 35 };

            // Load Test Data
            TestDb.Add(careTypeA_Plexia);
            TestDb.Add(careTypeB_Plexia);
            TestDb.Add(careTypeB_Cerner);
            TestDb.SaveChanges();

            IEnumerable<int> vendorCodeMatches = await service.GetVendorsByCareTypeAsync(3, -1);
            Assert.Empty(vendorCodeMatches);
            vendorCodeMatches = await service.GetVendorsByCareTypeAsync(1, 10);
            Assert.Empty(vendorCodeMatches);
            vendorCodeMatches = await service.GetVendorsByCareTypeAsync(3, 10);
            Assert.Equal(25, vendorCodeMatches.ElementAt(0));
            vendorCodeMatches = await service.GetVendorsByCareTypeAsync(3, 20);
            Assert.Equal(2, vendorCodeMatches.Count());
        }
    }
}

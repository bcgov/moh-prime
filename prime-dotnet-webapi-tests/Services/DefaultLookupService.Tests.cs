using Prime.Models;
using Prime.Services;
using Xunit;

namespace PrimeTests.Services
{
    public class DefaultLookupServiceTests : BaseServiceTests<DefaultLookupService>
    {
        [Fact]
        public async void testGetLookups()
        {
            // College Lookups
            {
                var results = await _service.GetLookupsAsync<College>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(4, results.Count);
            }

            // JobName Lookups
            {
                var results = await _service.GetLookupsAsync<JobName>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(8, results.Count);
            }

            // License Lookups
            {
                var results = await _service.GetLookupsAsync<License>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(5, results.Count);
            }

            // OrganizationName Lookups
            {
                var results = await _service.GetLookupsAsync<OrganizationName>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(2, results.Count);
            }

            // OrganizationType Lookups
            {
                var results = await _service.GetLookupsAsync<OrganizationType>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(2, results.Count);
            }

            // Practice Lookups
            {
                var results = await _service.GetLookupsAsync<Practice>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(4, results.Count);
            }

            // Status Lookups
            {
                var results = await _service.GetLookupsAsync<Status>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(6, results.Count);
            }

        }
    }
}
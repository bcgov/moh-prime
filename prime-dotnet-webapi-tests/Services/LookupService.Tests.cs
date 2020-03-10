using Prime.Models;
using Prime.Services;
using Xunit;

namespace PrimeTests.Services
{
    public class LookupServiceTests : BaseServiceTests<LookupService>
    {
        [Fact]
        public async void testGetLookups()
        {
            // College Lookups
            {
                var results = await _service.GetLookupsAsync<int, College>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(4, results.Count);
            }

            // JobName Lookups
            {
                var results = await _service.GetLookupsAsync<int, JobName>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(4, results.Count);
            }

            // License Lookups
            {
                var results = await _service.GetLookupsAsync<int, License>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(5, results.Count);
            }

            // OrganizationType Lookups
            {
                var results = await _service.GetLookupsAsync<int, OrganizationType>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(2, results.Count);
            }

            // Practice Lookups
            {
                var results = await _service.GetLookupsAsync<int, Practice>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(4, results.Count);
            }

            // Status Lookups
            {
                var results = await _service.GetLookupsAsync<int, Status>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(4, results.Count);
            }

            // Country Lookups
            {
                var results = await _service.GetLookupsAsync<string, Country>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Single(results);
            }

            // Province Lookups
            {
                var results = await _service.GetLookupsAsync<string, Province>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(13, results.Count);
            }

            // Status Reason Lookups
            {
                var results = await _service.GetLookupsAsync<int, StatusReason>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(11, results.Count);
            }
        }
    }
}

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
                var results = await _service.GetLookupsAsync<short, College>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(4, results.Count);
            }

            // JobName Lookups
            {
                var results = await _service.GetLookupsAsync<short, JobName>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(4, results.Count);
            }

            // License Lookups
            {
                var results = await _service.GetLookupsAsync<short, License>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(5, results.Count);
            }

            // OrganizationType Lookups
            {
                var results = await _service.GetLookupsAsync<short, OrganizationType>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(2, results.Count);
            }

            // Practice Lookups
            {
                var results = await _service.GetLookupsAsync<short, Practice>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(4, results.Count);
            }

            // Status Lookups
            {
                var results = await _service.GetLookupsAsync<short, Status>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(6, results.Count);
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
                var results = await _service.GetLookupsAsync<short, StatusReason>();
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.Equal(8, results.Count);
            }

        }
    }
}

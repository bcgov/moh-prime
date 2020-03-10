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
                var results = await _service.GetLookupsAsync();
                Assert.NotNull(results);
                Assert.NotEmpty(results.Colleges);
                Assert.Equal(4, results.Colleges.Count);
            }

            // JobName Lookups
            {
                var results = await _service.GetLookupsAsync();
                Assert.NotNull(results);
                Assert.NotEmpty(results.JobNames);
                Assert.Equal(4, results.JobNames.Count);
            }

            // License Lookups
            {
                var results = await _service.GetLookupsAsync();
                Assert.NotNull(results);
                Assert.NotEmpty(results.Licenses);
                Assert.Equal(5, results.Licenses.Count);
            }

            // OrganizationType Lookups
            {
                var results = await _service.GetLookupsAsync();
                Assert.NotNull(results);
                Assert.NotEmpty(results.OrganizationTypes);
                Assert.Equal(2, results.OrganizationTypes.Count);
            }

            // Practice Lookups
            {
                var results = await _service.GetLookupsAsync();
                Assert.NotNull(results);
                Assert.NotEmpty(results.Practices);
                Assert.Equal(4, results.Practices.Count);
            }

            // Status Lookups
            {
                var results = await _service.GetLookupsAsync();
                Assert.NotNull(results);
                Assert.NotEmpty(results.Statuses);
                Assert.Equal(4, results.Statuses.Count);
            }

            // Country Lookups
            {
                var results = await _service.GetLookupsAsync();
                Assert.NotNull(results);
                Assert.NotEmpty(results.Countries);
                Assert.Single(results.Countries);
            }

            // Province Lookups
            {
                var results = await _service.GetLookupsAsync();
                Assert.NotNull(results);
                Assert.NotEmpty(results.Provinces);
                Assert.Equal(13, results.Provinces.Count);
            }

            // Status Reason Lookups
            {
                var results = await _service.GetLookupsAsync();
                Assert.NotNull(results);
                Assert.NotEmpty(results.StatusReasons);
                Assert.Equal(11, results.StatusReasons.Count);
            }
        }
    }
}

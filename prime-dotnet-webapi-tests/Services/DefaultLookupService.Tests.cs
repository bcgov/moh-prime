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
            var result = await _service.GetLookupsAsync<JobName>();
        }
    }
}
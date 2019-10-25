using System.Net;
using Prime.Models;
using PrimeTests.Utils;
using Xunit;

namespace PrimeTests.Controllers
{
    public class LookupsControllerTests : BaseControllerTests
    {
        public LookupsControllerTests(CustomWebApplicationFactory<TestStartup> factory) : base(factory)
        { }

        [Fact]
        public async void testGetEnrolments()
        {
            var response = await _client.GetAsync("/api/lookups");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var results = (await TestUtils.DeserializeResponse<ApiOkResponse<LookupEntity>>(response)).Result;
            Assert.NotNull(results);
            Assert.NotEmpty(results.Colleges);
            Assert.NotEmpty(results.JobNames);
            Assert.NotEmpty(results.Licenses);
            Assert.NotEmpty(results.OrganizationNames);
            Assert.NotEmpty(results.OrganizationTypes);
            Assert.NotEmpty(results.Practices);
            Assert.NotEmpty(results.Statuses);
        }
    }
}
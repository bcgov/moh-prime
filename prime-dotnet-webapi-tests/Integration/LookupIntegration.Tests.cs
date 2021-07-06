using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using Prime.Models.Api;
using PrimeTests.Utils;
using Prime.Controllers;

namespace PrimeTests.Integration
{
    public class LookupIntegrationTests : BaseIntegrationTests
    {
        public LookupIntegrationTests(CustomWebApplicationFactory<TestStartup> factory)
            : base(factory)
        { }

        [Fact]
        public async void TestGetLookups()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // Arrange
                var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/lookups");
                TestUtils.AddAdminAuth(request);

                // Act
                var response = await _client.SendAsync(request);

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                var lookupsResult = await response.Content.ReadAsAsync<ApiResultResponse<LookupEntity>>();
                Assert.NotNull(lookupsResult);
                Assert.NotNull(lookupsResult.Result);
                Assert.NotEmpty(lookupsResult.Result.Licenses);
            }
        }
    }
}

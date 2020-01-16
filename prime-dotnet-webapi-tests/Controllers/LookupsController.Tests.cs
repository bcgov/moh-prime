using System;
using System.Net;
using System.Net.Http;
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
        public async void testGetLookups()
        {
            // create a request with an AUTH token
            var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/lookups", Guid.NewGuid());

            // try to get the lookups
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var results = (await TestUtils.DeserializeResponse<ApiOkResponse<LookupEntity>>(response)).Result;
            Assert.NotNull(results);
            Assert.NotEmpty(results.Colleges);
            Assert.NotEmpty(results.JobNames);
            Assert.NotEmpty(results.Licenses);
            Assert.NotEmpty(results.OrganizationTypes);
            Assert.NotEmpty(results.Practices);
            Assert.NotEmpty(results.Statuses);
            Assert.NotEmpty(results.Countries);
            Assert.NotEmpty(results.Provinces);
            Assert.NotEmpty(results.PrivilegeGroups);
            Assert.NotEmpty(results.PrivilegeTypes);
        }

        [Fact]
        public async void testGetLookups_401_Unauthorized()
        {
            // create a request with an AUTH token
            var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/lookups", Guid.NewGuid());

            //remove the AUTH token
            request.Headers.Authorization = null;

            // try to get the lookups without a token
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}

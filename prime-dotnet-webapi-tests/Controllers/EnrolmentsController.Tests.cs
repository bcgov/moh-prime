using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Mocks;
using PrimeTests.Utils;
using PrimeTests.Utils.Auth;
using Xunit;

namespace PrimeTests.Controllers
{
    public class EnrolmentsControllerTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<TestStartup> _factory;
        // private ApiDbContext _dbContext;

        public EnrolmentsControllerTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
            _client = factory//.CreateClient();
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddScoped<IEnrolmentService, EnrolmentServiceMock>();
                    });
                }).CreateClient();
            // _dbContext = _factory.Server.Host.Services.GetService(typeof(ApiDbContext)) as ApiDbContext;
        }

        [Fact]
        public async Task testGetEnrolments()
        {
            var response = await _client.GetAsync("/api/enrolments");
            var body = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task testGetEnrolments2()
        {
            // TestAuthenticationContext testContext = new TestAuthenticationContext();
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var _dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();

                var testEnrolment = TestUtils.CreateEnrolment(_dbContext);

                var response = await _client.GetAsync("/api/enrolments");
                var body = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        // [Fact]
        // public async Task testGetEnrolments3()
        // {
        //     TestAuthenticationContext testContext = new TestAuthenticationContext(Guid.NewGuid().ToString());
        //     var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolements");
        //     var _token = testContext.TokenBuilder
        //         .ForAudience("prime-web-api")
        //         .ForSubject("1234567890")
        //         .BuildToken();

        //     request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

        //     var _response = await _client.SendAsync(request);
        //     Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        // }

        [Fact]
        public async Task<Enrolment> createEnrolment()
        {
            Enrolment createdEnrolment;
            // TestAuthenticationContext testContext = new TestAuthenticationContext();
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var _dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();

                var testEnrolment = TestUtils.EnrolmentFaker.Generate();

                var response = await _client.PostAsJsonAsync("/api/enrolements", testEnrolment);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains(testEnrolment.Enrollee.FirstName, body);

                createdEnrolment = JsonConvert.DeserializeObject<Enrolment>(body);
                Assert.Equal(testEnrolment.Enrollee.UserId, createdEnrolment.Enrollee.UserId);

                //pull the id off the location URI, and stick into the created enrolment
                var enrolmentId = response.Headers.Location.Segments.Last();
                createdEnrolment.Id = int.Parse(enrolmentId);
            }
            return createdEnrolment;
        }
    }

}
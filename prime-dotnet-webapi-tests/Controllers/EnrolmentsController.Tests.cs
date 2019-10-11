using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Mocks;
using PrimeTests.Utils;
using PrimeTests.Utils.Auth;

namespace PrimeTests.Controllers
{
    public class EnrolmentsControllerTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;
        private readonly HttpClient _client;

        public EnrolmentsControllerTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddSingleton<IEnrolmentService, EnrolmentServiceMock>();
                    });
                });
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task testGetEnrolments()
        {
            var response = await _client.GetAsync("/api/enrolments");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var enrolments = (await TestUtils.DeserializeResponse<ApiOkResponse<IEnumerable<Enrolment>>>(response)).Result;
            Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
        }

        [Fact]
        public async Task testGetEnrolments4()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolments");

            BearerTokenBuilder _tokenBuilder = new BearerTokenBuilder()
                                    .ForAudience(TestAuthorizationConstants.Audience)
                                    .IssuedBy(TestAuthorizationConstants.Issuer)
                                    .WithSigningCertificate(EmbeddedResourceReader.GetCertificate("prime-api"));
            var _token = _tokenBuilder
                .ForAudience("prime-web-api")
                .ForSubject("1234567890")
                .BuildToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var enrolments = (await TestUtils.DeserializeResponse<ApiOkResponse<IEnumerable<Enrolment>>>(response)).Result;
            Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
        }

        [Fact]
        public async Task testGetEnrolments2()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                var response = await _client.GetAsync("/api/enrolments");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var enrolments = (await TestUtils.DeserializeResponse<ApiOkResponse<IEnumerable<Enrolment>>>(response)).Result;
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async Task testGetEnrolments3()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolments");
            var _token = TestUtils.TokenBuilder()
                .ForAudience("prime-web-api")
                .ForSubject("1234567890")
                .BuildToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

            var _response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }

        [Fact]
        public async Task<Enrolment> testCreateEnrolment()
        {
            Enrolment createdEnrolment;
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var _dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();

                var testEnrolment = TestUtils.EnrolmentFaker.Generate();

                var response = await _client.PostAsJsonAsync("/api/enrolments", testEnrolment);
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
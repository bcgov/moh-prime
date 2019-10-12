using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

using Prime;
using Prime.Models;
using PrimeTests.Utils;

namespace PrimeTests.Integration
{
    public class EnrolmentIntegrationTests : BaseIntegrationTests
    {
        public EnrolmentIntegrationTests(CustomWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        private Enrolment CreateEnrolment(IServiceScope scope)
        {
            var enrolment = TestUtils.EnrolmentFaker.Generate();
            var _dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
            _dbContext.Enrolments.Add(enrolment);
            _dbContext.SaveChanges();

            return enrolment;
        }

        private Enrolment GetEnrolment(IServiceScope scope, int enrolmentId)
        {
            var _dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
            return _dbContext.Enrolments
                        .Include(e => e.Enrollee).ThenInclude(e => e.PhysicalAddress)
                        .Include(e => e.Enrollee).ThenInclude(e => e.MailingAddress)
                        .Include(e => e.Certifications)
                        .Include(e => e.Jobs)
                        .Include(e => e.Organizations)
                        .AsNoTracking().Single(e => e.Id == enrolmentId);
        }

        [Fact]
        public async void testGetEnrolments()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var response = await _client.GetAsync("/api/enrolments");
                var body = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async void testGetEnrolments2()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                this.CreateEnrolment(scope);

                var response = await _client.GetAsync("/api/enrolments");
                var body = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async void testGetEnrolments3()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
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
        }

        [Fact]
        public async void createEnrolment()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var testEnrolment = TestUtils.EnrolmentFaker.Generate();

                var response = await _client.PostAsJsonAsync("/api/enrolments", testEnrolment);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains(testEnrolment.Enrollee.FirstName, body);

                Enrolment createdEnrolment = JsonConvert.DeserializeObject<Enrolment>(body);
                Assert.Equal(testEnrolment.Enrollee.UserId, createdEnrolment.Enrollee.UserId);
            }
        }

        [Fact]
        public async void getSingleEnrolment()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var enrolment = this.CreateEnrolment(scope);
                int expectedEnrolmentId = (int)enrolment.Id;
                string expectedUserId = enrolment.Enrollee.UserId;

                var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolments/" + expectedEnrolmentId);
                var _token = TestUtils.TokenBuilder()
                    .ForAudience("prime-web-api")
                    .ForSubject(expectedUserId)
                    .WithClaim(ClaimTypes.Role, PrimeConstants.PRIME_ENROLMENT_ROLE)
                    .BuildToken();

                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

                // Get enrolment by Id
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var body = await response.Content.ReadAsStringAsync();
                Enrolment responseEnrolment = JsonConvert.DeserializeObject<ApiOkResponse<Enrolment>>(body).Result;
                Assert.Equal(expectedEnrolmentId, responseEnrolment.Id);
                Assert.Equal(expectedUserId, responseEnrolment.Enrollee.UserId);
            }
        }

        [Fact]
        public async void getAllEnrolments()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                this.CreateEnrolment(scope);
                this.CreateEnrolment(scope);
                this.CreateEnrolment(scope);

                var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolments");
                var _token = TestUtils.TokenBuilder()
                    .ForAudience("prime-web-api")
                    .ForSubject("admin")
                    .WithClaim(ClaimTypes.Role, PrimeConstants.PRIME_ADMIN_ROLE)
                    .BuildToken();

                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

                // Get all enrolments for ADMIN user
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var body = await response.Content.ReadAsStringAsync();
                Enrolment[] responseEnrolments = JsonConvert.DeserializeObject<ApiOkResponse<Enrolment[]>>(body).Result;
                Assert.Equal(3, responseEnrolments.Count());
            }
        }

        [Fact]
        public async void updateSingleEnrolment()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                Enrolment enrolment = this.CreateEnrolment(scope);
                string expectedFirstName = "NewFirstName";
                int enrolmentId = (int)enrolment.Id;

                Assert.NotEqual(expectedFirstName, enrolment.Enrollee.FirstName);

                //update the first name
                enrolment.Enrollee.FirstName = expectedFirstName;

                var response = await _client.PutAsJsonAsync("/api/enrolments/" + enrolmentId, enrolment);
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                Enrolment updatedEnrolment = this.GetEnrolment(scope, enrolmentId);
                Assert.Equal(expectedFirstName, updatedEnrolment.Enrollee.FirstName);
            }
        }
    }
}

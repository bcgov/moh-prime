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
using System;
using System.Text;

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
                // create a request with an AUTH token
                var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolments");
                var _token = TestUtils.TokenBuilder()
                    .ForAudience("prime-web-api")
                    .ForSubject(Guid.NewGuid().ToString())
                    .WithClaim(ClaimTypes.Role, PrimeConstants.PRIME_ENROLMENT_ROLE)
                    .BuildToken();

                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async void testGetEnrolments2()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var enrolment = this.CreateEnrolment(scope);

                // create a request with an AUTH token
                var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolments");
                var _token = TestUtils.TokenBuilder()
                    .ForAudience("prime-web-api")
                    .ForSubject(enrolment.Enrollee.UserId.ToString())
                    .WithClaim(ClaimTypes.Role, PrimeConstants.PRIME_ENROLMENT_ROLE)
                    .BuildToken();

                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

                var response = await _client.SendAsync(request);
                var body = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async void createEnrolment()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var testEnrolment = TestUtils.EnrolmentFaker.Generate();

                // create a request with an AUTH token
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/enrolments");
                var _token = TestUtils.TokenBuilder()
                    .ForAudience("prime-web-api")
                    .ForSubject(Guid.NewGuid().ToString())
                    .WithClaim(ClaimTypes.Role, PrimeConstants.PRIME_ENROLMENT_ROLE)
                    .BuildToken();
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

                // try to create the enrolment
                request.Content = new StringContent(JsonConvert.SerializeObject(testEnrolment), Encoding.UTF8, "application/json");
                var response = await _client.SendAsync(request);
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
                Guid expectedUserId = enrolment.Enrollee.UserId;

                // create a request with an AUTH token
                var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolments/" + expectedEnrolmentId);
                var _token = TestUtils.TokenBuilder()
                    .ForAudience("prime-web-api")
                    .ForSubject(expectedUserId.ToString())
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

                // create a request with an AUTH token
                var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolments");
                var _token = TestUtils.TokenBuilder()
                    .ForAudience("prime-web-api")
                    .ForSubject(Guid.NewGuid().ToString())
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

                // create a request with an AUTH token
                var request = new HttpRequestMessage(HttpMethod.Put, "/api/enrolments/" + enrolmentId);
                var _token = TestUtils.TokenBuilder()
                    .ForAudience("prime-web-api")
                    .ForSubject(enrolment.Enrollee.UserId.ToString())
                    .WithClaim(ClaimTypes.Role, PrimeConstants.PRIME_ENROLMENT_ROLE)
                    .BuildToken();

                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

                request.Content = new StringContent(JsonConvert.SerializeObject(enrolment), Encoding.UTF8, "application/json");
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                Enrolment updatedEnrolment = this.GetEnrolment(scope, enrolmentId);
                Assert.Equal(expectedFirstName, updatedEnrolment.Enrollee.FirstName);
            }
        }
    }
}

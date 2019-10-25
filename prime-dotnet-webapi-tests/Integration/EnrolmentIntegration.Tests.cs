using System;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            // For integration tests, remove current status 'Status Object' that the faker created so it doesn't try to save the status twice to the DbContext
            enrolment.CurrentStatus.Status = null;
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
                        .Include(e => e.EnrolmentStatuses)
                        .AsNoTracking().Single(e => e.Id == enrolmentId);
        }

        [Fact]
        public async void testGetEnrolments()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/enrolments", Guid.NewGuid());

                // send the request
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
                var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/enrolments", enrolment.Enrollee.UserId);

                // send the request
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
                // For integration tests, remove the enrolment status that the faker created, as it should get created by the service layer
                testEnrolment.EnrolmentStatuses.Clear();

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Enrolment>(HttpMethod.Post, "/api/enrolments", testEnrolment.Enrollee.UserId, testEnrolment);

                // try to create the enrolment
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains(testEnrolment.Enrollee.FirstName, body);

                Enrolment createdEnrolment = JsonConvert.DeserializeObject<ApiCreatedResponse<Enrolment>>(body).Result;
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
                var request = TestUtils.CreateRequest(HttpMethod.Get, $"/api/enrolments/{expectedEnrolmentId}", enrolment.Enrollee.UserId);

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
                var request = TestUtils.CreateAdminRequest(HttpMethod.Get, $"/api/enrolments", Guid.NewGuid());

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
                var request = TestUtils.CreateRequest<Enrolment>(HttpMethod.Put, $"/api/enrolments/{enrolmentId}", enrolment.Enrollee.UserId, enrolment);

                // try to update the enrolment
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                Enrolment updatedEnrolment = this.GetEnrolment(scope, enrolmentId);
                Assert.Equal(expectedFirstName, updatedEnrolment.Enrollee.FirstName);
            }
        }
    }
}

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
    public class EnrolleeIntegrationTests : BaseIntegrationTests
    {
        public EnrolleeIntegrationTests(CustomWebApplicationFactory<TestStartup> factory) : base(factory)
        { }

        private Enrollee CreateEnrollee(IServiceScope scope)
        {
            var enrollee = TestUtils.EnrolleeFaker.Generate();
            // For integration tests, remove current status 'Status Object' that the faker created so it doesn't try to save the status twice to the DbContext
            enrollee.CurrentStatus.Status = null;
            var _dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
            _dbContext.Enrollees.Add(enrollee);
            _dbContext.SaveChanges();

            return enrollee;
        }

        private Enrollee GetEnrollee(IServiceScope scope, int enrolleeId)
        {
            var _dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
            return _dbContext.Enrollees
                        .Include(e => e.PhysicalAddress)
                        .Include(e => e.MailingAddress)
                        .Include(e => e.Certifications)
                        .Include(e => e.Jobs)
                        .Include(e => e.Organizations)
                        .Include(e => e.EnrolmentStatuses)
                        .AsNoTracking().Single(e => e.Id == enrolleeId);
        }

        [Fact]
        public async void testGetEnrollees()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/enrollees", Guid.NewGuid());

                // send the request
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async void testGetEnrollees2()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var enrollee = this.CreateEnrollee(scope);

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/enrollees", enrollee.UserId);

                // send the request
                var response = await _client.SendAsync(request);
                var body = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async void createEnrollee()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var testEnrollee = TestUtils.EnrolleeFaker.Generate();
                // For integration tests, remove the enrolment status that the faker created, as it should get created by the service layer
                testEnrollee.EnrolmentStatuses.Clear();

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Enrollee>(HttpMethod.Post, "/api/enrollees", testEnrollee.UserId, testEnrollee);

                // try to create the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains(testEnrollee.FirstName, body);

                Enrollee createdEnrollee = JsonConvert.DeserializeObject<ApiCreatedResponse<Enrollee>>(body).Result;
                Assert.Equal(testEnrollee.UserId, createdEnrollee.UserId);
            }
        }

        [Fact]
        public async void getSingleEnrollee()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var enrollee = this.CreateEnrollee(scope);
                int expectedEnrolleeId = (int)enrollee.Id;
                Guid expectedUserId = enrollee.UserId;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get, $"/api/enrollees/{expectedEnrolleeId}", enrollee.UserId);

                // Get enrollee by Id
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var body = await response.Content.ReadAsStringAsync();
                Enrollee responseEnrollee = JsonConvert.DeserializeObject<ApiOkResponse<Enrollee>>(body).Result;
                Assert.Equal(expectedEnrolleeId, responseEnrollee.Id);
                Assert.Equal(expectedUserId, responseEnrollee.UserId);
            }
        }

        [Fact]
        public async void getAllEnrollees()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                this.CreateEnrollee(scope);
                this.CreateEnrollee(scope);
                this.CreateEnrollee(scope);

                // create a request with an AUTH token
                var request = TestUtils.CreateAdminRequest(HttpMethod.Get, $"/api/enrollees", Guid.NewGuid());

                // Get all enrollees for ADMIN user
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var body = await response.Content.ReadAsStringAsync();
                Enrollee[] responseEnrollees = JsonConvert.DeserializeObject<ApiOkResponse<Enrollee[]>>(body).Result;
                Assert.Equal(3, responseEnrollees.Count());
            }
        }

        [Fact]
        public async void updateSingleEnrollee()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                Enrollee enrollee = this.CreateEnrollee(scope);
                string expectedFirstName = "NewFirstName";
                int enrolleeId = (int)enrollee.Id;

                Assert.NotEqual(expectedFirstName, enrollee.FirstName);

                //update the first name
                enrollee.FirstName = expectedFirstName;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Enrollee>(HttpMethod.Put, $"/api/enrollees/{enrolleeId}", enrollee.UserId, enrollee);

                // try to update the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                Enrollee updatedEnrollee = this.GetEnrollee(scope, enrolleeId);
                Assert.Equal(expectedFirstName, updatedEnrollee.FirstName);
            }
        }
    }
}

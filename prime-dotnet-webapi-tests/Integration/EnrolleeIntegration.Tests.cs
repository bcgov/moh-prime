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
using Prime.Models.Api;
using Prime.ViewModels;

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
                        .Include(e => e.EnrolleeCareSettings)
                        .Include(e => e.EnrolmentStatuses)
                        .AsNoTracking().Single(e => e.Id == enrolleeId);
        }

        [Fact]
        public async void TestGetEnrollees()
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
        public async void TestGetEnrollees2()
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
        public async void TestCreateBcscEnrollee()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var testEnrollee = TestUtils.EnrolleeFaker.Generate();
                var payload = new
                {
                    Enrollee = testEnrollee
                };

                var request = TestUtils.CreateRequest(HttpMethod.Post, "/api/enrollees", testEnrollee.UserId, payload);

                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                var body = await response.Content.ReadAsStringAsync();
                Enrollee createdEnrollee = JsonConvert.DeserializeObject<ApiResultResponse<Enrollee>>(body).Result;

                Assert.Equal(testEnrollee.UserId, createdEnrollee.UserId);
            }
        }

        [Fact]
        public async void TestGetSingleEnrollee()
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
                Enrollee responseEnrollee = JsonConvert.DeserializeObject<ApiResultResponse<Enrollee>>(body).Result;
                Assert.Equal(expectedEnrolleeId, responseEnrollee.Id);
                Assert.Equal(expectedUserId, responseEnrollee.UserId);
            }
        }

        [Fact(Skip = "Test fails for an unknown reason")]
        public async void TestGetAllEnrollees()
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
                Enrollee[] responseEnrollees = JsonConvert.DeserializeObject<ApiResultResponse<Enrollee[]>>(body).Result;
                Assert.Equal(3, responseEnrollees.Count());
            }
        }

        [Fact]
        public async void TestUpdateSingleEnrollee()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                Enrollee enrollee = this.CreateEnrollee(scope);
                string expectedFirstName = "NewFirstName";

                Assert.NotEqual(expectedFirstName, enrollee.FirstName);

                var enrolleeProfile = new EnrolleeUpdateModel
                {
                    PreferredFirstName = expectedFirstName
                };

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Put, $"/api/enrollees/{enrollee.Id}", enrollee.UserId, enrolleeProfile);

                // try to update the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                Enrollee updatedEnrollee = this.GetEnrollee(scope, enrollee.Id);
                Assert.Equal(expectedFirstName, updatedEnrollee.PreferredFirstName);
            }
        }
    }
}

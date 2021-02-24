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
                var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/enrollees");
                TestUtils.AddAdminAuth(request);

                var response = await _client.SendAsync(request);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async void TestGetEnrollees2()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var enrollee = CreateEnrollee(scope);
                var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/enrollees");
                TestUtils.AddEnrolleeAuth(request, enrollee);

                var response = await _client.SendAsync(request);

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
                var request = TestUtils.CreateRequest(HttpMethod.Post, "/api/enrollees", payload);
                TestUtils.AddEnrolleeAuth(request, testEnrollee);

                var response = await _client.SendAsync(request);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                var createdEnrollee = (await response.Content.ReadAsAsync<ApiResultResponse<Enrollee>>()).Result;
                Assert.Equal(testEnrollee.UserId, createdEnrollee.UserId);
            }
        }

        [Fact]
        public async void TestGetSingleEnrollee()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var expectedEnrollee = CreateEnrollee(scope);
                var request = TestUtils.CreateRequest(HttpMethod.Get, $"/api/enrollees/{expectedEnrollee.Id}");
                TestUtils.AddEnrolleeAuth(request, expectedEnrollee);

                var response = await _client.SendAsync(request);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                var responseEnrollee = (await response.Content.ReadAsAsync<ApiResultResponse<Enrollee>>()).Result;
                Assert.Equal(expectedEnrollee.Id, responseEnrollee.Id);
                Assert.Equal(expectedEnrollee.UserId, responseEnrollee.UserId);
            }
        }

        [Fact(Skip = "Test fails with an error specific to the In-Memory databse we are using when projecting to the Enrollee List ViewModel")]
        public async void TestGetAllEnrollees()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                CreateEnrollee(scope);
                CreateEnrollee(scope);
                CreateEnrollee(scope);
                var request = TestUtils.CreateRequest(HttpMethod.Get, $"/api/enrollees");
                TestUtils.AddAdminAuth(request);

                var response = await _client.SendAsync(request);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Enrollee[] responseEnrollees = (await response.Content.ReadAsAsync<ApiResultResponse<Enrollee[]>>()).Result;
                Assert.Equal(3, responseEnrollees.Count());
            }
        }

        [Fact]
        public async void TestUpdateSingleEnrollee()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                Enrollee enrollee = CreateEnrollee(scope);
                var updateModel = enrollee.CopyToUpdateModel();
                updateModel.Email = enrollee.Email.Bump();
                var request = TestUtils.CreateRequest(HttpMethod.Put, $"/api/enrollees/{enrollee.Id}", updateModel);
                TestUtils.AddEnrolleeAuth(request, enrollee);

                var response = await _client.SendAsync(request);

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                Enrollee updatedEnrollee = GetEnrollee(scope, enrollee.Id);
                Assert.Equal(updateModel.Email, updatedEnrollee.Email);
            }
        }
    }
}

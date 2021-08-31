using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using Prime;
using Prime.Models;
using Prime.Models.Api;
using PrimeTests.Utils;
using Prime.Controllers;

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

        // [Fact]
        // public async void TestGetEnrollees_AsEnrollee()
        // {
        //     using (var scope = _factory.Server.Host.Services.CreateScope())
        //     {
        //         var enrollee = CreateEnrollee(scope);
        //         var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/enrollees");
        //         TestUtils.AddEnrolleeAuth(request, enrollee);

        //         var response = await _client.SendAsync(request);

        //         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //     }
        // }

        // [Fact]
        // public async void TestGetEnrolleeById()
        // {
        //     using (var scope = _factory.Server.Host.Services.CreateScope())
        //     {
        //         var expectedEnrollee = CreateEnrollee(scope);
        //         var request = TestUtils.CreateRequest(HttpMethod.Get, $"/api/enrollees/{expectedEnrollee.Id}");
        //         TestUtils.AddEnrolleeAuth(request, expectedEnrollee);

        //         var response = await _client.SendAsync(request);

        //         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //         var responseEnrollee = (await response.Content.ReadAsAsync<ApiResultResponse<Enrollee>>()).Result;
        //         Assert.Equal(expectedEnrollee.Id, responseEnrollee.Id);
        //         Assert.Equal(expectedEnrollee.UserId, responseEnrollee.UserId);
        //     }
        // }

        // [Fact]
        // public async void TestCreateEnrollee()
        // {
        //     using (var scope = _factory.Server.Host.Services.CreateScope())
        //     {
        //         var testEnrollee = TestUtils.EnrolleeFaker.Generate();
        //         var request = TestUtils.CreateRequest(HttpMethod.Post, "/api/enrollees", new { Enrollee = testEnrollee });
        //         TestUtils.AddEnrolleeAuth(request, testEnrollee);

        //         var response = await _client.SendAsync(request);

        //         Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        //         var createdEnrollee = (await response.Content.ReadAsAsync<ApiResultResponse<Enrollee>>()).Result;
        //         Assert.Equal(testEnrollee.UserId, createdEnrollee.UserId);
        //     }
        // }
    }
}

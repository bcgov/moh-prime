using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using Prime.Models;
using Prime.Services;
using PrimeTests.Mocks;
using PrimeTests.Utils;

namespace PrimeTests.Controllers
{
    public class EnrolleesControllerTests : BaseControllerTests
    {
        public EnrolleesControllerTests(CustomWebApplicationFactory<TestStartup> factory) : base(factory)
        { }

        [Fact]
        public async void testGetEnrollees()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync();
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrollees.Count());

                //pick off an enrollee to get the userId from
                Enrollee expectedEnrollee = enrollees.First();

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/enrollees", expectedEnrollee.UserId);

                // try to get the enrollees
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the controller returned only the one user's enrollee record
                var returnedEnrollees = (await TestUtils.DeserializeResponse<ApiOkResponse<IEnumerable<Enrollee>>>(response)).Result;
                Assert.Single(returnedEnrollees);
            }
        }

        [Fact]
        public async void testGetEnrollees_Admin()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync();
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrollees.Count());

                // create a request with an AUTH token
                var request = TestUtils.CreateAdminRequest(HttpMethod.Get, "/api/enrollees", Guid.NewGuid());

                // try to get the enrollees
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the controller returned only the one user's enrollee record
                var returnedEnrollees = (await TestUtils.DeserializeResponse<ApiOkResponse<IEnumerable<Enrollee>>>(response)).Result;
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, returnedEnrollees.Count());
            }
        }
    }
}
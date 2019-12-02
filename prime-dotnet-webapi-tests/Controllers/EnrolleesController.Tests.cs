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
using Newtonsoft.Json;
using Prime;

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
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

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
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // create a request with an AUTH token
                var request = TestUtils.CreateAdminRequest(HttpMethod.Get, "/api/enrollees", Guid.NewGuid());

                // try to get the enrollees
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the controller returned only the one user's enrollee record
                var returnedEnrollees = (await TestUtils.DeserializeResponse<ApiOkResponse<IEnumerable<Enrollee>>>(response)).Result;
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, returnedEnrollees.Count());
            }
        }

        [Fact]
        public async void testGetEnrollees_401_Unauthorized()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync();
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                //pick off an enrollee to get the userId from
                Enrollee expectedEnrollee = enrollees.First();

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/enrollees", expectedEnrollee.UserId);

                //remove the AUTH token
                request.Headers.Authorization = null;

                // try to get the enrollees without a token
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync();
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testGetEnrollee()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to get
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get, $"/api/enrollees/{expectedEnrolleeId}", expectedEnrollee.Enrollee.UserId);

                // try to get the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the enrollee was returned
                var enrollee = (await TestUtils.DeserializeResponse<ApiOkResponse<Enrollee>>(response)).Result;
                Assert.NotNull(enrollee);
                Assert.Equal(expectedEnrolleeId, enrollee.Id);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testGetEnrollee_404_NotFound()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // get an enrollee id that does not exist
                int notFoundEnrolleeId = EnrolleeServiceMock.MAX_ENROLLEE_ID + 1;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get, $"/api/enrollees/{notFoundEnrolleeId}", Guid.NewGuid());

                // try to get an enrollee that does not exist
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testGetEnrollee_403_Forbidden()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to get
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get, $"/api/enrollees/{expectedEnrolleeId}", Guid.NewGuid());

                // try to get an enrollee with a different userId
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testGetEnrollee_401_Unauthorized()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to get
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get, $"/api/enrollees/{expectedEnrolleeId}", expectedEnrollee.Enrollee.UserId);

                //remove the AUTH token
                request.Headers.Authorization = null;

                // try to get an enrollee without a token
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testCreateEnrollee()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                //get references to the services
                var _dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();

                // make a new enrollee object
                var testEnrollee = TestUtils.EnrolleeFaker.Generate();

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Post, "/api/enrollees", Guid.NewGuid(), testEnrollee);

                // try to create the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                // check that the body contains the Enrollee first name
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains(testEnrollee.FirstName, body);

                // check that the body contains the Enrollee UserId
                Enrollee createdEnrollee = JsonConvert.DeserializeObject<ApiCreatedResponse<Enrollee>>(body).Result;
                Assert.Equal(testEnrollee.UserId, createdEnrollee.UserId);
            }
        }

        [Fact]
        public async void testCreateEnrollee_401_Unauthorized()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                //get references to the services
                var _dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();

                // make a new enrollee object
                var testEnrollee = TestUtils.EnrolleeFaker.Generate();

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Post, "/api/enrollees", Guid.NewGuid(), testEnrollee);

                //remove the AUTH token
                request.Headers.Authorization = null;

                // try to create the enrollee without a token
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Fact]
        public async void testCreateEnrollee_400_BadRequest_Enrollee_Exists()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to use for the userId
                Enrollee existingEnrollee = enrollees.First();

                // make a new enrollee object
                var testEnrollee = TestUtils.EnrolleeFaker.Generate();
                testEnrollee.UserId = existingEnrollee.UserId;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Post, "/api/enrollees", existingEnrollee.UserId, testEnrollee);

                // try to create the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("An enrollee already exists for this User Id, only one enrollee is allowed per User Id.", body);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testDeleteEnrollee()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to delete
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Delete, $"/api/enrollees/{expectedEnrolleeId}", expectedEnrollee.Enrollee.UserId);

                // try to delete the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the enrollee was removed
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE - 1, enrollees.Count());
            }
        }

        [Fact]
        public async void testDeleteEnrollee_404_NotFound()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // get an enrollee id that does not exist
                int notFoundEnrolleeId = EnrolleeServiceMock.MAX_ENROLLEE_ID + 1;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Delete, $"/api/enrollees/{notFoundEnrolleeId}", Guid.NewGuid());

                // try to delete a non-existing enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testDeleteEnrollee_403_Forbidden()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to delete
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Delete, $"/api/enrollees/{expectedEnrolleeId}", Guid.NewGuid());

                // try to delete the enrollee with a different userId
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

                // check that the enrollee was not removed
                Assert.True(service.EnrolleeExists(expectedEnrolleeId));
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testUpdateEnrollee()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to update
                Enrollee enrollee = enrollees.First();
                int enrolleeId = (int)enrollee.Id;
                string previousFirstName = enrollee.FirstName;
                string previousLastName = enrollee.LastName;
                string expectedFirstName = "NewFirstName";
                string expectedLastName = "NewLastName";

                // update the names
                enrollee.FirstName = expectedFirstName;
                enrollee.LastName = expectedLastName;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Enrollee>(HttpMethod.Put, $"/api/enrollees/{enrolleeId}", enrollee.UserId, enrollee);

                // call the controller to update the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // check the updated enrollee in the database
                var updatedEnrollee = await service.GetEnrolleeAsync(enrolleeId);
                Assert.NotNull(updatedEnrollee);
                Assert.Equal(enrolleeId, updatedEnrollee.Id);
                Assert.Equal(expectedFirstName, updatedEnrollee.FirstName);
                Assert.Equal(expectedLastName, updatedEnrollee.LastName);
            }
        }

        [Fact]
        public async void testUpdateEnrollee_400_BadRequest_Empty_UserId()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to update
                Enrollee enrollee = enrollees.First();
                int enrolleeId = (int)enrollee.Id;

                // get the User id from the record
                Guid subject = enrollee.UserId;

                // put in an invalid userId
                enrollee.UserId = Guid.Empty;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Enrollee>(HttpMethod.Put, $"/api/enrollees/{enrolleeId}", subject, enrollee);

                // call the controller to update the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("UserId cannot be the empty value", body);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testUpdateEnrollee_400_BadRequest_Mismatched_EnrolleeId()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to update
                Enrollee enrollee = enrollees.First();
                int enrolleeId = (int)enrollee.Id;

                // put in a mismatched enrolleeId
                enrollee.Id = enrolleeId + 1;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Enrollee>(HttpMethod.Put, $"/api/enrollees/{enrolleeId}", enrollee.UserId, enrollee);

                // call the controller to update the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("Enrollee Id does not match with the payload.", body);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testUpdateEnrollee_400_BadRequest_Missing_EnrolleeId()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to update
                Enrollee enrollee = enrollees.First();
                int enrolleeId = (int)enrollee.Id;

                // remove the enrolleeId
                enrollee.Id = null;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Enrollee>(HttpMethod.Put, $"/api/enrollees/{enrolleeId}", enrollee.UserId, enrollee);

                // call the controller to update the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("Enrollee Id is required to make updates.", body);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testUpdateEnrollee_404_NotFound()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to update
                Enrollee enrollee = enrollees.First();

                // change the enrolleeId to one that will not be found
                int notFoundEnrolleeId = EnrolleeServiceMock.MAX_ENROLLEE_ID + 1;
                enrollee.Id = notFoundEnrolleeId;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Enrollee>(HttpMethod.Put,
                 $"/api/enrollees/{notFoundEnrolleeId}", enrollee.UserId, enrollee);

                // call the controller to update the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("Enrollee not found with id " + notFoundEnrolleeId, body);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testUpdateEnrollee_400_BadRequest_WrongEnrolleeStatus()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to update
                Enrollee enrollee = enrollees.First();
                int enrolleeId = (int)enrollee.Id;

                // update the status to 'Submitted'
                await service.CreateEnrolmentStatusAsync(enrolleeId, new Status { Code = Status.SUBMITTED_CODE, Name = "Submitted" });
                enrollee = await service.GetEnrolleeAsync(enrolleeId);

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Enrollee>(HttpMethod.Put,
                 $"/api/enrollees/{enrolleeId}", enrollee.UserId, enrollee);

                // call the controller to update the enrollee
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("Enrollee can not be updated when the current status is not 'In Progress'.", body);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testUpdateEnrollee_403_Forbidden()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to update
                Enrollee enrollee = enrollees.First();
                int enrolleeId = (int)enrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Enrollee>(HttpMethod.Put,
                 $"/api/enrollees/{enrolleeId}", Guid.NewGuid(), enrollee);

                // call the controller to update the enrollee with a different userId
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testGetAvailableEnrolmentStatuses()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to get
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get,
                 $"/api/enrollees/{expectedEnrolleeId}/availableStatuses", expectedEnrollee.UserId);

                // try to get the available enrollee statuses
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the statuses were returned
                var statuses = (await TestUtils.DeserializeResponse<ApiOkResponse<IEnumerable<Status>>>(response)).Result;
                Assert.NotNull(statuses);
                Assert.Single(statuses);
                Assert.Contains(new Status { Code = Status.SUBMITTED_CODE }, statuses);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testGetAvailableEnrolmentStatuses_404_NotFound()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // get an enrollee id that does not exist
                int notFoundEnrolleeId = EnrolleeServiceMock.MAX_ENROLLEE_ID + 1;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get,
                 $"/api/enrollees/{notFoundEnrolleeId}/availableStatuses", Guid.NewGuid());

                // try to get an enrollee that does not exist
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testGetAvailableEnrolmentStatuses_403_Forbidden()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to get
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get,
                $"/api/enrollees/{expectedEnrolleeId}/availableStatuses", Guid.NewGuid());

                // try to get the available enrollee statuses with a different userId
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testGetEnrolmentStatuses()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to get
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get,
                 $"/api/enrollees/{expectedEnrolleeId}/statuses", expectedEnrollee.UserId);

                // try to get the enrollee statuses
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the enrollee statuses were returned
                var enrolleeStatuses = (await TestUtils.DeserializeResponse<ApiOkResponse<IEnumerable<EnrolmentStatus>>>(response)).Result;
                Assert.NotNull(enrolleeStatuses);
                Assert.Single(enrolleeStatuses);
                Assert.Equal(Status.IN_PROGRESS_CODE, enrolleeStatuses.First().StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testGetEnrolmentStatuses_404_NotFound()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // get an enrollee id that does not exist
                int notFoundEnrolleeId = EnrolleeServiceMock.MAX_ENROLLEE_ID + 1;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get,
                 $"/api/enrollees/{notFoundEnrolleeId}/statuses", Guid.NewGuid());

                // try to get an enrollee that does not exist
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testGetEnrolmentStatuses_403_Forbidden()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to get
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get,
                 $"/api/enrollees/{expectedEnrolleeId}/statuses", Guid.NewGuid());

                // try to get the enrollee statuses with a different userId
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testCreateEnrolmentStatuses()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to get
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Status>(HttpMethod.Post,
                 $"/api/enrollees/{expectedEnrolleeId}/statuses", expectedEnrollee.UserId, new Status { Code = Status.SUBMITTED_CODE });

                // try to create a new enrollee status
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the statuses were returned
                var enrolleeStatus = (await TestUtils.DeserializeResponse<ApiOkResponse<EnrolmentStatus>>(response)).Result;
                Assert.NotNull(enrolleeStatus);
                Assert.Equal(Status.SUBMITTED_CODE, enrolleeStatus.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testCreateEnrolmentStatuses_404_NotFound()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // try to get an enrollee that does not exist
                int notFoundEnrolleeId = EnrolleeServiceMock.MAX_ENROLLEE_ID + 1;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Status>(HttpMethod.Post,
                 $"/api/enrollees/{notFoundEnrolleeId}/statuses", Guid.NewGuid(), new Status { Code = Status.SUBMITTED_CODE });

                // try to get an enrollee that does not exist
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testCreateEnrolleeStatuses_400_BadRequest_Empty_StatusCode()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to get
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Status>(HttpMethod.Post,
                 $"/api/enrollees/{expectedEnrolleeId}/statuses", expectedEnrollee.UserId, new Status { Name = "No Code" });

                // try to create a new enrollee status
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("Status Code is required to create statuses.", body);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testCreateEnrolmentStatuses_400_BadRequest_Invalid_StatusCode()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to get
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Status>(HttpMethod.Post,
                 $"/api/enrollees/{expectedEnrolleeId}/statuses", expectedEnrollee.UserId, new Status { Code = Status.APPROVED_CODE });

                // try to create a new enrolment status
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("Cannot change from current Status Code: " + Status.IN_PROGRESS_CODE + " to the new Status Code: " + Status.APPROVED_CODE, body);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }

        [Fact]
        public async void testCreateEnrolmentStatuses_403_Forbidden()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var service = scope.ServiceProvider.GetRequiredService<IEnrolleeService>();
                ((EnrolleeServiceMock)service).InitializeDb();

                // check the initial state
                var enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());

                // pick off an enrollee to get
                Enrollee expectedEnrollee = enrollees.First();
                int expectedEnrolleeId = (int)expectedEnrollee.Id;

                // create a request with an AUTH token
                var request = TestUtils.CreateRequest<Status>(HttpMethod.Post,
                 $"/api/enrollees/{expectedEnrolleeId}/statuses", Guid.NewGuid(), new Status { Code = Status.SUBMITTED_CODE });

                // try to create a new enrolment status with a different userId
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

                // make sure the same amount of enrollees exist
                enrollees = await service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
                Assert.Equal(EnrolleeServiceMock.DEFAULT_ENROLLEES_SIZE, enrollees.Count());
            }
        }
    }
}

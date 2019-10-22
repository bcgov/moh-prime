using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Mocks;
using PrimeTests.Utils;
using System;

namespace PrimeTests.Controllers
{
    public class EnrolmentsControllerTests : BaseControllerTests
    {
        private static EnrolmentSearchOptions EMPTY_ENROLMENT_SEARCH_OPTIONS = new EnrolmentSearchOptions();

        public EnrolmentsControllerTests(CustomWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [Fact]
        public async void testGetEnrolments()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(new EnrolmentSearchOptions());
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // try to get the enrolments
                var response = await _client.GetAsync("/api/enrolments");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the controller returned all of the enrolments
                var returnedEnrolments = (await TestUtils.DeserializeResponse<ApiOkResponse<IEnumerable<Enrolment>>>(response)).Result;
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, returnedEnrolments.Count());
            }
        }

        [Fact]
        public async void testGetEnrolments_WithAuthToken()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // create a request with an AUTH token
                var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolments");
                var _token = TestUtils.TokenBuilder()
                    .ForAudience("prime-web-api")
                    .ForSubject("1234567890")
                    .BuildToken();

                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

                // try to get the enrolments
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the controller returned all of the enrolments
                var returnedEnrolments = (await TestUtils.DeserializeResponse<ApiOkResponse<IEnumerable<Enrolment>>>(response)).Result;
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, returnedEnrolments.Count());
            }
        }

        [Fact]
        public async void testGetEnrolment()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to get
                Enrolment expectedEnrolment = enrolments.First();
                int expectedEnrolmentId = (int)expectedEnrolment.Id;

                // try to get the enrolment
                var response = await _client.GetAsync("/api/enrolments/" + expectedEnrolmentId);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the enrolment was returned
                var enrolment = (await TestUtils.DeserializeResponse<ApiOkResponse<Enrolment>>(response)).Result;
                Assert.NotNull(enrolment);
                Assert.Equal(expectedEnrolmentId, enrolment.Id);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testGetEnrolment_404_NotFound()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // try to get an enrolment that does not exist
                int notFoundEnrolmentId = EnrolmentServiceMock.MAX_ENROLMENT_ID + 1;
                var response = await _client.GetAsync("/api/enrolments/" + notFoundEnrolmentId);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testCreateEnrolment()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                //get references to the services
                var _dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();

                // make a new enrolment object
                var testEnrolment = TestUtils.EnrolmentFaker.Generate();

                // try to create the enrolment
                var response = await _client.PostAsJsonAsync("/api/enrolments", testEnrolment);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                // check that the body contains the Enrollee first name
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains(testEnrolment.Enrollee.FirstName, body);

                // check that the body contains the Enrollee UserId
                Enrolment createdEnrolment = JsonConvert.DeserializeObject<Enrolment>(body);
                Assert.Equal(testEnrolment.Enrollee.UserId, createdEnrolment.Enrollee.UserId);
            }
        }

        [Fact]
        public async void testDeleteEnrolment()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to delete
                Enrolment expectedEnrolment = enrolments.First();
                int expectedEnrolmentId = (int)expectedEnrolment.Id;

                // try to delete the enrolment
                var response = await _client.DeleteAsync("/api/enrolments/" + expectedEnrolmentId);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the enrolment was removed
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE - 1, enrolments.Count());
            }
        }

        [Fact]
        public async void testDeleteEnrolment_404_NotFound()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // try to delete a non-existing enrolment
                int notFoundEnrolmentId = EnrolmentServiceMock.MAX_ENROLMENT_ID + 1;
                var response = await _client.DeleteAsync("/api/enrolments/" + notFoundEnrolmentId);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testUpdateEnrolment()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to update
                Enrolment enrolment = enrolments.First();
                int enrolmentId = (int)enrolment.Id;
                string previousFirstName = enrolment.Enrollee.FirstName;
                string previousLastName = enrolment.Enrollee.LastName;
                string expectedFirstName = "NewFirstName";
                string expectedLastName = "NewLastName";

                // update the names
                enrolment.Enrollee.FirstName = expectedFirstName;
                enrolment.Enrollee.LastName = expectedLastName;

                // call the controller to update the enrolment
                var response = await _client.PutAsJsonAsync("/api/enrolments/" + enrolmentId, enrolment);
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // check the updated enrolment in the database
                var updatedEnrolment = await _service.GetEnrolmentAsync(enrolmentId);
                Assert.NotNull(updatedEnrolment);
                Assert.Equal(enrolmentId, updatedEnrolment.Id);
                Assert.Equal(expectedFirstName, updatedEnrolment.Enrollee.FirstName);
                Assert.Equal(expectedLastName, updatedEnrolment.Enrollee.LastName);
            }
        }

        [Fact]
        public async void testUpdateEnrolment_400_BadRequest_Empty_UserId()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to update
                Enrolment enrolment = enrolments.First();
                int enrolmentId = (int)enrolment.Id;

                // put in an invalid userId
                enrolment.Enrollee.UserId = Guid.Empty;

                // call the controller to update the enrolment
                var response = await _client.PutAsJsonAsync("/api/enrolments/" + enrolmentId, enrolment);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("UserId cannot be the empty value", body);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testUpdateEnrolment_400_BadRequest_Mismatched_EnrolmentId()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to update
                Enrolment enrolment = enrolments.First();
                int enrolmentId = (int)enrolment.Id;

                // put in a mismatched enrolmentId
                enrolment.Id = enrolmentId + 1;

                // call the controller to update the enrolment
                var response = await _client.PutAsJsonAsync("/api/enrolments/" + enrolmentId, enrolment);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("Enrolment Id does not match with the payload.", body);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testUpdateEnrolment_400_BadRequest_Missing_EnrolleeId()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to update
                Enrolment enrolment = enrolments.First();
                int enrolmentId = (int)enrolment.Id;

                // remove the enrolleeId
                enrolment.Enrollee.Id = null;

                // call the controller to update the enrolment
                var response = await _client.PutAsJsonAsync("/api/enrolments/" + enrolmentId, enrolment);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("Enrollee Id is required to make updates.", body);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testUpdateEnrolment_404_NotFound()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to update
                Enrolment enrolment = enrolments.First();

                // change the enrolmentId to one that will not be found
                int notFoundEnrolmentId = EnrolmentServiceMock.MAX_ENROLMENT_ID + 1;
                enrolment.Id = notFoundEnrolmentId;

                // call the controller to update the enrolment
                var response = await _client.PutAsJsonAsync("/api/enrolments/" + notFoundEnrolmentId, enrolment);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("Enrolment not found with id " + notFoundEnrolmentId, body);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testUpdateEnrolment_400_BadRequest_WrongEnrolmentStatus()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to update
                Enrolment enrolment = enrolments.First();
                int enrolmentId = (int)enrolment.Id;

                // update the status to 'Submitted'
                await _service.CreateEnrolmentStatusAsync(enrolmentId, new Status { Code = Status.SUBMITTED_CODE, Name = "Submitted" });
                enrolment = await _service.GetEnrolmentAsync(enrolmentId);

                // call the controller to update the enrolment
                var response = await _client.PutAsJsonAsync("/api/enrolments/" + enrolmentId, enrolment);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("Enrolment can not be updated when the current status is not 'In Progress'.", body);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testGetAvailableEnrolmentStatuses()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to get
                Enrolment expectedEnrolment = enrolments.First();
                int expectedEnrolmentId = (int)expectedEnrolment.Id;

                // try to get the available enrolment statuses
                var response = await _client.GetAsync("/api/enrolments/" + expectedEnrolmentId + "/availableStatuses");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the statuses were returned
                var statuses = (await TestUtils.DeserializeResponse<ApiOkResponse<IEnumerable<Status>>>(response)).Result;
                Assert.NotNull(statuses);
                Assert.Single(statuses);
                Assert.Contains(new Status { Code = Status.SUBMITTED_CODE }, statuses);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testGetAvailableEnrolmentStatuses_404_NotFound()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // try to get an enrolment that does not exist
                int notFoundEnrolmentId = EnrolmentServiceMock.MAX_ENROLMENT_ID + 1;
                var response = await _client.GetAsync("/api/enrolments/" + notFoundEnrolmentId + "/availableStatuses");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testGetEnrolmentStatuses()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to get
                Enrolment expectedEnrolment = enrolments.First();
                int expectedEnrolmentId = (int)expectedEnrolment.Id;

                // try to get the enrolment statuses
                var response = await _client.GetAsync("/api/enrolments/" + expectedEnrolmentId + "/statuses");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the enrolment statuses were returned
                var enrolmentStatuses = (await TestUtils.DeserializeResponse<ApiOkResponse<IEnumerable<EnrolmentStatus>>>(response)).Result;
                Assert.NotNull(enrolmentStatuses);
                Assert.Single(enrolmentStatuses);
                Assert.Equal(Status.IN_PROGRESS_CODE, enrolmentStatuses.First().StatusCode);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testGetEnrolmentStatuses_404_NotFound()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // try to get an enrolment that does not exist
                int notFoundEnrolmentId = EnrolmentServiceMock.MAX_ENROLMENT_ID + 1;
                var response = await _client.GetAsync("/api/enrolments/" + notFoundEnrolmentId + "/statuses");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testCreateEnrolmentStatuses()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to get
                Enrolment expectedEnrolment = enrolments.First();
                int expectedEnrolmentId = (int)expectedEnrolment.Id;

                // try to create a new enrolment status
                var response = await _client.PostAsJsonAsync("/api/enrolments/" + expectedEnrolmentId + "/statuses", new Status { Code = Status.SUBMITTED_CODE });
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // check that the statuses were returned
                var enrolmentStatus = (await TestUtils.DeserializeResponse<ApiOkResponse<EnrolmentStatus>>(response)).Result;
                Assert.NotNull(enrolmentStatus);
                Assert.Equal(Status.SUBMITTED_CODE, enrolmentStatus.StatusCode);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testCreateEnrolmentStatuses_404_NotFound()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // try to get an enrolment that does not exist
                int notFoundEnrolmentId = EnrolmentServiceMock.MAX_ENROLMENT_ID + 1;
                var response = await _client.PostAsJsonAsync("/api/enrolments/" + notFoundEnrolmentId + "/statuses", new Status { Code = Status.SUBMITTED_CODE });
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testCreateEnrolmentStatuses_400_BadRequest_Empty_StatusCode()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to get
                Enrolment expectedEnrolment = enrolments.First();
                int expectedEnrolmentId = (int)expectedEnrolment.Id;

                // try to create a new enrolment status
                var response = await _client.PostAsJsonAsync("/api/enrolments/" + expectedEnrolmentId + "/statuses", new Status { Name = "No Code" });
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("Status Code is required to create statuses.", body);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }

        [Fact]
        public async void testCreateEnrolmentStatuses_400_BadRequest_Invalid_StatusCode()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // initialize the data
                var _service = scope.ServiceProvider.GetRequiredService<IEnrolmentService>();
                ((EnrolmentServiceMock)_service).InitializeDb();

                // check the initial state
                var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());

                // pick off an enrolment to get
                Enrolment expectedEnrolment = enrolments.First();
                int expectedEnrolmentId = (int)expectedEnrolment.Id;

                // try to create a new enrolment status
                var response = await _client.PostAsJsonAsync("/api/enrolments/" + expectedEnrolmentId + "/statuses", new Status { Code = Status.APPROVED_CODE });
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                // check for the expected error messages
                var body = await response.Content.ReadAsStringAsync();
                Assert.Contains("Cannot change from current Status Code: " + Status.IN_PROGRESS_CODE + " to the new Status Code: " + Status.APPROVED_CODE, body);

                // make sure the same amount of enrolments exist
                enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
                Assert.Equal(EnrolmentServiceMock.DEFAULT_ENROLMENTS_SIZE, enrolments.Count());
            }
        }
    }
}
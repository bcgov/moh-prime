using System;
using System.Linq;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

using Newtonsoft.Json;

using Xunit;

using Prime.Controllers;
using Prime.Models;
using Prime.Services;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using System.Net;
using System.Threading.Tasks;

using Bogus;
using Prime;
using PrimeTests.Utils;

using System.Net.Http;
using System.Net.Http.Headers;

using PrimeTests.Utils.Auth;

namespace PrimeTests.Controllers
{
    public class EnrolmentControllerTests : IDisposable
    {
        private string _databaseName;
        private ApiDbContext _dbContext;
        private TestAuthenticationContext _context;

        public EnrolmentControllerTests()
        {
            _databaseName = Guid.NewGuid().ToString();

            // var options = new DbContextOptionsBuilder<ApiDbContext>()
            //             .UseInMemoryDatabase(databaseName: _databaseName)
            //           .Options;

            // _dbContext = new ApiDbContext(options);

            _context = new TestAuthenticationContext(_databaseName);

            _dbContext = _context.ApiDbContext;
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Fact]
        public async Task testGetEnrolments()
        {
            var response = await _context.Client.GetAsync("/api/enrolments");
            var body = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task testGetEnrolments2()
        {
            // TestAuthenticationContext testContext = new TestAuthenticationContext();
            var testEnrolment = _context.CreateEnrolment();

            var response = await _context.Client.GetAsync("/api/enrolments");
            var body = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task testGetEnrolments3()
        {
            // TestAuthenticationContext testContext = new TestAuthenticationContext();
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolements");
            var _token = _context.TokenBuilder
                .ForAudience("prime-web-api")
                .ForSubject("1234567890")
                .BuildToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

            var _response = await _context.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
        }

        [Fact]
        public async Task<Enrolment> createEnrolment()
        {
            // TestAuthenticationContext testContext = new TestAuthenticationContext();
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();

            var response = await _context.Client.PostAsJsonAsync("/api/enrolements", testEnrolment);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            Assert.Contains(testEnrolment.Enrollee.FirstName, body);

            Enrolment createdEnrolment = JsonConvert.DeserializeObject<Enrolment>(body);
            Assert.Equal(testEnrolment.Enrollee.UserId, createdEnrolment.Enrollee.UserId);

            //pull the id off the location URI, and stick into the created enrolment
            var enrolmentId = response.Headers.Location.Segments.Last();
            createdEnrolment.Id = int.Parse(enrolmentId);

            return createdEnrolment;
        }

        [Fact]
        public async Task getSingleEnrolment()
        {
            // TestAuthenticationContext testContext = new TestAuthenticationContext();

            Enrolment enrolment = _context.CreateEnrolment();
            string expectedUserId = enrolment.Enrollee.UserId;

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolements/" + enrolment.Id);
            var _token = _context.TokenBuilder
                .ForAudience("prime-web-api")
                .ForSubject(expectedUserId)
                .WithClaim(ClaimTypes.Role, PrimeConstants.PRIME_ENROLMENT_ROLE)
                .BuildToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

            // Get enrolment by Id
            var response = await _context.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            Enrolment responseEnrolment = JsonConvert.DeserializeObject<Enrolment>(body);
            Assert.Equal(expectedUserId, responseEnrolment.Enrollee.UserId);
        }

        [Fact]
        public async void getAllEnrolments()
        {
            // TestAuthenticationContext testContext = new TestAuthenticationContext();

            _context.CreateEnrolment();
            _context.CreateEnrolment();
            _context.CreateEnrolment();

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/enrolements");
            var _token = _context.TokenBuilder
                .ForAudience("prime-web-api")
                .ForSubject("admin")
                .WithClaim(ClaimTypes.Role, PrimeConstants.PRIME_ADMIN_ROLE)
                .BuildToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

            // Get all enrolments for ADMIN user
            var response = await _context.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            Enrolment[] responseEnrolments = JsonConvert.DeserializeObject<Enrolment[]>(body);
            Assert.True(responseEnrolments.Count() > 2);
        }

        [Fact]
        public async void updateSingleEnrolment()
        {
            // TestAuthenticationContext testContext = new TestAuthenticationContext();
            Enrolment enrolment = _context.CreateEnrolment();
            string expectedFirstName = "NewFirstName";
            int enrolmentId = (int)enrolment.Id;

            Assert.NotEqual(expectedFirstName, enrolment.Enrollee.FirstName);

            //update the first name
            enrolment.Enrollee.FirstName = expectedFirstName;

            var response = await _context.Client.PutAsJsonAsync("/api/enrolements/" + enrolmentId, enrolment);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            Enrolment updatedEnrolment = _context.GetEnrolmentById(enrolmentId);
            Assert.Equal(expectedFirstName, updatedEnrolment.Enrollee.FirstName);
        }
    }
}

using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

using Xunit;
using Moq;

using Prime.Controllers;
using Prime.Models;

namespace PrimeTests
{
    public class ApplicationControllerTests
    {
        [Fact]
        public void createApplication()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestApplicationController")
                .Options;

            using (var context = new ApiDbContext(options))
            {
                var service = new ApplicationController(context);
                var testApplication = new Application()
                {
                    ApplicantName = "Test Applicant",
                    ApplicantId = "CREATE_APPLICATION",
                    PharmacistRegistrationNumber = "1234",
                    AppliedDate = DateTime.Now
                };

                service.Post(testApplication);
                context.SaveChanges();
            }

            using (var context = new ApiDbContext(options))
            {
                Assert.Equal(true, context.Application.Any(x => x.ApplicantId == "CREATE_APPLICATION"));
            }
        }

        [Fact]
        public void getSingleApplication()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestApplicationController")
                .Options;

            using (var context = new ApiDbContext(options))
            {
                var service = new ApplicationController(context);
                var reqApplication = new Application()
                {
                    ApplicantName = "Test Applicant",
                    ApplicantId = "GET_SINGLE_APPLICATION",
                    PharmacistRegistrationNumber = "1234",
                    AppliedDate = DateTime.Now
                };

                service.Post(reqApplication);
                context.SaveChanges();
            }

            using (var context = new ApiDbContext(options))
            {
                var service = new ApplicationController(context);
                var resApplicationResult = service.Get(1);

                var resApplication = resApplicationResult.Result.Value;

                Assert.Equal("GET_SINGLE_APPLICATION", resApplication.ApplicantId);
            }
        }
    }
}

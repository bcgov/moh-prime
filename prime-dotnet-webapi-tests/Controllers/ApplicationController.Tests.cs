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
                Assert.True(context.Application.Any(x => x.ApplicantId == "CREATE_APPLICATION"));
            }
        }

        [Fact]
        public void getSingleApplication()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestApplicationController")
                .Options;

            // Add application.
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

            int ApplicationId;

            // Get application to find inserted Id
            using (var context = new ApiDbContext(options))
            {
                var service = new ApplicationController(context);
                var resApplicationResult = service.Get();
                ApplicationId = (int)resApplicationResult.Result.Value.Where(x => x.ApplicantId == "GET_SINGLE_APPLICATION").First().Id;
            }

            // Get application by Id
            using (var context = new ApiDbContext(options))
            {
                var service = new ApplicationController(context);
                var resApplicationResult = service.Get(ApplicationId);

                var resApplication = resApplicationResult.Result.Value;

                Assert.Equal("GET_SINGLE_APPLICATION", resApplication.ApplicantId);
            }
        }

        [Fact]
        public void getAllApplications()
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
                    ApplicantId = "GET_ALL_APPLICATIONS",
                    PharmacistRegistrationNumber = "1234",
                    AppliedDate = DateTime.Now
                };
                var reqApplication2 = new Application()
                {
                    ApplicantName = "Test Applicant",
                    ApplicantId = "GET_ALL_APPLICATIONS2",
                    PharmacistRegistrationNumber = "1234",
                    AppliedDate = DateTime.Now
                };

                service.Post(reqApplication);
                service.Post(reqApplication2);
                context.SaveChanges();
            }

            using (var context = new ApiDbContext(options))
            {
                var service = new ApplicationController(context);
                var resApplicationResult = service.Get();

                var resApplications = resApplicationResult.Result.Value;

                Assert.True(resApplications.Count() >= 2);
            }
        }

        [Fact]
        public void updateSingleApplication()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestApplicationController")
                .Options;

            // Add application.
            using (var context = new ApiDbContext(options))
            {
                var service = new ApplicationController(context);
                var reqApplication = new Application()
                {
                    ApplicantName = "Test Applicant",
                    ApplicantId = "UPDATE_SINGLE_APPLICATION",
                    PharmacistRegistrationNumber = "1234",
                    AppliedDate = DateTime.Now
                };

                service.Post(reqApplication);
                context.SaveChanges();
            }

            Application application;

            // Get application 
            using (var context = new ApiDbContext(options))
            {
                var service = new ApplicationController(context);
                var resApplicationResult = service.Get();
                application = (Application)resApplicationResult.Result.Value.Where(x => x.ApplicantId == "UPDATE_SINGLE_APPLICATION").First();
            }

            // Update application by Id
            using (var context = new ApiDbContext(options))
            {
                var service = new ApplicationController(context);
                application.PharmacistRegistrationNumber = "9999";
                service.Put((int)application.Id, application);
            }

            // Get application by Id
            using (var context = new ApiDbContext(options))
            {
                var service = new ApplicationController(context);
                var resApplicationResult = service.Get((int)application.Id);
                var resApplication = resApplicationResult.Result.Value;

                Assert.Equal("9999", resApplication.PharmacistRegistrationNumber);
            }
        }
    }
}

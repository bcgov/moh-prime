using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.HttpClients;
using PrimeTests.Utils;
using PrimeTests.ModelFactories;
using AutoMapper;

namespace PrimeTests.UnitTests
{
    public class EnrolleeServiceTests : InMemoryDbTest
    {
        [Fact]
        public async void TestGetActiveGpid_HappyPath()
        {
            // Arrange
            var service = MockDependenciesFor<EnrolleeService>();
            var enrollee = TestDb.HasAnEnrollee("default,status.editable");
            enrollee.GPID = "expectedgpid";
            TestDb.SaveChanges();

            // Act
            var response = await service.GetActiveGpidAsync(enrollee.UserId);

            // Assert
            Assert.Equal(enrollee.GPID, response);
        }

        [Fact]
        public async void TestGetActiveGpid_Declined()
        {
            // Arrange
            var service = MockDependenciesFor<EnrolleeService>();
            var enrollee = TestDb.HasAnEnrollee("default,status.editable");
            enrollee.GPID = "notexpectedgpid";
            enrollee.AddEnrolmentStatus(StatusType.Declined);
            TestDb.SaveChanges();

            // Act
            var response = await service.GetActiveGpidAsync(enrollee.UserId);

            // Assert
            Assert.Null(response);
        }


        [Fact]
        public async void TestGpidValidation_NoParams()
        {
            // Arrange
            var service = MockDependenciesFor<EnrolleeService>();
            var enrollee = TestDb.HasAnEnrollee("default,status.editable");
            var request = new GpidValidationParameters();

            // Act
            var response = await service.ValidateProvisionerDataAsync(enrollee.GPID, request);

            // Assert
            Assert.NotNull(response);
            foreach (var property in typeof(GpidValidationParameters).GetProperties())
            {
                var responseProp = typeof(GpidValidationResponse).GetProperty(property.Name);
                Assert.NotNull(responseProp);
                Assert.Null(responseProp.GetValue(response));
            }
        }

        [Fact]
        public async void TestGpidValidation_MatchesEmail()
        {
            // Arrange
            var service = MockDependenciesFor<EnrolleeService>();
            var enrollee = TestDb.HasAnEnrollee("default,status.editable");
            var request = new GpidValidationParameters()
            {
                Email = enrollee.Email
            };

            // Act
            var response = await service.ValidateProvisionerDataAsync(enrollee.GPID, request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.AllPropertiesNullExcept(nameof(response.Email)));
            Assert.Equal(GpidValidationResponse.MatchText, response.Email);
        }

        [Fact]
        public async void TestGpidValidation_MatchesPrefferredName()
        {
            // Arrange
            var service = MockDependenciesFor<EnrolleeService>();
            var enrollee = TestDb.HasAnEnrollee("default,status.editable");
            enrollee.PreferredFirstName = enrollee.FirstName + "extracharacters";
            var request = new GpidValidationParameters()
            {
                FirstName = enrollee.PreferredFirstName
            };

            // Act
            var response = await service.ValidateProvisionerDataAsync(enrollee.GPID, request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.AllPropertiesNullExcept(nameof(response.FirstName)));
            Assert.Equal(GpidValidationResponse.MatchText, response.FirstName);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void TestGpidValidation_Birthdate(bool requestMatches)
        {
            // Arrange
            var service = MockDependenciesFor<EnrolleeService>();
            var enrollee = TestDb.HasAnEnrollee("default,status.editable");
            var request = new GpidValidationParameters()
            {
                DateOfBirth = enrollee.DateOfBirth.AddDays(requestMatches ? 0 : 2)
            };
            var expectedText = requestMatches ? GpidValidationResponse.MatchText : GpidValidationResponse.NoMatchText;

            // Act
            var response = await service.ValidateProvisionerDataAsync(enrollee.GPID, request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.AllPropertiesNullExcept(nameof(response.DateOfBirth)));
            Assert.Equal(expectedText, response.DateOfBirth);
        }

        [Fact]
        public async void TestGpidValidation_NoMatch()
        {
            // Arrange
            var service = MockDependenciesFor<EnrolleeService>();
            var enrollee = TestDb.HasAnEnrollee("default,status.editable");
            enrollee.SmsPhone = null;
            var request = new GpidValidationParameters()
            {
                MobilePhone = "1-800-COOL-ENROLLEE"
            };

            // Act
            var response = await service.ValidateProvisionerDataAsync(enrollee.GPID, request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.AllPropertiesNullExcept(nameof(response.MobilePhone)));
            Assert.Equal(GpidValidationResponse.MissingText, response.MobilePhone);
        }

        [Fact]
        public async void TestGpidValidation_MultiMatch()
        {
            // Arrange
            var service = MockDependenciesFor<EnrolleeService>();
            var enrollee = TestDb.HasAnEnrollee("default,status.editable");
            enrollee.PhoneExtension = "123";
            var request = new GpidValidationParameters()
            {
                LastName = enrollee.LastName,
                Phone = enrollee.Phone,
                PhoneExtension = enrollee.PhoneExtension + "6"
            };

            // Act
            var response = await service.ValidateProvisionerDataAsync(enrollee.GPID, request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.AllPropertiesNullExcept(
                nameof(response.LastName),
                nameof(response.Phone),
                nameof(response.PhoneExtension)));
            Assert.Equal(GpidValidationResponse.MatchText, response.LastName);
            Assert.Equal(GpidValidationResponse.MatchText, response.Phone);
            Assert.Equal(GpidValidationResponse.NoMatchText, response.PhoneExtension);
        }

        [Fact]
        public async void TestGpidValidation_Certifications()
        {
            // Arrange
            var service = MockDependenciesFor<EnrolleeService>();
            var enrollee = TestDb.HasAnEnrollee("default,status.editable");
            enrollee.Certifications = new Certification[]
            {
                new Certification
                {
                    License = new License
                    {
                        Prefix = "91"
                    },
                    LicenseNumber = "11111"
                },
                new Certification
                {
                    License = new License
                    {
                        Prefix = "P1"
                    },
                    LicenseNumber = "22222"
                },
                new Certification
                {
                    License = new License
                    {
                        Prefix = "96"
                    },
                    LicenseNumber = "33333"
                }
            };

            var matchingRecord = new GpidValidationParameters.CollegeRecord
            {
                CollegeName = "P1",
                CollegeId = "22222"
            };
            var nonMatchingRecord = new GpidValidationParameters.CollegeRecord
            {
                CollegeName = "96",
                CollegeId = "77777"
            };
            var request = new GpidValidationParameters()
            {
                CollegeRecords = new[] { matchingRecord, nonMatchingRecord }
            };

            // Act
            var response = await service.ValidateProvisionerDataAsync(enrollee.GPID, request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.AllPropertiesNullExcept(nameof(response.CollegeRecords)));
            Assert.Equal(request.CollegeRecords.Count(), response.CollegeRecords.Count());

            var matchingResponse = response.CollegeRecords.Single(c => c.CollegeName == matchingRecord.CollegeName && c.CollegeId == matchingRecord.CollegeId);
            Assert.Equal(GpidValidationResponse.MatchText, matchingResponse.Match);

            var nonMatchingResponse = response.CollegeRecords.Single(c => c.CollegeName == nonMatchingRecord.CollegeName && c.CollegeId == nonMatchingRecord.CollegeId);
            Assert.Equal(GpidValidationResponse.NoMatchText, nonMatchingResponse.Match);
        }
    }
}

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

using SimpleBase;

using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;
using PrimeTests.Mocks;
using Prime.Models.Api;
using Prime.ViewModels;

namespace PrimeTests.Services
{
    public class EnrolleeServiceTests : BaseServiceTests<EnrolleeService>
    {
        private static EnrolleeSearchOptions EMPTY_ENROLLEE_SEARCH_OPTIONS = new EnrolleeSearchOptions();

        public EnrolleeServiceTests() : base(new object[] {
            new AutomaticAdjudicationServiceMock(),
            new EmailServiceMock(),
            new PrivilegeServiceMock(),
            new AccessTermServiceMock(),
            new EnrolleeProfileVersionServiceMock()
        })
        { }

        [Fact]
        public async void testGetEnrollees()
        {
            // make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create some enrollees directly to the context
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());

            await _dbContext.SaveChangesAsync();

            // get the enrollees through the service layer code
            var enrollees = await _service.GetEnrolleesAsync(EMPTY_ENROLLEE_SEARCH_OPTIONS);
            Assert.NotNull(enrollees);
            Assert.Equal(3, enrollees.Count());
        }

        [Fact]
        public async void testGetEnrolleeForUserId()
        {
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();
            Guid expectedUserId = testEnrollee.UserId;

            // create the enrollee directly to the context
            _dbContext.Enrollees.Add(testEnrollee);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolleeId = (int)testEnrollee.Id;

            // get the enrollee through the service layer code
            Enrollee enrollee = await _service.GetEnrolleeForUserIdAsync(expectedUserId);
            Assert.NotNull(enrollee);
            Assert.Equal(expectedEnrolleeId, enrollee.Id);
            Assert.Equal(expectedUserId, enrollee.UserId);
        }

        [Fact]
        public void testGuid_Ascii85_Encoding()
        {
            Guid guid = Guid.NewGuid();
            Assert.NotEqual(Guid.Empty, guid);

            string encodedString = Base85.Ascii85.Encode(guid.ToByteArray());
            Assert.NotNull(encodedString);
            Assert.Equal(20, encodedString.Length);

            Span<byte> decodedBytes = Base85.Ascii85.Decode(encodedString);
            Guid decodedGuid = new Guid(decodedBytes);
            Assert.NotEqual(Guid.Empty, decodedGuid);
            Assert.Equal(guid, decodedGuid);
        }

        [Fact]
        public async void testEnrolleeExists()
        {
            // make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());

            // create the enrollee directly to the context
            var enrollee = TestUtils.EnrolleeFaker.Generate();

            _dbContext.Enrollees.Add(enrollee);
            await _dbContext.SaveChangesAsync();

            //make sure there are now enrollees
            Assert.True(_dbContext.Enrollees.Any());

            // get the created enrollee id
            int expectedEnrolleeId = (int)enrollee.Id;

            // check to see if the enrollee exists through the service layer
            Assert.True(await _service.EnrolleeExistsAsync(expectedEnrolleeId));
        }

        [Fact]
        public async void testCreateEnrollee()
        {
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();
            // remove the enrollee status that the faker created, as it should get created by the service layer
            testEnrollee.EnrolmentStatuses.Clear();
            Guid expectedUserId = testEnrollee.UserId;

            // create the enrollee through the service layer code
            int expectedEnrolleeId = (int)await _service.CreateEnrolleeAsync(testEnrollee);

            // check that the enrollee save actually saved to the context
            Enrollee enrollee = _dbContext.Enrollees.Include(e => e.Certifications).Where(e => e.Id == expectedEnrolleeId).SingleOrDefault();
            Assert.NotNull(enrollee);
            Assert.Equal(expectedEnrolleeId, enrollee.Id);
            Assert.Equal(expectedUserId, enrollee.UserId);
        }

        [Fact]
        public async void testGetEnrollee()
        {
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();
            Guid expectedUserId = testEnrollee.UserId;

            // create the enrollee directly to the context
            _dbContext.Enrollees.Add(testEnrollee);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolleeId = (int)testEnrollee.Id;

            // get the enrollee through the service layer code
            Enrollee enrollee = await _service.GetEnrolleeAsync((int)expectedEnrolleeId);
            Assert.NotNull(enrollee);
            Assert.Equal(expectedEnrolleeId, enrollee.Id);
            Assert.Equal(expectedUserId, enrollee.UserId);
        }

        [Fact]
        public async void testGetEnrollees_Filtered()
        {
            // make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create some enrollees directly to the context
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());

            await _dbContext.SaveChangesAsync();

            // get the enrollees through the service layer code
            var enrolleesActive = await _service.GetEnrolleesAsync(new EnrolleeSearchOptions { StatusCode = Status.ACTIVE_CODE });
            Assert.NotNull(enrolleesActive);
            Assert.Equal(3, enrolleesActive.Count());

            // get the enrollees through the service layer code
            var enrolleesUnderReview = await _service.GetEnrolleesAsync(new EnrolleeSearchOptions { StatusCode = Status.UNDER_REVIEW_CODE });
            Assert.NotNull(enrolleesUnderReview);
            Assert.Empty(enrolleesUnderReview);
        }

        [Fact]
        public async void testUpdateEnrollee()
        {
            string expectedName = "ChangedName";
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();

            testEnrollee.FirstName = "StartingName";

            // create the enrollee directly to the context
            _dbContext.Enrollees.Add(testEnrollee);
            await _dbContext.SaveChangesAsync();
            int enrolleeId = (int)testEnrollee.Id;


            // get the enrollee directly from the context
            Enrollee enrollee = await _dbContext.Enrollees
                            .Include(e => e.PhysicalAddress)
                            .Include(e => e.MailingAddress)
                            .Include(e => e.Certifications)
                            .Include(e => e.Jobs)
                            .Include(e => e.Organizations)
                            .AsNoTracking()
                            .Where(e => e.Id == enrolleeId)
                            .SingleOrDefaultAsync();
            Assert.NotNull(enrollee);

            // make sure we are not tracking anything - i.e. isolate following transaction
            TestUtils.DetachAllEntities(_dbContext);




            // update the enrollee through the service layer code
            EnrolleeProfileViewModel enrolleeProfile = new EnrolleeProfileViewModel
            {
                PreferredFirstName = expectedName
            };

            int updated = await _service.UpdateEnrolleeAsync(enrolleeId, enrolleeProfile);
            Assert.True(updated > 0);

            // get the updated enrollee directly from the context
            Enrollee updatedEnrollee = await _dbContext.Enrollees.FindAsync(enrolleeId);
            Assert.NotNull(updatedEnrollee);
            Assert.Equal(enrolleeId, updatedEnrollee.Id);
            Assert.Equal(expectedName, updatedEnrollee.FirstName);
        }

        [Fact]
        public async void testDeleteEnrollee()
        {
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();
            Guid expectedUserId = testEnrollee.UserId;

            // create the enrollee directly to the context
            _dbContext.Enrollees.Add(testEnrollee);
            await _dbContext.SaveChangesAsync();
            int enrolleeId = (int)testEnrollee.Id;

            // check that the enrollee save actually saved to the context
            Enrollee enrollee = await _dbContext.Enrollees.FindAsync(enrolleeId);
            Assert.NotNull(enrollee);
            Assert.Equal(enrolleeId, enrollee.Id);
            Assert.Equal(expectedUserId, enrollee.UserId);

            // delete the enrollee through the service layer code
            await _service.DeleteEnrolleeAsync(enrolleeId);

            // get the updated enrollee through the service layer code
            Enrollee deletedEnrollee = await _dbContext.Enrollees.FindAsync(enrolleeId);
            Assert.Null(deletedEnrollee);
        }

        [Fact]
        public async void testGetAvailableEnrolmentStatuses()
        {
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();
            Guid expectedUserId = testEnrollee.UserId;

            // create the enrollee directly to the context
            _dbContext.Enrollees.Add(testEnrollee);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolleeId = (int)testEnrollee.Id;

            // get the available statuses through the service layer code
            var statuses = await _service.GetAvailableEnrolmentStatusesAsync((int)expectedEnrolleeId);
            Assert.NotNull(statuses);
            Assert.Single(statuses);
            Assert.Contains(_dbContext.Statuses.Single(s => s.Code == Status.UNDER_REVIEW_CODE), statuses);
        }

        [Fact]
        public async void testGetEnrolmentStatuses()
        {
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();
            Guid expectedUserId = testEnrollee.UserId;

            // create the enrollee directly to the context
            _dbContext.Enrollees.Add(testEnrollee);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolleeId = (int)testEnrollee.Id;

            // get the enrolment statuses through the service layer code
            var enrolmentStatuses = await _service.GetEnrolmentStatusesAsync((int)expectedEnrolleeId);
            Assert.NotNull(enrolmentStatuses);
            Assert.Single(enrolmentStatuses);
            Assert.Equal(_dbContext.Statuses.Single(s => s.Code == Status.ACTIVE_CODE), enrolmentStatuses.First().Status);
        }

        [Fact]
        public async void testCreateEnrolmentStatus()
        {
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();
            Guid expectedUserId = testEnrollee.UserId;

            // create the enrollee directly to the context
            _dbContext.Enrollees.Add(testEnrollee);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolleeId = (int)testEnrollee.Id;

            // create the enrolment status through the service layer code
            var enrolmentStatus = await _service.CreateEnrolmentStatusAsync((int)expectedEnrolleeId, _dbContext.Statuses.Single(s => s.Code == Status.UNDER_REVIEW_CODE));
            Assert.NotNull(enrolmentStatus);
            Assert.Equal(_dbContext.Statuses.Single(s => s.Code == Status.UNDER_REVIEW_CODE), enrolmentStatus.Status);
        }


        [Fact]
        public async void testCreateEnrolmentStatus_Generate_LicensePlate()
        {
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();
            // manually change the status to approved
            testEnrollee.CurrentStatus.StatusCode = Status.REQUIRES_TOA_CODE;
            Guid expectedUserId = testEnrollee.UserId;

            // create the enrollee directly to the context
            _dbContext.Enrollees.Add(testEnrollee);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolleeId = (int)testEnrollee.Id;

            // create the enrolment status through the service layer code
            var enrolmentStatus = await _service.CreateEnrolmentStatusAsync((int)expectedEnrolleeId, _dbContext.Statuses.Single(s => s.Code == Status.ACTIVE_CODE));
            Assert.NotNull(enrolmentStatus);
            Assert.Equal(_dbContext.Statuses.Single(s => s.Code == Status.ACTIVE_CODE), enrolmentStatus.Status);

            // get the enrollee object, and check that there is a 20 character license plate
            var enrollee = _dbContext.Enrollees.Single(e => e.UserId == expectedUserId);
            Assert.NotNull(enrollee);
            Assert.NotNull(enrollee.GPID);
            Assert.Equal(20, enrollee.GPID.Length);
        }

        [Fact]
        public async void testCreateEnrolmentStatus_AcceptedTOS_to_Submitted()
        {
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();
            testEnrollee.CurrentStatus.StatusCode = Status.ACTIVE_CODE;

            // create the enrollee directly to the context
            _dbContext.Enrollees.Add(testEnrollee);
            await _dbContext.SaveChangesAsync();

            // create the enrolment status through the service layer code
            var enrolmentStatusInProgress = await _service
                .CreateEnrolmentStatusAsync(testEnrollee.Id.Value, _dbContext.Statuses
                .Single(s => s.Code == Status.UNDER_REVIEW_CODE));

            Assert.NotNull(enrolmentStatusInProgress);
            Assert.Equal(_dbContext.Statuses.Single(s => s.Code == Status.UNDER_REVIEW_CODE), enrolmentStatusInProgress.Status);
        }

        [Fact]
        public void IsStatusChangeAllowed()
        {
            Status ACTIVE = _dbContext.Statuses.Single(s => s.Code == Status.ACTIVE_CODE);
            Status UNDER_REVIEW = _dbContext.Statuses.Single(s => s.Code == Status.UNDER_REVIEW_CODE);
            Status REQUIRES_TOA = _dbContext.Statuses.Single(s => s.Code == Status.REQUIRES_TOA_CODE);
            Status LOCKED = _dbContext.Statuses.Single(s => s.Code == Status.LOCKED_CODE);

            Assert.True(_service.IsStatusChangeAllowed(null, ACTIVE));
            Assert.False(_service.IsStatusChangeAllowed(null, UNDER_REVIEW));
            Assert.False(_service.IsStatusChangeAllowed(null, REQUIRES_TOA));
            Assert.False(_service.IsStatusChangeAllowed(null, LOCKED));

            Assert.False(_service.IsStatusChangeAllowed(ACTIVE, null));
            Assert.False(_service.IsStatusChangeAllowed(ACTIVE, ACTIVE));
            Assert.True(_service.IsStatusChangeAllowed(ACTIVE, UNDER_REVIEW));
            Assert.False(_service.IsStatusChangeAllowed(ACTIVE, REQUIRES_TOA));
            Assert.True(_service.IsStatusChangeAllowed(ACTIVE, LOCKED));

            Assert.False(_service.IsStatusChangeAllowed(UNDER_REVIEW, null));
            Assert.False(_service.IsStatusChangeAllowed(UNDER_REVIEW, ACTIVE));
            Assert.False(_service.IsStatusChangeAllowed(UNDER_REVIEW, UNDER_REVIEW));
            Assert.True(_service.IsStatusChangeAllowed(UNDER_REVIEW, REQUIRES_TOA));
            Assert.True(_service.IsStatusChangeAllowed(UNDER_REVIEW, LOCKED));

            Assert.False(_service.IsStatusChangeAllowed(REQUIRES_TOA, null));
            Assert.True(_service.IsStatusChangeAllowed(REQUIRES_TOA, ACTIVE));
            Assert.False(_service.IsStatusChangeAllowed(REQUIRES_TOA, UNDER_REVIEW));
            Assert.False(_service.IsStatusChangeAllowed(REQUIRES_TOA, REQUIRES_TOA));
            Assert.True(_service.IsStatusChangeAllowed(REQUIRES_TOA, LOCKED));

            Assert.False(_service.IsStatusChangeAllowed(LOCKED, null));
            Assert.True(_service.IsStatusChangeAllowed(LOCKED, ACTIVE));
            Assert.False(_service.IsStatusChangeAllowed(LOCKED, UNDER_REVIEW));
            Assert.False(_service.IsStatusChangeAllowed(LOCKED, REQUIRES_TOA));
            Assert.False(_service.IsStatusChangeAllowed(LOCKED, LOCKED));
        }

        [Fact]
        public void IsStatusChangeAllowed_As_Admin()
        {
            Status ACTIVE = _dbContext.Statuses.Single(s => s.Code == Status.ACTIVE_CODE);
            Status UNDER_REVIEW = _dbContext.Statuses.Single(s => s.Code == Status.UNDER_REVIEW_CODE);
            Status REQUIRES_TOA = _dbContext.Statuses.Single(s => s.Code == Status.REQUIRES_TOA_CODE);
            Status LOCKED = _dbContext.Statuses.Single(s => s.Code == Status.LOCKED_CODE);

            // TODO This is dangerous, as it sets the admin flag for this entire context.
            // By default tests in the same test collection run sequentially but if they were run in parallel this could cause unintended side effects in other tests.
            // Also, Assertions throw errors and stop the current test, so the RemoveAdminRoleFromUser method will not be called if any assertions fail.
            TestUtils.AddAdminRoleToUser(_httpContext?.HttpContext?.User);

            Assert.True(_service.IsStatusChangeAllowed(null, ACTIVE));
            Assert.False(_service.IsStatusChangeAllowed(null, UNDER_REVIEW));
            Assert.False(_service.IsStatusChangeAllowed(null, REQUIRES_TOA));
            Assert.True(_service.IsStatusChangeAllowed(null, LOCKED));

            Assert.False(_service.IsStatusChangeAllowed(ACTIVE, null));
            Assert.False(_service.IsStatusChangeAllowed(ACTIVE, ACTIVE));
            Assert.True(_service.IsStatusChangeAllowed(ACTIVE, UNDER_REVIEW));
            Assert.False(_service.IsStatusChangeAllowed(ACTIVE, REQUIRES_TOA));
            Assert.True(_service.IsStatusChangeAllowed(ACTIVE, LOCKED));

            Assert.False(_service.IsStatusChangeAllowed(UNDER_REVIEW, null));
            Assert.True(_service.IsStatusChangeAllowed(UNDER_REVIEW, ACTIVE));
            Assert.False(_service.IsStatusChangeAllowed(UNDER_REVIEW, UNDER_REVIEW));
            Assert.True(_service.IsStatusChangeAllowed(UNDER_REVIEW, REQUIRES_TOA));
            Assert.True(_service.IsStatusChangeAllowed(UNDER_REVIEW, LOCKED));

            Assert.False(_service.IsStatusChangeAllowed(REQUIRES_TOA, null));
            Assert.True(_service.IsStatusChangeAllowed(REQUIRES_TOA, ACTIVE));
            Assert.False(_service.IsStatusChangeAllowed(REQUIRES_TOA, UNDER_REVIEW));
            Assert.False(_service.IsStatusChangeAllowed(REQUIRES_TOA, REQUIRES_TOA));
            Assert.True(_service.IsStatusChangeAllowed(REQUIRES_TOA, LOCKED)); ;

            Assert.False(_service.IsStatusChangeAllowed(LOCKED, null));
            Assert.True(_service.IsStatusChangeAllowed(LOCKED, ACTIVE));
            Assert.False(_service.IsStatusChangeAllowed(LOCKED, UNDER_REVIEW));
            Assert.False(_service.IsStatusChangeAllowed(LOCKED, REQUIRES_TOA));
            Assert.False(_service.IsStatusChangeAllowed(LOCKED, LOCKED));

            TestUtils.RemoveAdminRoleFromUser(_httpContext?.HttpContext?.User);
        }
    }
}

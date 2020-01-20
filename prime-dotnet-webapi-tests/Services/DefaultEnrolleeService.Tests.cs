using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

using SimpleBase;

using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;
using PrimeTests.Mocks;

namespace PrimeTests.Services
{
    public class DefaultEnrolleeServiceTests : BaseServiceTests<DefaultEnrolleeService>
    {
        private static EnrolleeSearchOptions EMPTY_ENROLLEE_SEARCH_OPTIONS = new EnrolleeSearchOptions();

        public DefaultEnrolleeServiceTests() : base(new object[] { new AutomaticAdjudicationServiceMock(), new EmailServiceMock(), new PrivilegeServiceMock(), new AccessTermServiceMock(), new EnrolleeProfileVersionServiceMock() })
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
            var enrolleesInProgress = await _service.GetEnrolleesAsync(new EnrolleeSearchOptions { StatusCode = Status.IN_PROGRESS_CODE });
            Assert.NotNull(enrolleesInProgress);
            Assert.Equal(3, enrolleesInProgress.Count());

            // get the enrollees through the service layer code
            var enrolleesSubmitted = await _service.GetEnrolleesAsync(new EnrolleeSearchOptions { StatusCode = Status.SUBMITTED_CODE });
            Assert.NotNull(enrolleesSubmitted);
            Assert.Empty(enrolleesSubmitted);
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
            enrollee.FirstName = expectedName;
            int updated = await _service.UpdateEnrolleeAsync(enrollee);
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
            Assert.Contains(_dbContext.Statuses.Single(s => s.Code == Status.SUBMITTED_CODE), statuses);
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
            Assert.Equal(_dbContext.Statuses.Single(s => s.Code == Status.IN_PROGRESS_CODE), enrolmentStatuses.First().Status);
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
            var enrolmentStatus = await _service.CreateEnrolmentStatusAsync((int)expectedEnrolleeId, _dbContext.Statuses.Single(s => s.Code == Status.SUBMITTED_CODE));
            Assert.NotNull(enrolmentStatus);
            Assert.Equal(_dbContext.Statuses.Single(s => s.Code == Status.SUBMITTED_CODE), enrolmentStatus.Status);
        }


        [Fact]
        public async void testCreateEnrolmentStatus_Generate_LicensePlate()
        {
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();
            // manually change the status to approved
            testEnrollee.CurrentStatus.StatusCode = Status.APPROVED_CODE;
            Guid expectedUserId = testEnrollee.UserId;

            // create the enrollee directly to the context
            _dbContext.Enrollees.Add(testEnrollee);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolleeId = (int)testEnrollee.Id;

            // create the enrolment status through the service layer code
            var enrolmentStatus = await _service.CreateEnrolmentStatusAsync((int)expectedEnrolleeId, _dbContext.Statuses.Single(s => s.Code == Status.ACCEPTED_TOS_CODE));
            Assert.NotNull(enrolmentStatus);
            Assert.Equal(_dbContext.Statuses.Single(s => s.Code == Status.ACCEPTED_TOS_CODE), enrolmentStatus.Status);

            // get the enrollee object, and check that there is a 20 character license plate
            var enrollee = _dbContext.Enrollees.Single(e => e.UserId == expectedUserId);
            Assert.NotNull(enrollee);
            Assert.NotNull(enrollee.LicensePlate);
            Assert.Equal(20, enrollee.LicensePlate.Length);
        }

        [Fact]
        public async void testCreateEnrolmentStatus_AcceptedTOS_to_Submitted()
        {
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();
            testEnrollee.CurrentStatus.StatusCode = Status.ACCEPTED_TOS_CODE;

            // create the enrollee directly to the context
            _dbContext.Enrollees.Add(testEnrollee);
            await _dbContext.SaveChangesAsync();

            // create the enrolment status through the service layer code
            var enrolmentStatusInProgress = await _service.CreateEnrolmentStatusAsync(testEnrollee.Id.Value, _dbContext.Statuses.Single(s => s.Code == Status.SUBMITTED_CODE));
            Assert.NotNull(enrolmentStatusInProgress);
            Assert.Equal(_dbContext.Statuses.Single(s => s.Code == Status.SUBMITTED_CODE), enrolmentStatusInProgress.Status);
        }

        [Fact]
        public void IsStatusChangeAllowed()
        {
            Status IN_PROGRESS = _dbContext.Statuses.Single(s => s.Code == Status.IN_PROGRESS_CODE);
            Status SUBMITTED = _dbContext.Statuses.Single(s => s.Code == Status.SUBMITTED_CODE);
            Status APPROVED = _dbContext.Statuses.Single(s => s.Code == Status.APPROVED_CODE);
            Status DECLINED = _dbContext.Statuses.Single(s => s.Code == Status.DECLINED_CODE);
            Status ACCEPTED_TOS = _dbContext.Statuses.Single(s => s.Code == Status.ACCEPTED_TOS_CODE);
            Status DECLINED_TOS = _dbContext.Statuses.Single(s => s.Code == Status.DECLINED_TOS_CODE);

            Assert.True(_service.IsStatusChangeAllowed(null, IN_PROGRESS));
            Assert.False(_service.IsStatusChangeAllowed(null, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(null, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(null, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(null, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(null, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(IN_PROGRESS, null));
            Assert.False(_service.IsStatusChangeAllowed(IN_PROGRESS, IN_PROGRESS));
            Assert.True(_service.IsStatusChangeAllowed(IN_PROGRESS, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(IN_PROGRESS, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(IN_PROGRESS, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(IN_PROGRESS, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(IN_PROGRESS, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(SUBMITTED, null));
            Assert.False(_service.IsStatusChangeAllowed(SUBMITTED, IN_PROGRESS));
            Assert.False(_service.IsStatusChangeAllowed(SUBMITTED, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(SUBMITTED, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(SUBMITTED, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(SUBMITTED, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(SUBMITTED, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(APPROVED, null));
            Assert.False(_service.IsStatusChangeAllowed(APPROVED, IN_PROGRESS));
            Assert.False(_service.IsStatusChangeAllowed(APPROVED, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(APPROVED, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(APPROVED, DECLINED));
            Assert.True(_service.IsStatusChangeAllowed(APPROVED, ACCEPTED_TOS));
            Assert.True(_service.IsStatusChangeAllowed(APPROVED, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(DECLINED, null));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, IN_PROGRESS));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, null));
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, IN_PROGRESS));
            Assert.True(_service.IsStatusChangeAllowed(ACCEPTED_TOS, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, null));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, IN_PROGRESS));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, DECLINED_TOS));
        }

        [Fact]
        public void IsStatusChangeAllowed_As_Admin()
        {
            Status IN_PROGRESS = _dbContext.Statuses.Single(s => s.Code == Status.IN_PROGRESS_CODE);
            Status SUBMITTED = _dbContext.Statuses.Single(s => s.Code == Status.SUBMITTED_CODE);
            Status APPROVED = _dbContext.Statuses.Single(s => s.Code == Status.APPROVED_CODE);
            Status DECLINED = _dbContext.Statuses.Single(s => s.Code == Status.DECLINED_CODE);
            Status ACCEPTED_TOS = _dbContext.Statuses.Single(s => s.Code == Status.ACCEPTED_TOS_CODE);
            Status DECLINED_TOS = _dbContext.Statuses.Single(s => s.Code == Status.DECLINED_TOS_CODE);

            // TODO This is dangerous, as it sets the admin flag for this entire context.
            // By default tests in the same test collection run sequentially but if they were run in parallel this could cause unintended side effects in other tests.
            // Also, Assertions throw errors and stop the current test, so the RemoveAdminRoleFromUser method will not be called if any assertions fail.
            TestUtils.AddAdminRoleToUser(_httpContext?.HttpContext?.User);

            Assert.True(_service.IsStatusChangeAllowed(null, IN_PROGRESS));
            Assert.False(_service.IsStatusChangeAllowed(null, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(null, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(null, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(null, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(null, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(IN_PROGRESS, null));
            Assert.False(_service.IsStatusChangeAllowed(IN_PROGRESS, IN_PROGRESS));
            Assert.True(_service.IsStatusChangeAllowed(IN_PROGRESS, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(IN_PROGRESS, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(IN_PROGRESS, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(IN_PROGRESS, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(IN_PROGRESS, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(SUBMITTED, null));
            Assert.True(_service.IsStatusChangeAllowed(SUBMITTED, IN_PROGRESS));
            Assert.False(_service.IsStatusChangeAllowed(SUBMITTED, SUBMITTED));
            Assert.True(_service.IsStatusChangeAllowed(SUBMITTED, APPROVED));
            Assert.True(_service.IsStatusChangeAllowed(SUBMITTED, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(SUBMITTED, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(SUBMITTED, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(APPROVED, null));
            Assert.False(_service.IsStatusChangeAllowed(APPROVED, IN_PROGRESS));
            Assert.False(_service.IsStatusChangeAllowed(APPROVED, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(APPROVED, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(APPROVED, DECLINED));
            Assert.True(_service.IsStatusChangeAllowed(APPROVED, ACCEPTED_TOS));
            Assert.True(_service.IsStatusChangeAllowed(APPROVED, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(DECLINED, null));
            Assert.True(_service.IsStatusChangeAllowed(DECLINED, IN_PROGRESS));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, null));
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, IN_PROGRESS));
            Assert.True(_service.IsStatusChangeAllowed(ACCEPTED_TOS, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, null));
            Assert.True(_service.IsStatusChangeAllowed(DECLINED_TOS, IN_PROGRESS));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED_TOS, DECLINED_TOS));

            TestUtils.RemoveAdminRoleFromUser(_httpContext?.HttpContext?.User);
        }
    }
}

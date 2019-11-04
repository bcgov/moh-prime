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
    public class DefaultEnrolmentServiceTests : BaseServiceTests<DefaultEnrolmentService>
    {
        private static EnrolmentSearchOptions EMPTY_ENROLMENT_SEARCH_OPTIONS = new EnrolmentSearchOptions();

        public DefaultEnrolmentServiceTests() : base(new object[] { new AutomaticAdjudicationServiceMock() })
        { }

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
        public async void testEnrolmentExists()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());

            // create the enrolment directly to the context
            var enrolment = TestUtils.EnrolmentFaker.Generate();

            _dbContext.Enrolments.Add(enrolment);
            await _dbContext.SaveChangesAsync();

            //make sure there are now enrolments
            Assert.True(_dbContext.Enrolments.Any());

            // get the created enrolment id
            int expectedEnrolmentId = (int)enrolment.Id;

            // check to see if the enrolment exists through the service layer
            Assert.True(_service.EnrolmentExists(expectedEnrolmentId));
        }

        [Fact]
        public async void testCreateEnrolment()
        {
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();
            // remove the enrolment status that the faker created, as it should get created by the service layer
            testEnrolment.EnrolmentStatuses.Clear();
            Guid expectedUserId = testEnrolment.Enrollee.UserId;

            // create the enrolment through the service layer code
            int expectedEnrolmentId = (int)await _service.CreateEnrolmentAsync(testEnrolment);

            // check that the enrolment save actually saved to the context
            Enrolment enrolment = _dbContext.Enrolments.Include(e => e.Enrollee).Include(e => e.Certifications).Where(e => e.Id == expectedEnrolmentId).SingleOrDefault();
            Assert.NotNull(enrolment);
            Assert.Equal(expectedEnrolmentId, enrolment.Id);
            Assert.Equal(expectedUserId, enrolment.Enrollee.UserId);
        }

        [Fact]
        public async void testGetEnrolment()
        {
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();
            Guid expectedUserId = testEnrolment.Enrollee.UserId;

            // create the enrolment directly to the context
            _dbContext.Enrolments.Add(testEnrolment);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolmentId = (int)testEnrolment.Id;

            // get the enrolment through the service layer code
            Enrolment enrolment = await _service.GetEnrolmentAsync((int)expectedEnrolmentId);
            Assert.NotNull(enrolment);
            Assert.Equal(expectedEnrolmentId, enrolment.Id);
            Assert.Equal(expectedUserId, enrolment.Enrollee.UserId);
        }

        [Fact]
        public async void testGetEnrolmentForUserId()
        {
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();
            Guid expectedUserId = testEnrolment.Enrollee.UserId;

            // create the enrolment directly to the context
            _dbContext.Enrolments.Add(testEnrolment);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolmentId = (int)testEnrolment.Id;

            // get the enrolment through the service layer code
            Enrolment enrolment = await _service.GetEnrolmentForUserIdAsync(expectedUserId);
            Assert.NotNull(enrolment);
            Assert.Equal(expectedEnrolmentId, enrolment.Id);
            Assert.Equal(expectedUserId, enrolment.Enrollee.UserId);
        }

        [Fact]
        public async void testGetEnrolments()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create some enrolments directly to the context
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());

            await _dbContext.SaveChangesAsync();

            // get the enrolments through the service layer code
            var enrolments = await _service.GetEnrolmentsAsync(EMPTY_ENROLMENT_SEARCH_OPTIONS);
            Assert.NotNull(enrolments);
            Assert.Equal(3, enrolments.Count());
        }

        [Fact]
        public async void testGetEnrolments_Filtered()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create some enrolments directly to the context
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());

            await _dbContext.SaveChangesAsync();

            // get the enrolments through the service layer code
            var enrolmentsInProgress = await _service.GetEnrolmentsAsync(new EnrolmentSearchOptions { StatusCode = Status.IN_PROGRESS_CODE });
            Assert.NotNull(enrolmentsInProgress);
            Assert.Equal(3, enrolmentsInProgress.Count());

            // get the enrolments through the service layer code
            var enrolmentsSubmitted = await _service.GetEnrolmentsAsync(new EnrolmentSearchOptions { StatusCode = Status.SUBMITTED_CODE });
            Assert.NotNull(enrolmentsSubmitted);
            Assert.Empty(enrolmentsSubmitted);
        }

        [Fact]
        public async void testGetEnrolmentsForUserId()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create some enrolments directly to the context
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();
            Guid expectedUserId = testEnrolment.Enrollee.UserId;
            _dbContext.Enrolments.Add(testEnrolment);
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());

            await _dbContext.SaveChangesAsync();

            // get the enrolments through the service layer code
            var enrolments = await _service.GetEnrolmentsForUserIdAsync(expectedUserId);
            Assert.NotNull(enrolments);
            Assert.Single(enrolments);
        }

        [Fact]
        public async void testUpdateEnrolment()
        {
            string expectedName = "ChangedName";
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();

            testEnrolment.Enrollee.FirstName = "StartingName";

            // create the enrolment directly to the context
            _dbContext.Enrolments.Add(testEnrolment);
            await _dbContext.SaveChangesAsync();
            int enrolmentId = (int)testEnrolment.Id;

            // get the enrolment directly from the context
            Enrolment enrolment = await _dbContext.Enrolments
                            .Include(e => e.Enrollee).ThenInclude(e => e.PhysicalAddress)
                            .Include(e => e.Enrollee).ThenInclude(e => e.MailingAddress)
                            .Include(e => e.Certifications)
                            .Include(e => e.Jobs)
                            .Include(e => e.Organizations)
                            .AsNoTracking()
                            .Where(e => e.Id == enrolmentId)
                            .SingleOrDefaultAsync();
            Assert.NotNull(enrolment);

            // make sure we are not tracking anything - i.e. isolate following transaction
            TestUtils.DetachAllEntities(_dbContext);

            // update the enrolment through the service layer code
            enrolment.Enrollee.FirstName = expectedName;
            int updated = await _service.UpdateEnrolmentAsync(enrolment);
            Assert.True(updated > 0);

            // get the updated enrolment directly from the context
            Enrolment updatedEnrolment = await _dbContext.Enrolments.FindAsync(enrolmentId);
            Assert.NotNull(updatedEnrolment);
            Assert.Equal(enrolmentId, updatedEnrolment.Id);
            Assert.Equal(expectedName, updatedEnrolment.Enrollee.FirstName);
        }

        [Fact]
        public async void testDeleteEnrolment()
        {
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();
            Guid expectedUserId = testEnrolment.Enrollee.UserId;

            // create the enrolment directly to the context
            _dbContext.Enrolments.Add(testEnrolment);
            await _dbContext.SaveChangesAsync();
            int enrolmentId = (int)testEnrolment.Id;

            // check that the enrolment save actually saved to the context
            Enrolment enrolment = await _dbContext.Enrolments.FindAsync(enrolmentId);
            Assert.NotNull(enrolment);
            Assert.Equal(enrolmentId, enrolment.Id);
            Assert.Equal(expectedUserId, enrolment.Enrollee.UserId);

            // delete the enrolment through the service layer code
            await _service.DeleteEnrolmentAsync(enrolmentId);

            // get the updated enrolment through the service layer code
            Enrolment deletedEnrolment = await _dbContext.Enrolments.FindAsync(enrolmentId);
            Assert.Null(deletedEnrolment);
        }

        [Fact]
        public async void testGetAvailableEnrolmentStatuses()
        {
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();
            Guid expectedUserId = testEnrolment.Enrollee.UserId;

            // create the enrolment directly to the context
            _dbContext.Enrolments.Add(testEnrolment);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolmentId = (int)testEnrolment.Id;

            // get the available statuses through the service layer code
            var statuses = await _service.GetAvailableEnrolmentStatusesAsync((int)expectedEnrolmentId);
            Assert.NotNull(statuses);
            Assert.Single(statuses);
            Assert.Contains(_dbContext.Statuses.Single(s => s.Code == Status.SUBMITTED_CODE), statuses);
        }

        [Fact]
        public async void testGetEnrolmentStatuses()
        {
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();
            Guid expectedUserId = testEnrolment.Enrollee.UserId;

            // create the enrolment directly to the context
            _dbContext.Enrolments.Add(testEnrolment);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolmentId = (int)testEnrolment.Id;

            // get the enrolment statuses through the service layer code
            var enrolmentStatuses = await _service.GetEnrolmentStatusesAsync((int)expectedEnrolmentId);
            Assert.NotNull(enrolmentStatuses);
            Assert.Single(enrolmentStatuses);
            Assert.Equal(_dbContext.Statuses.Single(s => s.Code == Status.IN_PROGRESS_CODE), enrolmentStatuses.First().Status);
        }

        [Fact]
        public async void testCreateEnrolmentStatuses()
        {
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();
            Guid expectedUserId = testEnrolment.Enrollee.UserId;

            // create the enrolment directly to the context
            _dbContext.Enrolments.Add(testEnrolment);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolmentId = (int)testEnrolment.Id;

            // create the enrolment status through the service layer code
            var enrolmentStatus = await _service.CreateEnrolmentStatusAsync((int)expectedEnrolmentId, _dbContext.Statuses.Single(s => s.Code == Status.SUBMITTED_CODE));
            Assert.NotNull(enrolmentStatus);
            Assert.Equal(_dbContext.Statuses.Single(s => s.Code == Status.SUBMITTED_CODE), enrolmentStatus.Status);
        }

        [Fact]
        public async void testCreateEnrolmentStatuses_Generate_LicensePlate()
        {
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();
            // manually change the status to approved
            testEnrolment.CurrentStatus.StatusCode = Status.APPROVED_CODE;
            Guid expectedUserId = testEnrolment.Enrollee.UserId;

            // create the enrolment directly to the context
            _dbContext.Enrolments.Add(testEnrolment);
            await _dbContext.SaveChangesAsync();
            int expectedEnrolmentId = (int)testEnrolment.Id;

            // create the enrolment status through the service layer code
            var enrolmentStatus = await _service.CreateEnrolmentStatusAsync((int)expectedEnrolmentId, _dbContext.Statuses.Single(s => s.Code == Status.ACCEPTED_TOS_CODE));
            Assert.NotNull(enrolmentStatus);
            Assert.Equal(_dbContext.Statuses.Single(s => s.Code == Status.ACCEPTED_TOS_CODE), enrolmentStatus.Status);

            // get the enrollee object, and check that there is a 20 character license plate
            var enrollee = _dbContext.Enrollees.Single(e => e.UserId == expectedUserId);
            Assert.NotNull(enrollee);
            Assert.NotNull(enrollee.LicensePlate);
            Assert.Equal(20, enrollee.LicensePlate.Length);
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

            // check all of the permutations for workflow state changes
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
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, SUBMITTED));
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

            // add the admin role to the user
            TestUtils.AddAdminRoleToUser(_httpContext?.HttpContext?.User);

            // check all of the permutations for workflow state changes
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
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, IN_PROGRESS));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, SUBMITTED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, APPROVED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, DECLINED));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, ACCEPTED_TOS));
            Assert.False(_service.IsStatusChangeAllowed(DECLINED, DECLINED_TOS));

            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, null));
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, IN_PROGRESS));
            Assert.False(_service.IsStatusChangeAllowed(ACCEPTED_TOS, SUBMITTED));
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

            // remove the admin role from the user
            TestUtils.RemoveAdminRoleFromUser(_httpContext?.HttpContext?.User);
        }
    }
}
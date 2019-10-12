using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;

namespace PrimeTests.Services
{
    public class DefaultEnrolmentServiceTests :  BaseServiceTests<DefaultEnrolmentService>
    {
        [Fact]
        public async void testEnrolmentExists()
        {
            //make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());

            // since there are no items, we are expecting the created Id to get set to 1
            int expectedEnrolmentId = 1;

            // check to see if the enrolment does not exist through the service layer
            Assert.False(_service.EnrolmentExists(expectedEnrolmentId));

            // create the enrolment directly to the context
            var enrolment = TestUtils.EnrolmentFaker.Generate();

            _dbContext.Enrolments.Add(enrolment);
            await _dbContext.SaveChangesAsync();

            //make sure there are now enrolments
            Assert.True(_dbContext.Enrolments.Any());
            Assert.Equal(expectedEnrolmentId, enrolment.Id);

            // check to see if the enrolment exists through the service layer
            Assert.True(_service.EnrolmentExists(expectedEnrolmentId));
        }

        [Fact]
        public async void testCreateEnrolment()
        {
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();
            string expectedUserId = testEnrolment.Enrollee.UserId;

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
            string expectedUserId = testEnrolment.Enrollee.UserId;

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
            string expectedUserId = testEnrolment.Enrollee.UserId;

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
            //make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create some enrolments directly to the context
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());

            await _dbContext.SaveChangesAsync();

            // get the enrolments through the service layer code
            var enrolments = await _service.GetEnrolmentsAsync();
            Assert.NotNull(enrolments);
            Assert.Equal(3, enrolments.Count());
        }

        [Fact]
        public async void testGetEnrolmentsForUserId()
        {
            //make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create some enrolments directly to the context
            var testEnrolment = TestUtils.EnrolmentFaker.Generate();
            string expectedUserId = testEnrolment.Enrollee.UserId;
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
            string expectedUserId = testEnrolment.Enrollee.UserId;

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
    }
}
using System;
using System.Linq;
using Xunit;

using Prime.Services;
using PrimeTests.Utils;

namespace PrimeTests.Services
{
    public class DefaultEnrolleeServiceTests : BaseServiceTests<DefaultEnrolleeService>
    {
        [Fact]
        public async void testGetEnrollees()
        {
            //make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create some enrollees directly to the context
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());

            await _dbContext.SaveChangesAsync();

            // get the enrollees through the service layer code
            var enrollees = await _service.GetEnrolleesAsync();
            Assert.NotNull(enrollees);
            Assert.Equal(3, enrollees.Count());
        }

        [Fact]
        public async void testGetEnrolleesForUserId()
        {
            //make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create some enrollees directly to the context
            var testEnrollee = TestUtils.EnrolleeFaker.Generate();
            Guid expectedUserId = testEnrollee.UserId;
            _dbContext.Enrollees.Add(testEnrollee);
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());

            await _dbContext.SaveChangesAsync();

            // get the enrollees through the service layer code
            var enrollees = await _service.GetEnrolleesForUserIdAsync(expectedUserId);
            Assert.NotNull(enrollees);
            Assert.Single(enrollees);
        }
    }
}
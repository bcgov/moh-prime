using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;
using Xunit;

namespace PrimeTests.Services
{
    public class DefaultEnrolmentServiceTests
    {
        [Fact]
        public async Task<int?> testCreateEnrolment()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestEnrolmentService")
                .Options;

            int? expectedEnrolmentId = null;
            string expectedUserId = "12234";

            using (var context = new ApiDbContext(options))
            {
                var service = new DefaultEnrolmentService(context);

                var testEnrolment = TestUtils.EnrolmentFaker.Generate();
                testEnrolment.Enrollee.UserId = expectedUserId;

                expectedEnrolmentId = await service.CreateEnrolmentAsync(testEnrolment);
            }

            using (var context = new ApiDbContext(options))
            {
                Enrolment enrolment = context.Enrolments.Include(e => e.Enrollee).FirstAsync().Result;
                Assert.Equal(expectedEnrolmentId, enrolment.Id);
                Assert.Equal(expectedUserId, enrolment.Enrollee.UserId);
            }

            return expectedEnrolmentId;
        }

        [Fact]
        public async Task testGetEnrolment()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "TestEnrolmentService")
                .Options;

            int? expectedEnrolmentId = null;

            using (var context = new ApiDbContext(options))
            {
                var service = new DefaultEnrolmentService(context);
                expectedEnrolmentId = await service.CreateEnrolmentAsync(TestUtils.EnrolmentFaker.Generate());
            }

            using (var context = new ApiDbContext(options))
            {
                var service = new DefaultEnrolmentService(context);
                var enrolment = await service.GetEnrolmentAsync((int)expectedEnrolmentId);

                Assert.Equal(expectedEnrolmentId, enrolment.Id);
            }
        }
    }
}
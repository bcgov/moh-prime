using System;
using System.Linq;
using Xunit;

using Prime.Models;
using PrimeTests.Utils;

namespace PrimeTests.UnitTests
{
    public class AuditTests : InMemoryDbTest
    {
        [Fact]
        public async void TestAudits_Creation()
        {
            var enrollee = new Enrollee();
            TestDb.Enrollees.Add(enrollee);
            Assert.Equal(default(DateTimeOffset), enrollee.CreatedTimeStamp);
            Assert.Equal(default(DateTimeOffset), enrollee.UpdatedTimeStamp);

            await TestDb.SaveChangesAsync();

            Assert.NotEqual(default(DateTimeOffset), enrollee.CreatedTimeStamp);
            Assert.NotEqual(default(DateTimeOffset), enrollee.UpdatedTimeStamp);
        }

        [Fact]
        public async void TestAudits_Update()
        {
            var enrollee = new Enrollee();
            TestDb.Enrollees.Add(enrollee);
            await TestDb.SaveChangesAsync();
            var initialCreated = enrollee.CreatedTimeStamp;
            var initialUpdated = enrollee.UpdatedTimeStamp;

            enrollee.FirstName = "Name";
            await TestDb.SaveChangesAsync();

            Assert.Equal(initialCreated, enrollee.CreatedTimeStamp);
            Assert.True(enrollee.UpdatedTimeStamp > initialUpdated);
        }

        [Fact]
        public async void TestAudits_ImmutableCreated()
        {
            var enrollee = TestDb.HasAnEnrollee();
            var initialCreated = enrollee.CreatedTimeStamp;

            var retrieved = TestDb.Enrollees.Single();
            retrieved.CreatedTimeStamp = DateTimeOffset.Now.AddDays(10);
            await TestDb.SaveChangesAsync();

            retrieved = TestDb.Enrollees.Single();
            Assert.Equal(initialCreated, retrieved.CreatedTimeStamp);
        }
    }
}

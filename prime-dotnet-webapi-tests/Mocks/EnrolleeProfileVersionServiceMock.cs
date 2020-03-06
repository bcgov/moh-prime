using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;

namespace PrimeTests.Mocks
{
    public class EnrolleeProfileVersionServiceMock : BaseMockService, IEnrolleeProfileVersionService
    {
        public EnrolleeProfileVersionServiceMock() : base()
        { }

        public override void SeedData()
        { }

        public Task CreateEnrolleeProfileVersionAsync(Enrollee enrollee)
        {
            return Task.CompletedTask;
        }

        public Task<EnrolleeProfileVersion> GetEnrolleeProfileVersionAsync(int enrolleeProfileVersionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EnrolleeProfileVersion>> GetEnrolleeProfileVersionsAsync(int enrolleeId)
        {
            throw new NotImplementedException();
        }

        public Task<EnrolleeProfileVersion> GetEnrolleeProfileVersionBeforeDateAsync(int enrolleeId, DateTime dateTime)
        {
            var profileVersion = TestUtils.EnrolleeProfileVersionFaker.Generate();
            return Task.FromResult(profileVersion);
        }
    }
}

using System;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class AccessTermServiceMock : BaseMockService, IAccessTermService
    {
        public AccessTermServiceMock() : base()
        { }

        public override void SeedData()
        { }

        public async Task<AccessTerm> GetAccessTermAsync(Enrollee enrollee)
        {
            throw new NotImplementedException();
        }

        public async Task<AccessTerm> GetEnrolleeAccessTermsAsync(int enrolleeId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateEnrolleeAccessTermAsync(Enrollee enrollee)
        {
            throw new NotImplementedException();
        }

        public async Task SetAcceptedDateForAccessTermAsync(Enrollee enrollee)
        {
            // throw new NotImplementedException();
        }
    }
}

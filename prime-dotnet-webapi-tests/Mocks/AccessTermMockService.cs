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

        public Task SetEnrolleeAccessTermsAsync(Enrollee enrollee)
        {
            throw new NotImplementedException();
        }

        public Task<AccessTerm> GetEnrolleeAccessTermsAsync(int enrolleeId)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Threading.Tasks;
using Prime.Models;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class TermsOfAccessServiceMock : BaseMockService, ITermsOfAccessService
    {
        public TermsOfAccessServiceMock() : base()
        { }

        public override void SeedData()
        { }

        public Task SetEnrolleeTermsOfAccessAsync(Enrollee enrollee)
        {
            throw new NotImplementedException();
        }

        public Task<TermsOfAccess> GetEnrolleeTermsOfAccessAsync(int enrolleeId)
        {
            throw new NotImplementedException();
        }
    }
}

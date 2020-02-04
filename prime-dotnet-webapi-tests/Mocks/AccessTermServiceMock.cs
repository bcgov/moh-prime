using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public Task<AccessTerm> GetMostRecentNotAcceptedEnrolleesAccessTermAsync(int enrolleeId)
        {
            throw new NotImplementedException();
        }

        public Task<AccessTerm> GetMostRecentAcceptedEnrolleesAccessTermAsync(int enrolleeId)
        {
            throw new NotImplementedException();
        }

        public Task<AccessTerm> GetEnrolleesAccessTermAsync(int enrolleeId, int accessTermId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccessTerm>> GetAcceptedAccessTerms(int enrolleeId)
        {
            throw new NotImplementedException();
        }

        public Task CreateEnrolleeAccessTermAsync(Enrollee enrollee)
        {
            throw new NotImplementedException();
        }

        public Task AcceptCurrentAccessTermAsync(Enrollee enrollee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AccessTermExistsAsync(int accessTermId)
        {
            throw new NotImplementedException();
        }
    }
}

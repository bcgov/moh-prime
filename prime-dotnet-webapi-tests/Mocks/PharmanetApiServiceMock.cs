using System;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class PharmanetApiServiceMock : BaseMockService, IPharmanetApiService
    {
        public PharmanetApiServiceMock() : base()
        { }

        public override void SeedData()
        {
            throw new NotImplementedException();
        }

        public Task<PharmanetCollegeRecord> GetCollegeRecord(Certification certification)
        {
            throw new NotImplementedException();
        }
    }
}

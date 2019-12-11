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
            // no data to seed, as it is done in the base class
        }

        public Task<PharmanetCollegeRecord> GetCollegeRecord(Certification certification)
        {
            throw new NotImplementedException();
        }
    }
}

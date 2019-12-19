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
            // TODO data?
        }

        public Task<PharmanetCollegeRecord> GetCollegeRecordAsync(Certification certification)
        {
            return Task.FromResult<PharmanetCollegeRecord>(null);
        }
    }
}

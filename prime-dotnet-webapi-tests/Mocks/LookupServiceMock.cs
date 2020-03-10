using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Prime.Models.Api;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class LookupServiceMock : BaseMockService, ILookupService
    {
        public LookupServiceMock() : base()
        { }

        public override void SeedData()
        {
            // no data to seed, as it is done in the base class for lookups
        }

        public Task<LookupEntity> GetLookupsAsync()
        {
            return Task.FromResult(new LookupEntity());
        }
    }
}

using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Agreements;

namespace PrimeTests.ModelFactories
{
    public static class AgreementLookup
    {
        public static IEnumerable<Agreement> All { get => AgreementConfiguration.SeedData; }
    }
}

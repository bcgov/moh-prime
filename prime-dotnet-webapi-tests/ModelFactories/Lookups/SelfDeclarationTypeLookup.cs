using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class SelfDeclarationTypeLookup
    {
        private static IEnumerable<SelfDeclarationType> _seedData = new SelfDeclarationTypeConfiguration().SeedData;

        public static IEnumerable<SelfDeclarationType> All { get { return _seedData; } }

    }
}

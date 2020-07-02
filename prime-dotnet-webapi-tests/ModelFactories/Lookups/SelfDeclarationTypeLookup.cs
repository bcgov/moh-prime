using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class SelfDeclarationTypeLookup
    {
        private static ICollection<SelfDeclarationType> _seedData = new SelfDeclarationTypeConfiguration().SeedData;

        public static ICollection<SelfDeclarationType> All { get { return _seedData; } }

    }
}

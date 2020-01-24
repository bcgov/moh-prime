using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class GlobalClauseLookup
    {
        private static ICollection<GlobalClause> _seedData = new GlobalClauseConfiguration().SeedData;

        public static ICollection<GlobalClause> All { get { return _seedData; } }
    }
}

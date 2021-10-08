using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class JobNameLookup
    {
        private static IEnumerable<JobName> _seedData = new JobNameConfiguration().SeedData;

        public static IEnumerable<JobName> All { get { return _seedData; } }
    }
}

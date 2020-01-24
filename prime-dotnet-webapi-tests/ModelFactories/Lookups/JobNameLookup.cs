using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class JobNameLookup
    {
        private static ICollection<JobName> _seedData = new JobNameConfiguration().SeedData;

        public static ICollection<JobName> All { get { return _seedData; } }
    }
}

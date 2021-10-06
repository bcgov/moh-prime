using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class LicenseLookup
    {
        private static IEnumerable<License> _seedData = new LicenseConfiguration().SeedData;

        public static IEnumerable<License> All { get { return _seedData; } }

        public static IEnumerable<License> AllowedFor(int collegeCode)
        {
            return CollegeLicenseLookup.All
                .Where(cl => cl.CollegeCode == collegeCode)
                .Select(cl => All.Single(l => l.Code == cl.LicenseCode));
        }
    }
}

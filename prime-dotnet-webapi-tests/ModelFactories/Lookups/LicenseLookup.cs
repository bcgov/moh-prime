using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class LicenseLookup
    {
        private static ICollection<License> _seedData = new LicenseConfiguration().SeedData;

        public static ICollection<License> All { get { return _seedData; } }

        public static IEnumerable<License> AllowedFor(short collegeCode)
        {
            return CollegeLicenseLookup.All
                .Where(cl => cl.CollegeCode == collegeCode)
                .Select(cl => All.Single(l => l.Code == cl.LicenseCode));
        }
    }
}

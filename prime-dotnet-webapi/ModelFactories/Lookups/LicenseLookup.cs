using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class LicenseLookup
    {
        private static ICollection<License> _seedData = new LicenseConfiguration().SeedData;

        public static ICollection<License> All { get { return _seedData; } }

        public static IEnumerable<License> AllowedFor(College college)
        {
            return CollegeLicenseLookup.All
                .Where(x => x.College.Code == college.Code)
                .Select(x => x.License);
        }
    }
}

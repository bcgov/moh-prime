using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class LicenseLookup
    {
        private static ICollection<License> _seedData = new LicenseConfiguration().SeedData;

        public static ICollection<License> All { get { return _seedData; } }
    }
}

using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class CollegeLicenseLookup
    {
        private static ICollection<CollegeLicense> _seedData = new CollegeLicenseConfiguration().SeedData;

        public static ICollection<CollegeLicense> All { get { return _seedData; } }
    }
}

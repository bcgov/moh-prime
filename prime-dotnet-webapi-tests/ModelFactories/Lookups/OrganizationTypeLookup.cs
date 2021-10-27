using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class CareSettingLookup
    {
        private static IEnumerable<CareSetting> _seedData = new CareSettingConfiguration().SeedData;

        public static IEnumerable<CareSetting> All { get { return _seedData; } }

        // Contain only "Private Community Health Practice" and "Community Pharmacy"
        public static IEnumerable<CareSetting> SiteCareSettings { get { return _seedData.Where(cs => cs.Code == 2 || cs.Code == 3); } }
    }
}

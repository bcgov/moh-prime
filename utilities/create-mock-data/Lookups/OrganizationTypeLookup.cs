using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class CareSettingLookup
    {
        private static IEnumerable<CareSetting> _seedData = new CareSettingConfiguration().SeedData;

        public static IEnumerable<CareSetting> All { get { return _seedData; } }
    }
}

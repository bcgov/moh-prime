using System.Collections.Generic;
using System.Linq;
using Prime.Configuration.Database;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public static class VendorLookup
    {
        private static IEnumerable<Vendor> _seedData = new VendorConfiguration().SeedData;

        public static IEnumerable<Vendor> All { get { return _seedData; } }

        public static IEnumerable<Vendor> AllowedFor(int careSettingCode)
        {
            return VendorLookup.All
                .Where(v => v.CareSettingCode == careSettingCode);
        }
    }
}

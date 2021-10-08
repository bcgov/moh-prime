using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class ProvinceLookup
    {
        private static IEnumerable<Province> _seedData = new ProvinceConfiguration().SeedData;

        public static IEnumerable<Province> All { get { return _seedData; } }

        public static Province BC
        {
            get { return All.Single(p => p.Code == Province.BRITISH_COLUMBIA_CODE); }
        }
        public static IEnumerable<Province> NotBC
        {
            get { return All.Where(p => p.Code != Province.BRITISH_COLUMBIA_CODE); }
        }
    }
}

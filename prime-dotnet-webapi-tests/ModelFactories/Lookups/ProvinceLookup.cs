using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTest.ModelFactories
{
    public static class ProvinceLookup
    {
        private static ICollection<Province> _seedData = new ProvinceConfiguration().SeedData;

        public static ICollection<Province> All {get {return _seedData;}}

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

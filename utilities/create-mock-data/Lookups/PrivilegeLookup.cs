using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class PrivilegeLookup
    {
        private static IEnumerable<Privilege> _seedData = new PrivilegeConfiguration().SeedData;

        public static IEnumerable<Privilege> All { get { return _seedData; } }

        public static Privilege ById(int privilegeId)
        {
            return All.Single(p => p.Id == privilegeId);
        }
    }
}

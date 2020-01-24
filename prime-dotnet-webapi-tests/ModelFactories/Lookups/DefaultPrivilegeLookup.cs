using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTest.ModelFactories
{
    public static class DefaultPrivilegeLookup
    {
        private static ICollection<DefaultPrivilege> _seedData = new DefaultPrivilegeConfiguration().SeedData;

        public static ICollection<DefaultPrivilege> All { get { return _seedData; } }
    }
}

using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class DefaultPrivilegeLookup
    {
        private static IEnumerable<DefaultPrivilege> _seedData = new DefaultPrivilegeConfiguration().SeedData;

        public static IEnumerable<DefaultPrivilege> All { get { return _seedData; } }
    }
}

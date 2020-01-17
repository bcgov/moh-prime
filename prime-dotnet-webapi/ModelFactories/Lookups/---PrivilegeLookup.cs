using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class PrivilegeLookup
    {
        private static ICollection<Privilege> _seedData = new PrivilegeConfiguration().SeedData;

        public static ICollection<Privilege> All { get { return _seedData; } }
    }
}

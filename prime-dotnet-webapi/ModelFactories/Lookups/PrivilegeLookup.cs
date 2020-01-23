using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class PrivilegeLookup
    {
        private static ICollection<Privilege> _seedData = new PrivilegeConfiguration().SeedData;

        public static ICollection<Privilege> All { get { return _seedData; } }

        public static Privilege ById(int privilegeId)
        {
            return All.Single(p => p.Id == privilegeId);
        }
    }
}

using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class PrivilegeLookup
    {
        private static ICollection<Privilege> _seedData = new PrivilegeConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<Privilege> All { get { return _seedData; } }

        public static Privilege ById(int privilegeId)
        {
            return _seedData.Single(p => p.Id == privilegeId);
        }
    }
}

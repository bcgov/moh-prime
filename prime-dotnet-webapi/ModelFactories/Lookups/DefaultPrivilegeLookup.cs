using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class DefaultPrivilegeLookup
    {
        private static ICollection<DefaultPrivilege> _seedData = new DefaultPrivilegeConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<DefaultPrivilege> All { get { return _seedData; } }
    }
}

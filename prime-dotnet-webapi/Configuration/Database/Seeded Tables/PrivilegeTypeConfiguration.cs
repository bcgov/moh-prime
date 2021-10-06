using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class PrivilegeTypeConfiguration : SeededTable<PrivilegeType>
    {
        public override IEnumerable<PrivilegeType> SeedData
        {
            get
            {
                return new[]{
                    new PrivilegeType { Code = 1, Name = "Allowable Role"        },
                    new PrivilegeType { Code = 2, Name = "Allowable Transaction" }
                };
            }
        }
    }
}

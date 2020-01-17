using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class PrivilegeTypeConfiguration : SeededTable<PrivilegeType>
    {
        public override ICollection<PrivilegeType> SeedData
        {
            get
            {
                return new[]{
                    new PrivilegeType { Code = 1, Name = "Allowable Role", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new PrivilegeType { Code = 2, Name = "Allowable Transaction", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

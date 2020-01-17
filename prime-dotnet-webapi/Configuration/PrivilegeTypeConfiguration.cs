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
                new PrivilegeType { Code = 1, Name = "Allowable Role", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new PrivilegeType { Code = 2, Name = "Allowable Transaction", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
                };
            }
        }
    }
}

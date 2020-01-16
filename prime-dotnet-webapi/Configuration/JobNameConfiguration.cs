using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class JobNameConfiguration : SeededTable<JobName>
    {
        public override ICollection<JobName> SeedData
        {
            get
            {
                return new[] {
                new JobName { Code = 1, Name = "Medical Office Assistant", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new JobName { Code = 2, Name = "Pharmacy Assistant", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new JobName { Code = 3, Name = "Registration Clerk", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new JobName { Code = 4, Name = "Ward Clerk", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
                };
            }
        }
    }
}

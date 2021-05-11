using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class JobNameConfiguration : SeededTable<JobName>
    {
        public override IEnumerable<JobName> SeedData
        {
            get
            {
                return new[] {
                    new JobName { Code = 1, Name = "Medical office assistant", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new JobName { Code = 2, Name = "Pharmacy assistant",       CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new JobName { Code = 3, Name = "Registration clerk",       CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new JobName { Code = 4, Name = "Ward clerk",               CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new JobName { Code = 5, Name = "Nursing unit assistant",   CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

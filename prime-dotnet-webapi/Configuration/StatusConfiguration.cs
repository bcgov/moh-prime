using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class StatusConfiguration : SeededTable<Status>
    {
        public override IEnumerable<Status> SeedData
        {
            get
            {
                return new[] {
                    new Status { Code = 1, Name = "Editable", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Status { Code = 2, Name = "Under Review", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Status { Code = 3, Name = "Requires TOA", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Status { Code = 4, Name = "Locked", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Status { Code = 5, Name = "Declined", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

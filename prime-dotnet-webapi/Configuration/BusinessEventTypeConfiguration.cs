using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class BusinessEventTypeConfiguration : SeededTable<BusinessEventType>
    {
        public override IEnumerable<BusinessEventType> SeedData
        {
            get
            {
                return new[] {
                    new BusinessEventType { Code = 1, Name = "Status Change", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new BusinessEventType { Code = 2, Name = "Email", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new BusinessEventType { Code = 3, Name = "Note", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new BusinessEventType { Code = 4, Name = "Admin Claim", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new BusinessEventType { Code = 5, Name = "Enrollee", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new BusinessEventType { Code = 6, Name = "Site", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new BusinessEventType { Code = 7, Name = "Admin View", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new BusinessEventType { Code = 8, Name = "Organization", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }

    }
}

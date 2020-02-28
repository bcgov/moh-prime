using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class BusinessEventTypeConfiguration : SeededTable<BusinessEventType>
    {
        public override ICollection<BusinessEventType> SeedData
        {
            get
            {
                return new[] {
                    new BusinessEventType { Code = 1, Name = "Status Change", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new BusinessEventType { Code = 2, Name = "Email", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new BusinessEventType { Code = 3, Name = "Note", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }

    }
}

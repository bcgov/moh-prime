using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class StatusConfiguration : SeededTable<Status>
    {
        public override ICollection<Status> SeedData
        {
            get
            {
                return new[] {
                new Status { Code = 1, Name = "In Progress", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new Status { Code = 2, Name = "Submitted", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new Status { Code = 3, Name = "Adjudicated/Approved", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new Status { Code = 4, Name = "Declined", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new Status { Code = 5, Name = "Accepted Access Agreement", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new Status { Code = 6, Name = "Declined Access Agreement", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

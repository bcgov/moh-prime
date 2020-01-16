using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class StatusReasonConfiguration : SeededTable<StatusReason>
    {
        public override ICollection<StatusReason> SeedData
        {
            get
            {
                return new[] {
                new StatusReason { Code = 1, Name = "Automatic", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 2, Name = "Manual", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 3, Name = "PharmaNet Error, Licence could not be Validated", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 4, Name = "College Licence not in PharmaNet", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 6, Name = "Birthdate Discrepancy with PharmaNet College Licence", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 5, Name = "Name Discrepancy with PharmaNet College Licence", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 7, Name = "Listed as Non-Practicing on PharmaNet College Licence", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 8, Name = "Insulin Pump Provider", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 9, Name = "Licence Class", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 10, Name = "Answered one or more Self Declaration questions \"Yes\"", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new StatusReason { Code = 11, Name = "Contact Address or Identity Address not in British Columbia", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

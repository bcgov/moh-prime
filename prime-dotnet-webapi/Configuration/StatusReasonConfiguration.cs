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
                    new StatusReason { Code = 1,  Name = "Automatically Adjudicated", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 2,  Name = "Manually Adjudicated", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 3,  Name = "PharmaNet Error, Licence could not be validated", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 4,  Name = "College Licence not in PharmaNet practitioner table", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 6,  Name = "Birthdate discrepancy in PharmaNet practitioner table", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 5,  Name = "Name discrepancy in PharmaNet practitioner table", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 7,  Name = "Listed as Non-Practicing in PharmaNet practitioner table", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 8,  Name = "Insulin Pump Provider", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 9,  Name = "Licence Class requires manual adjudication", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 10, Name = "Answered one or more Self Declaration questions \"Yes\"", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 11, Name = "Contact Address or Identity Address not in British Columbia", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 12, Name = "Admin has flagged the applicant for manual adjudication", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 13, Name = "User does not have high enough identity assurance level", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 14, Name = "User authenticated with a method other than BC Services Card", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new StatusReason { Code = 15, Name = "User has Requested Remote Access", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                };
            }
        }
    }
}

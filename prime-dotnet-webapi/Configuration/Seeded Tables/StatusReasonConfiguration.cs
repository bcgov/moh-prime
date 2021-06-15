using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class StatusReasonConfiguration : SeededTable<StatusReason>
    {
        public override IEnumerable<StatusReason> SeedData
        {
            get
            {
                return new[] {
                    new StatusReason { Code = 1,  Name = "Automatically Adjudicated"                                    },
                    new StatusReason { Code = 2,  Name = "Manually Adjudicated"                                         },
                    new StatusReason { Code = 3,  Name = "PharmaNet Error, License could not be validated"              },
                    new StatusReason { Code = 4,  Name = "College License or Practitioner ID not in PharmaNet table"    },
                    new StatusReason { Code = 6,  Name = "Birthdate discrepancy in PharmaNet practitioner table"        },
                    new StatusReason { Code = 5,  Name = "Name discrepancy in PharmaNet practitioner table"             },
                    new StatusReason { Code = 7,  Name = "Listed as Non-Practicing in PharmaNet practitioner table"     },
                    new StatusReason { Code = 8,  Name = "Insulin Pump Provider"                                        },
                    new StatusReason { Code = 9,  Name = "Licence Class requires manual adjudication"                   },
                    new StatusReason { Code = 10, Name = "Answered one or more Self Declaration questions \"Yes\""      },
                    new StatusReason { Code = 11, Name = "Contact Address or Identity Address not in British Columbia"  },
                    new StatusReason { Code = 12, Name = "Admin has flagged the applicant for manual adjudication"      },
                    new StatusReason { Code = 13, Name = "User does not have high enough identity assurance level"      },
                    new StatusReason { Code = 14, Name = "User authenticated with a method other than BC Services Card" },
                    new StatusReason { Code = 15, Name = "User has Requested Remote Access"                             },
                    new StatusReason { Code = 16, Name = "Terms of Access to be determined by an Adjudicator"           },
                    new StatusReason { Code = 17, Name = "No address from BCSC. Enrollee entered address."              },
                };
            }
        }
    }
}

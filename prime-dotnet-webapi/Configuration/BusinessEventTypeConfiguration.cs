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
                    new BusinessEventType { Code = 1, Name = "Status Change"      },
                    new BusinessEventType { Code = 2, Name = "Email"              },
                    new BusinessEventType { Code = 3, Name = "Note"               },
                    new BusinessEventType { Code = 4, Name = "Admin Claim"        },
                    new BusinessEventType { Code = 5, Name = "Enrollee"           },
                    new BusinessEventType { Code = 6, Name = "Site"               },
                    new BusinessEventType { Code = 7, Name = "Admin View"         },
                    new BusinessEventType { Code = 8, Name = "Organization"       },
                    new BusinessEventType { Code = 9, Name = "Pharmanet API Call" }
                };
            }
        }

    }
}

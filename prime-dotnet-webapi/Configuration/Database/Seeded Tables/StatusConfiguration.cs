using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class StatusConfiguration : SeededTable<Status>
    {
        public override IEnumerable<Status> SeedData
        {
            get
            {
                return new[] {
                    new Status { Code = 1, Name = "Editable"     },
                    new Status { Code = 2, Name = "Under Review" },
                    new Status { Code = 3, Name = "Requires TOA" },
                    new Status { Code = 4, Name = "Locked"       },
                    new Status { Code = 5, Name = "Declined"     }
                };
            }
        }
    }
}

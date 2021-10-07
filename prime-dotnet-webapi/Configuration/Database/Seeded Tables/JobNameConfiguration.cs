using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class JobNameConfiguration : SeededTable<JobName>
    {
        public override IEnumerable<JobName> SeedData
        {
            get
            {
                return new[] {
                    new JobName { Code = 1, Name = "Medical office assistant" },
                    new JobName { Code = 2, Name = "Pharmacy assistant"       },
                    new JobName { Code = 3, Name = "Registration clerk"       },
                    new JobName { Code = 4, Name = "Ward clerk"               },
                    new JobName { Code = 5, Name = "Nursing unit assistant"   }
                };
            }
        }
    }
}

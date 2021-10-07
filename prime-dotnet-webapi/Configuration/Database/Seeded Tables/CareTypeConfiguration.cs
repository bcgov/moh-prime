using System.Collections.Generic;
using Prime.Models.HealthAuthorities;

namespace Prime.Configuration.Database
{
    public class CareTypeConfiguration : SeededTable<CareType>
    {
        public override IEnumerable<CareType> SeedData
        {
            get
            {
                return new[] {
                    new CareType { Code = 1, Name = "Ambulatory Care" },
                    new CareType { Code = 2, Name = "Acute Care"      },
                };
            }
        }
    }
}

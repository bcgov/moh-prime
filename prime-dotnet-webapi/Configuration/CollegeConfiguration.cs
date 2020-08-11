using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class CollegeConfiguration : SeededTable<College>
    {
        public override IEnumerable<College> SeedData
        {
            get
            {
                return new[] {
                    new College { Code = 1, Name = "College of Physicians and Surgeons of BC (CPSBC)", Prefix = "91", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new College { Code = 2, Name = "College of Pharmacists of BC (CPBC)", Prefix = "P1", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new College { Code = 3, Name = "BC College of Nursing Professionals (BCCNP)", Prefix = "96", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

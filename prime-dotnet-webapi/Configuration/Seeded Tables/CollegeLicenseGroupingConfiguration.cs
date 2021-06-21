using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class CollegelicenseGroupingConfiguration : SeededTable<CollegeLicenseGrouping>
    {
        public override IEnumerable<CollegeLicenseGrouping> SeedData
        {
            get
            {
                return new[] {
                    new CollegeLicenseGrouping { Code = 1, Name = "Licensed Practical Nurse",                 Weight = 1 },
                    new CollegeLicenseGrouping { Code = 2, Name = "Registered Nurse/Licensed Graduate Nurse", Weight = 2 },
                    new CollegeLicenseGrouping { Code = 3, Name = "Registered Psychiatric Nurse",             Weight = 3 },
                    new CollegeLicenseGrouping { Code = 4, Name = "Nurse Practitioner",                       Weight = 4 },
                    new CollegeLicenseGrouping { Code = 5, Name = "Midwife",                                  Weight = 5 }
                };
            }
        }
    }
}

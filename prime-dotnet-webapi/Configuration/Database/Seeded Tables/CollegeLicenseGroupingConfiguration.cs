using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class CollegelicenseGroupingConfiguration : SeededTable<CollegeLicenseGrouping>
    {
        public override IEnumerable<CollegeLicenseGrouping> SeedData
        {
            get
            {
                return new[] {
                    //Nurse Groups
                    new CollegeLicenseGrouping { Code = 1, Name = "Licensed Practical Nurse",                 Weight = 1 },
                    new CollegeLicenseGrouping { Code = 2, Name = "Registered Nurse/Licensed Graduate Nurse", Weight = 2 },
                    new CollegeLicenseGrouping { Code = 3, Name = "Registered Psychiatric Nurse",             Weight = 3 },
                    new CollegeLicenseGrouping { Code = 4, Name = "Nurse Practitioner",                       Weight = 4 },
                    new CollegeLicenseGrouping { Code = 5, Name = "Midwife",                                  Weight = 5 },

                    //Oral Health Professional Groups
                    new CollegeLicenseGrouping { Code = 6, Name = "Dental Assistant",                         Weight = 6 },
                    new CollegeLicenseGrouping { Code = 7, Name = "Dental Hygienist",                         Weight = 7 },
                    new CollegeLicenseGrouping { Code = 8, Name = "Dental Technician",                        Weight = 8 },
                    new CollegeLicenseGrouping { Code = 9, Name = "Dental Therapist",                         Weight = 9 },
                    new CollegeLicenseGrouping { Code = 10, Name = "Dentist",                                 Weight = 10 },
                    new CollegeLicenseGrouping { Code = 11, Name = "Denturist",                               Weight = 11 }
                };
            }
        }
    }
}

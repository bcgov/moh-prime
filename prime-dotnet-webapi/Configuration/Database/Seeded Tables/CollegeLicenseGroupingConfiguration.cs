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
                    new CollegeLicenseGrouping { Code = 1, Name = "Licensed Practical Nurse",                      Weight = 1 },
                    new CollegeLicenseGrouping { Code = 2, Name = "Registered Nurse/Licensed Graduate Nurse",      Weight = 2 },
                    new CollegeLicenseGrouping { Code = 3, Name = "Registered Psychiatric Nurse",                  Weight = 3 },
                    new CollegeLicenseGrouping { Code = 4, Name = "Nurse Practitioner",                            Weight = 4 },
                    new CollegeLicenseGrouping { Code = 5, Name = "Midwife",                                       Weight = 5 },

                    //Oral Health Professional Groups
                    new CollegeLicenseGrouping { Code = 10, Name = "Dentist",                                      Weight = 6 },
                    new CollegeLicenseGrouping { Code = 9, Name = "Dental Therapist",                              Weight = 7 },
                    new CollegeLicenseGrouping { Code = 6, Name = "Certified Dental Assistant",                    Weight = 8 },
                    new CollegeLicenseGrouping { Code = 7, Name = "Dental Hygienist",                              Weight = 9 },
                    new CollegeLicenseGrouping { Code = 8, Name = "Dental Technician",                             Weight = 10 },
                    new CollegeLicenseGrouping { Code = 11, Name = "Denturist",                                    Weight = 11 },

                    //Class from College of Health and Care Professionals of BC
                    new CollegeLicenseGrouping { Code = 12, Name = "Designated Health Profession of Dietetics",                                    Weight = 12 },
                    new CollegeLicenseGrouping { Code = 13, Name = "Designated Health Profession of Occupational Therapy",                         Weight = 13 },
                    new CollegeLicenseGrouping { Code = 14, Name = "Designated Health Profession of Opticianry",                                   Weight = 14 },
                    new CollegeLicenseGrouping { Code = 15, Name = "Designated Health Profession of Optometry",                                    Weight = 15 },
                    new CollegeLicenseGrouping { Code = 16, Name = "Designated Health Profession of Physical Therapy",                             Weight = 16 },
                    new CollegeLicenseGrouping { Code = 17, Name = "Designated Health Profession of Psychology",                                   Weight = 17 },
                    new CollegeLicenseGrouping { Code = 18, Name = "Designated Health Profession of Audiology",                                    Weight = 18 },
                    new CollegeLicenseGrouping { Code = 19, Name = "Designated Health Profession of Hearing Instrument Dispensing",                Weight = 19 },
                    new CollegeLicenseGrouping { Code = 20, Name = "Designated Health Profession of Speech-Language Pathology",                    Weight = 20 },

                    //Class from College of Complementary Health Professionals of BC
                    new CollegeLicenseGrouping { Code = 21, Name = "Designated Health Profession of Chiropractic",                                 Weight = 21 },
                    new CollegeLicenseGrouping { Code = 22, Name = "Designated Health Profession of Massage Therapy",                              Weight = 22 },
                    new CollegeLicenseGrouping { Code = 23, Name = "Designated Health Profession of Naturopathic Medicine",                        Weight = 23 },
                    new CollegeLicenseGrouping { Code = 24, Name = "Designated Health Profession of Traditional Chinese Medicine and Acupuncture", Weight = 24 },
                };
            }
        }
    }
}

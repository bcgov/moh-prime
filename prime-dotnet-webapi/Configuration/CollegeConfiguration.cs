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
                    new College { Code = 1,  Name = "College of Physicians and Surgeons of BC (CPSBC)",                               CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new College { Code = 2,  Name = "College of Pharmacists of BC (CPBC)",                                            CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new College { Code = 3,  Name = "BC College of Nurses and Midwives (BCCNM)",                                      CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    new College { Code = 4,  Name = "College of Chiropractors of BC",                                                 CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 5,  Name = "College of Dental Hygenists of BC",                                              CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 6,  Name = "College of Dental Technicians of BC",                                            CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 7,  Name = "College of Dental Surgeons of BC",                                               CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 8,  Name = "College of Denturists of BC",                                                    CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 9,  Name = "College of Dietitians of BC",                                                    CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 10, Name = "College of Massage Therapists of BC",                                            CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 11, Name = "College of Naturopathic Physicians of BC",                                       CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 12, Name = "College of Occupational Therapists of BC",                                       CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 13, Name = "College of Opticians of BC",                                                     CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 14, Name = "College of Optometrists of BC",                                                  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 15, Name = "College of Physical Therapists of BC",                                           CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 16, Name = "College of Psychologists of BC",                                                 CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 17, Name = "College of Speech and Hearing Health Professionals of BC",                       CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                    new College { Code = 18, Name = "College of Traditional Chinese Medicine Practitioners and Acupuncturists of BC", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE},
                };
            }
        }
    }
}

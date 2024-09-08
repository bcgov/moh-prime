using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class CollegeConfiguration : SeededTable<College>
    {
        public override IEnumerable<College> SeedData
        {
            get
            {
                return new[] {
                    new College { Code = 1,  Name = "College of Physicians and Surgeons of BC (CPSBC)", Weight = 10                                },
                    new College { Code = 2,  Name = "College of Pharmacists of BC (CPBC)", Weight = 20                                             },
                    new College { Code = 3,  Name = "BC College of Nurses and Midwives (BCCNM)", Weight = 30                                       },

                    new College { Code = 4,  Name = "College of Chiropractors of BC", Weight = 50                                                  },
                    new College { Code = 5,  Name = "College of Dental Hygenists of BC", Weight = 999                                              },
                    new College { Code = 6,  Name = "College of Dental Technicians of BC", Weight = 999                                            },
                    new College { Code = 7,  Name = "College of Dental Surgeons of BC", Weight = 999                                               },
                    new College { Code = 8,  Name = "College of Denturists of BC", Weight = 60                                                     },
                    new College { Code = 9,  Name = "College of Dietitians of BC", Weight = 70                                                     },
                    new College { Code = 10, Name = "College of Massage Therapists of BC", Weight = 80                                             },
                    new College { Code = 11, Name = "College of Naturopathic Physicians of BC", Weight = 90                                        },
                    new College { Code = 12, Name = "College of Occupational Therapists of BC", Weight = 100                                       },
                    new College { Code = 13, Name = "College of Opticians of BC", Weight = 110                                                     },
                    new College { Code = 14, Name = "College of Optometrists of BC", Weight = 120                                                  },
                    new College { Code = 15, Name = "College of Physical Therapists of BC", Weight = 130                                           },
                    new College { Code = 16, Name = "College of Psychologists of BC", Weight = 140                                                 },
                    new College { Code = 17, Name = "College of Speech and Hearing Health Professionals of BC", Weight = 160                       },
                    new College { Code = 18, Name = "College of Traditional Chinese Medicine Practitioners and Acupuncturists of BC", Weight = 170 },
                    new College { Code = 19, Name = "BC College of Social Workers", Weight = 180                                                   },
                    new College { Code = 20, Name = "BC College of Oral Health Professionals", Weight = 40                                         },

                    /*
                    College of Health and Care Professionals of BC is replacing the following colleges:
                    College of Dietitians of BC
                    College of Occupational Therapists of BC
                    College of Optometrists of BC
                    College of Opticians of BC
                    College of Physical Therapists of BC
                    College of Psychologists of BC
                    College of Speech and Hearing Health Professionals of BC
                    */
                    new College { Code = 21, Name = "College of Health and Care Professionals of BC", Weight = 70                                   },

                    /*
                    College of Complementary Health Professionals of British Columbia is replacing the following colleges:
                    College of Chiropractors of BC
                    College of Massage Therapists of BC
                    College of Naturopathic Physicians of BC
                    College of Traditional Chinese Medicine Practitioners and Acupuncturists of BC
                    */
                    new College { Code = 22, Name = "College of Complementary Health Professionals of BC", Weight = 50                              }
                };
            }
        }
    }
}


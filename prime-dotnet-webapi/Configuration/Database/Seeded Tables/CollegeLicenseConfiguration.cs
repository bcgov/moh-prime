using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class CollegeLicenseConfiguration : SeededTable<CollegeLicense>
    {
        public override IEnumerable<CollegeLicense> SeedData
        {
            get
            {
                return new[] {
                    // CPSBC
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 1 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 2 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 3 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 4 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 5 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 6 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 7 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 8 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 9 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 10 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 11 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 12 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 179 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 13 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 14 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 15 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 16 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 17 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 18 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 19 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 20 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 21, Discontinued = true },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 22 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 23 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 24, Discontinued = true },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 59 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 65 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 66 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 67 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 87 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 88 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 89 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 90 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 91 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 92 },

                    // Pharmacists
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 25 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 26 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 27 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 28 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 29 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 30 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 31 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 68 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 178 },

                    // Nurses
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 32,  CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 33,  CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 34,  CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 35,  CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 36,  CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 37,  CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 39,  CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 40,  CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 175, CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 41,  CollegeLicenseGroupingCode = 3 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 42,  CollegeLicenseGroupingCode = 3 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 43,  CollegeLicenseGroupingCode = 3 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 45,  CollegeLicenseGroupingCode = 3 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 46,  CollegeLicenseGroupingCode = 3 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 176, CollegeLicenseGroupingCode = 3 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 47,  CollegeLicenseGroupingCode = 4 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 48,  CollegeLicenseGroupingCode = 4 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 49,  CollegeLicenseGroupingCode = 4 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 51,  CollegeLicenseGroupingCode = 4 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 52,  CollegeLicenseGroupingCode = 1 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 53,  CollegeLicenseGroupingCode = 1 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 54,  CollegeLicenseGroupingCode = 1 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 55,  CollegeLicenseGroupingCode = 1 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 177, CollegeLicenseGroupingCode = 1 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 60,  CollegeLicenseGroupingCode = 5 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 61,  CollegeLicenseGroupingCode = 5 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 62,  CollegeLicenseGroupingCode = 5 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 63,  CollegeLicenseGroupingCode = 5 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 69,  CollegeLicenseGroupingCode = 5 },

                    // College of Dental Surgeons of BC
                    new CollegeLicense { CollegeCode = 7,  LicenseCode = 70, Discontinued = true },
                    new CollegeLicense { CollegeCode = 7,  LicenseCode = 75, Discontinued = true },
                    new CollegeLicense { CollegeCode = 7,  LicenseCode = 76, Discontinued = true },
                    new CollegeLicense { CollegeCode = 7,  LicenseCode = 77, Discontinued = true },

                    // College of Naturopathic Physicians of BC
                    new CollegeLicense { CollegeCode = 11,  LicenseCode = 78, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 11,  LicenseCode = 79, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 11,  LicenseCode = 80, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 11,  LicenseCode = 81, Discontinued = true  },

                    // College of Optometrists of BC
                    new CollegeLicense { CollegeCode = 14,  LicenseCode = 71, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 14,  LicenseCode = 72, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 14,  LicenseCode = 73, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 14,  LicenseCode = 74, Discontinued = true  },

                    // All other colleges are assigned the "Not Displayed" Licence
                    new CollegeLicense { CollegeCode = 4,  LicenseCode = 64, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 5,  LicenseCode = 64, Discontinued = true },
                    new CollegeLicense { CollegeCode = 6,  LicenseCode = 64, Discontinued = true },
                    new CollegeLicense { CollegeCode = 8,  LicenseCode = 64, Discontinued = true },

                    new CollegeLicense { CollegeCode = 9,  LicenseCode = 64, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 10, LicenseCode = 64, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 12, LicenseCode = 64, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 13, LicenseCode = 64, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 15, LicenseCode = 64, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 16, LicenseCode = 64, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 17, LicenseCode = 64, Discontinued = true  },
                    new CollegeLicense { CollegeCode = 18, LicenseCode = 64, Discontinued = true  },

                    //BC College of Social Workers
                    new CollegeLicense { CollegeCode = 19, LicenseCode = 82 },
                    new CollegeLicense { CollegeCode = 19, LicenseCode = 83 },
                    new CollegeLicense { CollegeCode = 19, LicenseCode = 84 },
                    new CollegeLicense { CollegeCode = 19, LicenseCode = 85 },
                    new CollegeLicense { CollegeCode = 19, LicenseCode = 86 },

                    //College of Oral Health Professionals
                    // Dental Assistant

                    new CollegeLicense { CollegeCode = 20, LicenseCode = 93, CollegeLicenseGroupingCode = 6},
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 94, CollegeLicenseGroupingCode = 6 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 95, CollegeLicenseGroupingCode = 6 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 96, CollegeLicenseGroupingCode = 6 },
                    // Dental Hygienist
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 97, CollegeLicenseGroupingCode = 7 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 98, CollegeLicenseGroupingCode = 7 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 99, CollegeLicenseGroupingCode = 7 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 100, CollegeLicenseGroupingCode = 7 },
                    // Dental Therapist
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 101, CollegeLicenseGroupingCode = 8 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 102, CollegeLicenseGroupingCode = 8 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 103, CollegeLicenseGroupingCode = 8 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 104, CollegeLicenseGroupingCode = 8 },
                    // Dental Therapist
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 105, CollegeLicenseGroupingCode = 9 },
                    // Dentist
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 106, CollegeLicenseGroupingCode = 10 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 107, CollegeLicenseGroupingCode = 10 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 108, CollegeLicenseGroupingCode = 10 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 109, CollegeLicenseGroupingCode = 10 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 110, CollegeLicenseGroupingCode = 10 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 111, CollegeLicenseGroupingCode = 10 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 112, CollegeLicenseGroupingCode = 10 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 113, CollegeLicenseGroupingCode = 10 },
                    // Denturist
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 114, CollegeLicenseGroupingCode = 11 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 115, CollegeLicenseGroupingCode = 11 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 116, CollegeLicenseGroupingCode = 11 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 117, CollegeLicenseGroupingCode = 11 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 118, CollegeLicenseGroupingCode = 11 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 119, CollegeLicenseGroupingCode = 11 },


                    //College of Health and Care Professionals of BC
                    //Dietetics
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 120, CollegeLicenseGroupingCode = 12},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 121, CollegeLicenseGroupingCode = 12},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 122, CollegeLicenseGroupingCode = 12},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 123, CollegeLicenseGroupingCode = 12},
                    //Occupational Therapy
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 124, CollegeLicenseGroupingCode = 13},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 125, CollegeLicenseGroupingCode = 13},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 126, CollegeLicenseGroupingCode = 13},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 127, CollegeLicenseGroupingCode = 13},
                    //Opticianry
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 128, CollegeLicenseGroupingCode = 14},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 129, CollegeLicenseGroupingCode = 14},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 130, CollegeLicenseGroupingCode = 14},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 131, CollegeLicenseGroupingCode = 14},
                    //Optometry
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 132, CollegeLicenseGroupingCode = 15},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 133, CollegeLicenseGroupingCode = 15},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 134, CollegeLicenseGroupingCode = 15},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 135, CollegeLicenseGroupingCode = 15},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 136, CollegeLicenseGroupingCode = 15},
                    //Physical Therapy
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 137, CollegeLicenseGroupingCode = 16},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 138, CollegeLicenseGroupingCode = 16},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 139, CollegeLicenseGroupingCode = 16},
                    //Psychology
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 140, CollegeLicenseGroupingCode = 17},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 141, CollegeLicenseGroupingCode = 17},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 142, CollegeLicenseGroupingCode = 17},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 143, CollegeLicenseGroupingCode = 17},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 144, CollegeLicenseGroupingCode = 17},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 145, CollegeLicenseGroupingCode = 17},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 146, CollegeLicenseGroupingCode = 17},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 147, CollegeLicenseGroupingCode = 17},
                    //Audiology
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 148, CollegeLicenseGroupingCode = 18},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 149, CollegeLicenseGroupingCode = 18},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 150, CollegeLicenseGroupingCode = 18},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 151, CollegeLicenseGroupingCode = 18},
                    //Hearing Instrument Dispensing
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 152, CollegeLicenseGroupingCode = 19},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 153, CollegeLicenseGroupingCode = 19},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 154, CollegeLicenseGroupingCode = 19},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 155, CollegeLicenseGroupingCode = 19},
                    //Speech-Language Pathology
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 156, CollegeLicenseGroupingCode = 20},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 157, CollegeLicenseGroupingCode = 20},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 158, CollegeLicenseGroupingCode = 20},
                    new CollegeLicense { CollegeCode = 21,  LicenseCode = 159, CollegeLicenseGroupingCode = 20},

                    //College of Complementary Health Professionals of BC
                    //Chiropractic
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 160, CollegeLicenseGroupingCode = 21},
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 161, CollegeLicenseGroupingCode = 21},
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 162, CollegeLicenseGroupingCode = 21},
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 163, CollegeLicenseGroupingCode = 21},
                    //Massage Therapy
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 164, CollegeLicenseGroupingCode = 22},
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 165, CollegeLicenseGroupingCode = 22},
                    //Naturopathic Medicine
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 166, CollegeLicenseGroupingCode = 23},
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 167, CollegeLicenseGroupingCode = 23},
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 168, CollegeLicenseGroupingCode = 23},
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 169, CollegeLicenseGroupingCode = 23},
                    //Traditional Chinese Medicine and Acupuncture
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 170, CollegeLicenseGroupingCode = 24},
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 171, CollegeLicenseGroupingCode = 24},
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 172, CollegeLicenseGroupingCode = 24},
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 173, CollegeLicenseGroupingCode = 24},
                    new CollegeLicense { CollegeCode = 22,  LicenseCode = 174, CollegeLicenseGroupingCode = 24},

                };
            }
        }

        public override void Configure(EntityTypeBuilder<CollegeLicense> builder)
        {
            builder.HasKey(cl => new { cl.CollegeCode, cl.LicenseCode });
            builder.HasOne(cl => cl.College)
                .WithMany(c => c.CollegeLicenses)
                .HasForeignKey(cl => cl.CollegeCode);
            builder.HasOne(cl => cl.License)
                .WithMany(l => l.CollegeLicenses)
                .HasForeignKey(cl => cl.LicenseCode);

            builder.HasData(SeedData);
        }
    }
}

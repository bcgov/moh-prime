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
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 13 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 14 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 15 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 16 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 17 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 18 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 19 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 20 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 21 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 22 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 23 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 24 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 59 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 65 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 66 },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 67 },

                    // Pharmacists
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 25 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 26 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 27 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 28 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 29 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 30 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 31 },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 68 },

                    // Nurses
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 32, CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 33, CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 34, CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 35, CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 36, CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 37, CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 39, CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 40, CollegeLicenseGroupingCode = 2 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 41, CollegeLicenseGroupingCode = 3 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 42, CollegeLicenseGroupingCode = 3 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 43, CollegeLicenseGroupingCode = 3 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 45, CollegeLicenseGroupingCode = 3 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 46, CollegeLicenseGroupingCode = 3 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 47, CollegeLicenseGroupingCode = 4 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 48, CollegeLicenseGroupingCode = 4 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 49, CollegeLicenseGroupingCode = 4 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 51, CollegeLicenseGroupingCode = 4 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 52, CollegeLicenseGroupingCode = 1 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 53, CollegeLicenseGroupingCode = 1 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 54, CollegeLicenseGroupingCode = 1 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 55, CollegeLicenseGroupingCode = 1 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 60, CollegeLicenseGroupingCode = 5 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 61, CollegeLicenseGroupingCode = 5 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 62, CollegeLicenseGroupingCode = 5 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 63, CollegeLicenseGroupingCode = 5 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 69, CollegeLicenseGroupingCode = 5 },

                    // College of Dental Surgeons of BC
                    new CollegeLicense { CollegeCode = 7,  LicenseCode = 70 },
                    new CollegeLicense { CollegeCode = 7,  LicenseCode = 75 },
                    new CollegeLicense { CollegeCode = 7,  LicenseCode = 76 },
                    new CollegeLicense { CollegeCode = 7,  LicenseCode = 77 },

                    // College of Naturopathic Physicians of BC
                    new CollegeLicense { CollegeCode = 11,  LicenseCode = 78 },
                    new CollegeLicense { CollegeCode = 11,  LicenseCode = 79 },
                    new CollegeLicense { CollegeCode = 11,  LicenseCode = 80 },
                    new CollegeLicense { CollegeCode = 11,  LicenseCode = 81 },

                    // College of Optometrists of BC
                    new CollegeLicense { CollegeCode = 14,  LicenseCode = 71 },
                    new CollegeLicense { CollegeCode = 14,  LicenseCode = 72 },
                    new CollegeLicense { CollegeCode = 14,  LicenseCode = 73 },
                    new CollegeLicense { CollegeCode = 14,  LicenseCode = 74 },

                    // All other colleges are assigned the "Not Displayed" Licence
                    new CollegeLicense { CollegeCode = 4,  LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 5,  LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 6,  LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 8,  LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 9,  LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 10, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 12, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 13, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 15, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 16, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 17, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 18, LicenseCode = 64 },

                    //BC College of Social Workers
                    new CollegeLicense { CollegeCode = 19, LicenseCode = 82 },
                    new CollegeLicense { CollegeCode = 19, LicenseCode = 83 },
                    new CollegeLicense { CollegeCode = 19, LicenseCode = 84 },
                    new CollegeLicense { CollegeCode = 19, LicenseCode = 85 },
                    new CollegeLicense { CollegeCode = 19, LicenseCode = 86 },

                    //College of Oral Health Professionals
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 87 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 88 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 89 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 90 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 91 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 92 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 93 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 94 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 95 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 96 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 97 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 98 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 99 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 100 },
                    new CollegeLicense { CollegeCode = 20, LicenseCode = 101 },
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

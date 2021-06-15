using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
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
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 32 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 33 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 34 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 35 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 36 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 37 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 38 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 39 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 40 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 41 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 42 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 43 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 44 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 45 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 46 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 47 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 48 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 49 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 50 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 51 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 52 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 53 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 54 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 55 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 56 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 57 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 58 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 60 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 61 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 62 },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 63 },

                    // All other colleges are assigned the "Not Displayed" Licence
                    new CollegeLicense { CollegeCode = 4,  LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 5,  LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 6,  LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 7,  LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 8,  LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 9,  LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 10, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 11, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 12, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 13, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 14, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 15, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 16, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 17, LicenseCode = 64 },
                    new CollegeLicense { CollegeCode = 18, LicenseCode = 64 },
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

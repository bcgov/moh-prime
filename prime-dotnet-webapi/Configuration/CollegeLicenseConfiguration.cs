using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class CollegeLicenseConfiguration : SeededTable<CollegeLicense>
    {
        public override ICollection<CollegeLicense> SeedData
        {
            get
            {
                return new[] {
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 1,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 2,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 3,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 4,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 5,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 6,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 7,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 8,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 9,  CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 10, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 11, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 12, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 13, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 14, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 15, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 16, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 17, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 18, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 19, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 20, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 21, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 22, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 23, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 1, LicenseCode = 24, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 25, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 26, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 27, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 28, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 29, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 30, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 2, LicenseCode = 31, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 32, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 33, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 34, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 35, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 36, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 37, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 38, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 39, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 40, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 41, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 42, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 43, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 44, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 45, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 46, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 47, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 48, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 49, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 50, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 51, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 52, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 53, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 54, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 55, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CollegeLicense { CollegeCode = 3, LicenseCode = 56, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
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

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
                new CollegeLicense { CollegeCode = 1, LicenseCode = 1,  CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 2,  CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 3,  CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 4,  CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 5,  CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 6,  CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 7,  CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 8,  CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 9,  CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 10, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 11, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 12, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 13, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 14, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 15, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 16, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 17, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 18, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 19, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 20, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 21, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 22, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 23, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 1, LicenseCode = 24, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 2, LicenseCode = 25, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 2, LicenseCode = 26, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 2, LicenseCode = 27, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 2, LicenseCode = 28, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 2, LicenseCode = 29, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 2, LicenseCode = 30, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 2, LicenseCode = 31, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 32, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 33, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 34, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 35, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 36, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 37, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 38, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 39, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 40, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 41, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 42, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 43, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 44, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 45, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 46, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 47, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 48, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 49, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 50, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 51, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 52, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 53, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 54, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 55, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new CollegeLicense { CollegeCode = 3, LicenseCode = 56, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
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

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class CollegePracticeConfiguration : SeededTable<CollegePractice>
    {
        public override ICollection<CollegePractice> SeedData
        {
            get
            {
                return new[] {
               new CollegePractice { CollegeCode = 3, PracticeCode = 1, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
               new CollegePractice { CollegeCode = 3, PracticeCode = 2, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
               new CollegePractice { CollegeCode = 3, PracticeCode = 3, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
               new CollegePractice { CollegeCode = 3, PracticeCode = 4, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }

        public override void Configure(EntityTypeBuilder<CollegePractice> builder)
        {
            builder.HasKey(cp => new { cp.CollegeCode, cp.PracticeCode });
            builder.HasOne(cp => cp.College)
                .WithMany(c => c.CollegePractices)
                .HasForeignKey(cp => cp.CollegeCode);
            builder.HasOne(cp => cp.Practice)
                .WithMany(p => p.CollegePractices)
                .HasForeignKey(cp => cp.PracticeCode);

            builder.HasData(SeedData);
        }
    }
}

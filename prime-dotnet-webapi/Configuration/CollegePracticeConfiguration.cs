using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

public class CollegePracticeConfiguration : IEntityTypeConfiguration<CollegePractice>
{
    private readonly Guid SYSTEM_USER = Guid.Empty;

    private readonly DateTime SEEDING_DATE = DateTime.Now;

    public void Configure(EntityTypeBuilder<CollegePractice> builder)
    {

        builder.HasKey(cp => new { cp.CollegeCode, cp.PracticeCode });
        builder.HasOne(cp => cp.College)
             .WithMany(c => c.CollegePractices)
             .HasForeignKey(cp => cp.CollegeCode);
        builder.HasOne(cp => cp.Practice)
            .WithMany(p => p.CollegePractices)
            .HasForeignKey(cp => cp.PracticeCode);

        #region CollegePracticeSeed
        builder.HasData(
            new CollegePractice { CollegeCode = 3, PracticeCode = 1, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new CollegePractice { CollegeCode = 3, PracticeCode = 2, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new CollegePractice { CollegeCode = 3, PracticeCode = 3, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new CollegePractice { CollegeCode = 3, PracticeCode = 4, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
            );
        #endregion
    }
}

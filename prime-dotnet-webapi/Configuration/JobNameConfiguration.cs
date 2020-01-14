using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;
namespace Prime.Configuration
{
    public class JobNameConfiguration : IEntityTypeConfiguration<JobName>
    {
        public void Configure(EntityTypeBuilder<JobName> builder)
        {
            builder.HasData(
                new JobName { Code = 1, Name = "Medical Office Assistant", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new JobName { Code = 2, Name = "Pharmacy Assistant", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new JobName { Code = 3, Name = "Registration Clerk", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new JobName { Code = 4, Name = "Ward Clerk", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
            );
        }
    }
}

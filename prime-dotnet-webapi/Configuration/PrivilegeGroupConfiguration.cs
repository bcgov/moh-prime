/*
 * Ministry of Health PRIME Project
 * Approved for Ministry of Health use only.
 */
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class PrivilegeGroupConfiguration : IEntityTypeConfiguration<PrivilegeGroup>
    {
        public void Configure(EntityTypeBuilder<PrivilegeGroup> builder)
        {
            builder.HasOne(pg => pg.PrivilegeType)
                    .WithMany(pt => pt.PrivilegeGroups)
                    .HasForeignKey(pg => pg.PrivilegeTypeCode);

            builder.HasData(
                new PrivilegeGroup { Code = 1, PrivilegeTypeCode = 2, Name = "Submit and Access Claims", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new PrivilegeGroup { Code = 2, PrivilegeTypeCode = 2, Name = "Record Medical History", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new PrivilegeGroup { Code = 3, PrivilegeTypeCode = 2, Name = "Access Medical History", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new PrivilegeGroup { Code = 4, PrivilegeTypeCode = 1, Name = "Role", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new PrivilegeGroup { Code = 5, PrivilegeTypeCode = 1, Name = "RU That Can Have OBO's", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
            );
        }
    }
}

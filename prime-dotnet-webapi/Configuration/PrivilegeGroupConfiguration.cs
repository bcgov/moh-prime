/*
 * Ministry of Health PRIME Project
 * Approved for Ministry of Health use only.
 */
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class PrivilegeGroupConfiguration : SeededTable<PrivilegeGroup>
    {
        public override ICollection<PrivilegeGroup> SeedData
        {
            get
            {
                return new[] {
                    new PrivilegeGroup { Code = 1, PrivilegeTypeCode = 2, Name = "Submit and Access Claims", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new PrivilegeGroup { Code = 2, PrivilegeTypeCode = 2, Name = "Record Medical History", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new PrivilegeGroup { Code = 3, PrivilegeTypeCode = 2, Name = "Access Medical History", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new PrivilegeGroup { Code = 4, PrivilegeTypeCode = 1, Name = "Role", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new PrivilegeGroup { Code = 5, PrivilegeTypeCode = 1, Name = "RU That Can Have OBO's", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }

        public override void Configure(EntityTypeBuilder<PrivilegeGroup> builder)
        {
            builder.HasOne(pg => pg.PrivilegeType)
                .WithMany(pt => pt.PrivilegeGroups)
                .HasForeignKey(pg => pg.PrivilegeTypeCode);

            builder.HasData(SeedData);
        }
    }
}

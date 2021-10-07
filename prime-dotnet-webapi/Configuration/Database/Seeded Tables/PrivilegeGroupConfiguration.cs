/*
 * Ministry of Health PRIME Project
 * Approved for Ministry of Health use only.
 */
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class PrivilegeGroupConfiguration : SeededTable<PrivilegeGroup>
    {
        public override IEnumerable<PrivilegeGroup> SeedData
        {
            get
            {
                return new[] {
                    new PrivilegeGroup { Code = 1, PrivilegeTypeCode = 2, Name = "Submit and Access Claims" },
                    new PrivilegeGroup { Code = 2, PrivilegeTypeCode = 2, Name = "Record Medical History"   },
                    new PrivilegeGroup { Code = 3, PrivilegeTypeCode = 2, Name = "Access Medical History"   },
                    new PrivilegeGroup { Code = 5, PrivilegeTypeCode = 1, Name = "RU That Can Have OBOs"    }
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

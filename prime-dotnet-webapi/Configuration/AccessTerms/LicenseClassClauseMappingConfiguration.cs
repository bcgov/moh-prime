using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class LicenseClassClauseMappingConfiguration : SeededTable<LicenseClassClauseMapping>
    {
        public override ICollection<LicenseClassClauseMapping> SeedData
        {
            get
            {
                return new[] {
                    new LicenseClassClauseMapping { LicenseCode = 1, OrganizatonTypeCode = 2, LicenseClassClauseId = 3, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new LicenseClassClauseMapping { LicenseCode = 1, OrganizatonTypeCode = 2, LicenseClassClauseId = 2, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new LicenseClassClauseMapping { LicenseCode = 1, OrganizatonTypeCode = 3, LicenseClassClauseId = 1, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new LicenseClassClauseMapping { LicenseCode = 1, OrganizatonTypeCode = 3, LicenseClassClauseId = 2, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }

        public override void Configure(EntityTypeBuilder<LicenseClassClauseMapping> builder)
        {
            builder.HasKey(m => new { m.LicenseCode, m.OrganizatonTypeCode, m.LicenseClassClauseId });
            builder.HasOne(m => m.License)
                .WithMany(l => l.LicenseClassClauseMappings)
                .HasForeignKey(m => m.LicenseCode);
            builder.HasOne(m => m.OrganizationType)
                .WithMany(o => o.LicenseClassClauseMappings)
                .HasForeignKey(m => m.OrganizatonTypeCode);
            builder.HasOne(m => m.LicenseClassClause)
                .WithMany(c => c.LicenseClassClauseMappings)
                .HasForeignKey(m => m.LicenseClassClauseId);

            builder.HasData(SeedData);
        }
    }
}








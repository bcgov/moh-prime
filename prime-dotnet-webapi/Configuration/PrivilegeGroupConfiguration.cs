/*
 * Ministry of Health PRIME Project
 * Approved for Ministry of Health use only.
 */
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class PrivilegeGroupConfiguration : IEntityTypeConfiguration<PrivilegeGroup>
    {
        public void Configure(EntityTypeBuilder<PrivilegeGroup> builder)
        {
            builder.HasData(
                new PrivilegeGroup { Id = 1, Name = "Submit and Access Claims", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new PrivilegeGroup { Id = 2, Name = "Record Medical History", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new PrivilegeGroup { Id = 3, Name = "Access Medical History", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new PrivilegeGroup { Id = 4, Name = "Can be RU (OBO)", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new PrivilegeGroup { Id = 5, Name = "Can be OBO (RU)", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
            );
        }
    }
}

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
        private readonly Guid SYSTEM_USER = Guid.Empty;
        private readonly DateTime SEEDING_DATE = DateTime.Now;

        public void Configure(EntityTypeBuilder<PrivilegeGroup> builder)
        {
            builder.HasData(
                new PrivilegeGroup { Id = 1, Name = "Submit and Access Claims", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new PrivilegeGroup { Id = 2, Name = "Record Medical History", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new PrivilegeGroup { Id = 3, Name = "Access Medical History", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new PrivilegeGroup { Id = 4, Name = "Can be RU (OBO)", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new PrivilegeGroup { Id = 5, Name = "Can be OBO (RU)", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
            );
        }
    }
}

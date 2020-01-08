using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;
namespace Prime.Configuration
{
    public class CollegeConfiguration : IEntityTypeConfiguration<College>
    {
        private readonly Guid SYSTEM_USER = Guid.Empty;
        private readonly DateTime SEEDING_DATE = DateTime.Now;

        public void Configure(EntityTypeBuilder<College> builder)
        {
            builder.HasData(
                new College { Code = 1, Name = "College of Physicians and Surgeons of BC (CPSBC)", Prefix = "91", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new College { Code = 2, Name = "College of Pharmacists of BC (CPBC)", Prefix = "P1", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new College { Code = 3, Name = "BC College of Nursing Professionals (BCCNP)", Prefix = "96", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
            );
        }
    }
}

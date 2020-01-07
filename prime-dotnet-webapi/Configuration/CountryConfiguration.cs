using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        private readonly Guid SYSTEM_USER = Guid.Empty;
        private readonly DateTime SEEDING_DATE = DateTime.Now;

        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country { Code = "CA", Name = "Canada", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Country { Code = "US", Name = "United States", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
            );
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;
namespace Prime.Configuration
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        private readonly Guid SYSTEM_USER = Guid.Empty;
        private readonly DateTime SEEDING_DATE = DateTime.Now;

        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasData(
                new Province { Code = "AB", CountryCode = "CA", Name = "Alberta", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "BC", CountryCode = "CA", Name = "British Columbia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MB", CountryCode = "CA", Name = "Manitoba", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NB", CountryCode = "CA", Name = "New Brunswick", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NL", CountryCode = "CA", Name = "Newfoundland and Labrador", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NS", CountryCode = "CA", Name = "Nova Scotia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "ON", CountryCode = "CA", Name = "Ontario", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "PE", CountryCode = "CA", Name = "Prince Edward Island", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "QC", CountryCode = "CA", Name = "Quebec", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "SK", CountryCode = "CA", Name = "Saskatchewan", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NT", CountryCode = "CA", Name = "Northwest Territories", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NU", CountryCode = "CA", Name = "Nunavut", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "YT", CountryCode = "CA", Name = "Yukon", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "AL", CountryCode = "US", Name = "Alabama", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "AK", CountryCode = "US", Name = "Alaska", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "AS", CountryCode = "US", Name = "American Samoa", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "AZ", CountryCode = "US", Name = "Arizona", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "AR", CountryCode = "US", Name = "Arkansas", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "CA", CountryCode = "US", Name = "California", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "CO", CountryCode = "US", Name = "Colorado", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "CT", CountryCode = "US", Name = "Connecticut", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "DE", CountryCode = "US", Name = "Delaware", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "DC", CountryCode = "US", Name = "District of Columbia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "FL", CountryCode = "US", Name = "Florida", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "GA", CountryCode = "US", Name = "Georgia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "GU", CountryCode = "US", Name = "Guam", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "HI", CountryCode = "US", Name = "Hawaii", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "ID", CountryCode = "US", Name = "Idaho", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "IL", CountryCode = "US", Name = "Illinois", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "IN", CountryCode = "US", Name = "Indiana", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "IA", CountryCode = "US", Name = "Iowa", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "KS", CountryCode = "US", Name = "Kansas", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "KY", CountryCode = "US", Name = "Kentucky", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "LA", CountryCode = "US", Name = "Louisiana", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "ME", CountryCode = "US", Name = "Maine", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MD", CountryCode = "US", Name = "Maryland", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MA", CountryCode = "US", Name = "Massachusetts", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MI", CountryCode = "US", Name = "Michigan", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MN", CountryCode = "US", Name = "Minnesota", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MS", CountryCode = "US", Name = "Mississippi", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MO", CountryCode = "US", Name = "Missouri", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MT", CountryCode = "US", Name = "Montana", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NE", CountryCode = "US", Name = "Nebraska", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NV", CountryCode = "US", Name = "Nevada", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NH", CountryCode = "US", Name = "New Hampshire", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NJ", CountryCode = "US", Name = "New Jersey", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NM", CountryCode = "US", Name = "New Mexico", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NY", CountryCode = "US", Name = "New York", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "NC", CountryCode = "US", Name = "North Carolina", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "ND", CountryCode = "US", Name = "North Dakota", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "MP", CountryCode = "US", Name = "Northern Mariana Islands", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "OH", CountryCode = "US", Name = "Ohio", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "OK", CountryCode = "US", Name = "Oklahoma", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "OR", CountryCode = "US", Name = "Oregon", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "PA", CountryCode = "US", Name = "Pennsylvania", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "PR", CountryCode = "US", Name = "Puerto Rico", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "RI", CountryCode = "US", Name = "Rhode Island", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "SC", CountryCode = "US", Name = "South Carolina", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "SD", CountryCode = "US", Name = "South Dakota", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "TN", CountryCode = "US", Name = "Tennessee", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "TX", CountryCode = "US", Name = "Texas", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "UM", CountryCode = "US", Name = "United States Minor Outlying Islands", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "UT", CountryCode = "US", Name = "Utah", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "VT", CountryCode = "US", Name = "Vermont", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "VI", CountryCode = "US", Name = "Virgin Islands, U.S.", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "VA", CountryCode = "US", Name = "Virginia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "WA", CountryCode = "US", Name = "Washington", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "WV", CountryCode = "US", Name = "West Virginia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "WI", CountryCode = "US", Name = "Wisconsin", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new Province { Code = "WY", CountryCode = "US", Name = "Wyoming", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
            );
        }
    }
}

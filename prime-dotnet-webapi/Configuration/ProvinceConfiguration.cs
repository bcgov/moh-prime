using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class ProvinceConfiguration : SeededTable<Province>
    {
        public override ICollection<Province> SeedData
        {
            get
            {
                return new[] {
                    new Province { Code = "AB", CountryCode = "CA", Name = "Alberta", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "BC", CountryCode = "CA", Name = "British Columbia", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "MB", CountryCode = "CA", Name = "Manitoba", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "NB", CountryCode = "CA", Name = "New Brunswick", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "NL", CountryCode = "CA", Name = "Newfoundland and Labrador", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "NS", CountryCode = "CA", Name = "Nova Scotia", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "ON", CountryCode = "CA", Name = "Ontario", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "PE", CountryCode = "CA", Name = "Prince Edward Island", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "QC", CountryCode = "CA", Name = "Quebec", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "SK", CountryCode = "CA", Name = "Saskatchewan", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "NT", CountryCode = "CA", Name = "Northwest Territories", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "NU", CountryCode = "CA", Name = "Nunavut", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "YT", CountryCode = "CA", Name = "Yukon", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "AL", CountryCode = "US", Name = "Alabama", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "AK", CountryCode = "US", Name = "Alaska", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "AS", CountryCode = "US", Name = "American Samoa", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "AZ", CountryCode = "US", Name = "Arizona", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "AR", CountryCode = "US", Name = "Arkansas", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "CA", CountryCode = "US", Name = "California", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "CO", CountryCode = "US", Name = "Colorado", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "CT", CountryCode = "US", Name = "Connecticut", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "DE", CountryCode = "US", Name = "Delaware", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "DC", CountryCode = "US", Name = "District of Columbia", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "FL", CountryCode = "US", Name = "Florida", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "GA", CountryCode = "US", Name = "Georgia", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "GU", CountryCode = "US", Name = "Guam", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "HI", CountryCode = "US", Name = "Hawaii", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "ID", CountryCode = "US", Name = "Idaho", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "IL", CountryCode = "US", Name = "Illinois", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "IN", CountryCode = "US", Name = "Indiana", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "IA", CountryCode = "US", Name = "Iowa", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "KS", CountryCode = "US", Name = "Kansas", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "KY", CountryCode = "US", Name = "Kentucky", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "LA", CountryCode = "US", Name = "Louisiana", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "ME", CountryCode = "US", Name = "Maine", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "MD", CountryCode = "US", Name = "Maryland", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "MA", CountryCode = "US", Name = "Massachusetts", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "MI", CountryCode = "US", Name = "Michigan", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "MN", CountryCode = "US", Name = "Minnesota", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "MS", CountryCode = "US", Name = "Mississippi", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "MO", CountryCode = "US", Name = "Missouri", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "MT", CountryCode = "US", Name = "Montana", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "NE", CountryCode = "US", Name = "Nebraska", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "NV", CountryCode = "US", Name = "Nevada", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "NH", CountryCode = "US", Name = "New Hampshire", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "NJ", CountryCode = "US", Name = "New Jersey", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "NM", CountryCode = "US", Name = "New Mexico", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "NY", CountryCode = "US", Name = "New York", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "NC", CountryCode = "US", Name = "North Carolina", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "ND", CountryCode = "US", Name = "North Dakota", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "MP", CountryCode = "US", Name = "Northern Mariana Islands", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "OH", CountryCode = "US", Name = "Ohio", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "OK", CountryCode = "US", Name = "Oklahoma", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "OR", CountryCode = "US", Name = "Oregon", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "PA", CountryCode = "US", Name = "Pennsylvania", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "PR", CountryCode = "US", Name = "Puerto Rico", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "RI", CountryCode = "US", Name = "Rhode Island", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "SC", CountryCode = "US", Name = "South Carolina", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "SD", CountryCode = "US", Name = "South Dakota", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "TN", CountryCode = "US", Name = "Tennessee", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "TX", CountryCode = "US", Name = "Texas", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "UM", CountryCode = "US", Name = "United States Minor Outlying Islands", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "UT", CountryCode = "US", Name = "Utah", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "VT", CountryCode = "US", Name = "Vermont", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "VI", CountryCode = "US", Name = "Virgin Islands, U.S.", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "VA", CountryCode = "US", Name = "Virginia", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "WA", CountryCode = "US", Name = "Washington", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "WV", CountryCode = "US", Name = "West Virginia", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "WI", CountryCode = "US", Name = "Wisconsin", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Province { Code = "WY", CountryCode = "US", Name = "Wyoming", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

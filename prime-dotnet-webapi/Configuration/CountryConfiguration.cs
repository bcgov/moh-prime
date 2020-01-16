using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class CountryConfiguration : SeededTable<Country>
    {
        public override ICollection<Country> SeedData
        {
            get
            {
                return new[] {
                new Country { Code = "CA", Name = "Canada", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Country { Code = "US", Name = "United States", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
                };
            }
        }
    }
}

using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class CountryConfiguration : SeededTable<Country>
    {
        public override IEnumerable<Country> SeedData
        {
            get
            {
                return new[] {
                    new Country { Code = "CA", Name = "Canada"        },
                    new Country { Code = "US", Name = "United States" }
                };
            }
        }
    }
}

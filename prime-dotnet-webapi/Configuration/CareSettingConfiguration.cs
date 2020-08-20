using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class CareSettingConfiguration : SeededTable<CareSetting>
    {
        public override IEnumerable<CareSetting> SeedData
        {
            get
            {
                return new[] {
                    new CareSetting { Code = 1, Name = "Health Authority", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CareSetting { Code = 2, Name = "Private Community Health Practice", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CareSetting { Code = 3, Name = "Community Pharmacy", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CareSetting { Code = 4, Name = "Device Provider", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

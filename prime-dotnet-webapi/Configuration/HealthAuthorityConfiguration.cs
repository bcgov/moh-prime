using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class HealthAuthorityConfiguration : SeededTable<HealthAuthority>
    {
        public override IEnumerable<HealthAuthority> SeedData
        {
            get
            {
                return new[] {
                    new HealthAuthority { Code = HealthAuthorityCode.NorthernHealth, Name = "Northern Health", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new HealthAuthority { Code = HealthAuthorityCode.InteriorHealth, Name = "Interior Health", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new HealthAuthority { Code = HealthAuthorityCode.VancouverCoastalHealth, Name = "Vancouver Coastal Health", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new HealthAuthority { Code = HealthAuthorityCode.IslandHealth, Name = "Island Health", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new HealthAuthority { Code = HealthAuthorityCode.FraserHealth, Name = "Fraser Health", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new HealthAuthority { Code = HealthAuthorityCode.ProvincialHealthServicesAuthority, Name = "Provincial Health Services Authority", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

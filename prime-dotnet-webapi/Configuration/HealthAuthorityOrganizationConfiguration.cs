using System.Collections.Generic;
using Prime.Models;
using Prime.Models.HealthAuthorities;

namespace Prime.Configuration
{
    public class HealthAuthorityOrganizationConfiguration : SeededTable<HealthAuthorityOrganization>
    {
        public override IEnumerable<HealthAuthorityOrganization> SeedData
        {
            get
            {
                return new[] {
                    new HealthAuthorityOrganization { Id = (int)HealthAuthorityCode.NorthernHealth,                    Name = "Northern Health",                      CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new HealthAuthorityOrganization { Id = (int)HealthAuthorityCode.InteriorHealth,                    Name = "Interior Health",                      CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new HealthAuthorityOrganization { Id = (int)HealthAuthorityCode.VancouverCoastalHealth,            Name = "Vancouver Coastal Health",             CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new HealthAuthorityOrganization { Id = (int)HealthAuthorityCode.IslandHealth,                      Name = "Island Health",                        CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new HealthAuthorityOrganization { Id = (int)HealthAuthorityCode.FraserHealth,                      Name = "Fraser Health",                        CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new HealthAuthorityOrganization { Id = (int)HealthAuthorityCode.ProvincialHealthServicesAuthority, Name = "Provincial Health Services Authority", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

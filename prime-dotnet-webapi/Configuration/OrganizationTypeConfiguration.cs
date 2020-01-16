using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class OrganizationTypeConfiguration : SeededTable<OrganizationType>
    {
        public override ICollection<OrganizationType> SeedData
        {
            get
            {
                return new[] {
                new OrganizationType { Code = 1, Name = "Community Health Practice Access to PharmaNet (ComPAP)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new OrganizationType { Code = 2, Name = "Health Authority", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new OrganizationType { Code = 3, Name = "Community Practice", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new OrganizationType { Code = 4, Name = "Community Pharmacy", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                new OrganizationType { Code = 5, Name = "Primary Care Network", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

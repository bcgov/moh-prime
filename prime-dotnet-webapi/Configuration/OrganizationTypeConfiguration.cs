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
                    new OrganizationType { Code = 1, Name = "Health Authority", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new OrganizationType { Code = 2, Name = "Private Community Health Practice", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new OrganizationType { Code = 3, Name = "Community Pharmacy", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new OrganizationType { Code = 5, Name = "Device Provider", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

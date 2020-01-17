using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class OrganizationTypeConfiguration : SeededTable<OrganizationType>
    {
        public override ICollection<OrganizationType> SeedData
        {
            builder.HasData(
                new OrganizationType { Code = 1, Name = "Health Authority", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new OrganizationType { Code = 2, Name = "Community Practice", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new OrganizationType { Code = 3, Name = "Community Pharmacy", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new OrganizationType { Code = 4, Name = "Primary Care Network", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
            );
        }
    }
}

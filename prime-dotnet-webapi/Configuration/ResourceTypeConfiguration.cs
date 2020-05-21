using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class ResourceTypeConfiguration : SeededTable<ResourceType>
    {
        public override ICollection<ResourceType> SeedData
        {
            get
            {
                return new[] {
                    new ResourceType { Code = 1, Name = "Site", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new ResourceType { Code = 2, Name = "Enrollee", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

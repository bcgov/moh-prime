/*
 * Ministry of Health PRIME Project
 * Approved for Ministry of Health use only.
 */
using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class PrivilegeGroupConfiguration : SeededTable<PrivilegeGroup>
    {
        public override ICollection<PrivilegeGroup> SeedData
        {
            get
            {
                return new[] {
                    new PrivilegeGroup { Id = 1, Name = "Submit and Access Claims", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new PrivilegeGroup { Id = 2, Name = "Record Medical History", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new PrivilegeGroup { Id = 3, Name = "Access Medical History", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new PrivilegeGroup { Id = 4, Name = "Can be RU (OBO)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new PrivilegeGroup { Id = 5, Name = "Can be OBO (RU)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

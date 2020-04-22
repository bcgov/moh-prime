using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class VendorConfiguration : SeededTable<Vendor>
    {
        public override ICollection<Vendor> SeedData
        {
            get
            {
                return new[] {
                    new Vendor { Id = 1, Name = "Care Connect", Email = "CareConnect@phsa.ca", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Id = 2, Name = "Excelleris", Email = "support@excelleris.com", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Id = 3, Name = "iClinic", Email = "help@iclinicemr.com", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Id = 4, Name = "MediNet", Email = "prime@medinet.ca", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Id = 5, Name = "Plexia", Email = "service@plexia.ca", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

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
                    new Vendor { Code = 1, Name = "CareConnect", Email = "CareConnect@phsa.ca", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 2, Name = "Excelleris", Email = "support@excelleris.com", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 3, Name = "iClinic", Email = "help@iclinicemr.com", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 4, Name = "Medinet", Email = "prime@medinet.ca", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 5, Name = "Plexia", Email = "service@plexia.ca", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}

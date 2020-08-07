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
                    new Vendor { Code = 1, OrganizationTypeCode = OrganizationType.CommunityPractice, Name = "CareConnect", Email = "CareConnect@phsa.ca", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 2, OrganizationTypeCode = OrganizationType.CommunityPractice, Name = "Excelleris", Email = "support@excelleris.com", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 3, OrganizationTypeCode = OrganizationType.CommunityPractice, Name = "iClinic", Email = "help@iclinicemr.com", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 4, OrganizationTypeCode = OrganizationType.CommunityPractice, Name = "Medinet", Email = "prime@medinet.ca", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 5, OrganizationTypeCode = OrganizationType.CommunityPractice, Name = "Plexia", Email = "service@plexia.ca", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 6, OrganizationTypeCode = OrganizationType.CommunityPharmacy, Name = "Telus Health", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 7, OrganizationTypeCode = OrganizationType.CommunityPharmacy, Name = "Shoppers Drug Mart", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 8, OrganizationTypeCode = OrganizationType.CommunityPharmacy, Name = "Applied Robotics", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 9, OrganizationTypeCode = OrganizationType.CommunityPharmacy, Name = "McKesson", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 10, OrganizationTypeCode = OrganizationType.CommunityPharmacy, Name = "Commander Group", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                };
            }
        }
    }
}

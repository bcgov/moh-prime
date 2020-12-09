using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class VendorConfiguration : SeededTable<Vendor>
    {
        public override IEnumerable<Vendor> SeedData
        {
            get
            {
                return new[] {
                    new Vendor { Code = 1,  CareSettingCode = (int)CareSettingType.CommunityPractice, Name = "CareConnect", Email = "CareConnect@phsa.ca", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 2,  CareSettingCode = (int)CareSettingType.CommunityPractice, Name = "Excelleris", Email = "support@excelleris.com", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 3,  CareSettingCode = (int)CareSettingType.CommunityPractice, Name = "iClinic", Email = "help@iclinicemr.com", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 4,  CareSettingCode = (int)CareSettingType.CommunityPractice, Name = "Medinet", Email = "prime@medinet.ca", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 5,  CareSettingCode = (int)CareSettingType.CommunityPractice, Name = "Plexia", Email = "service@plexia.ca", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 6,  CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "PharmaClik", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 7,  CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "Nexxsys", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 8,  CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "Kroll", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 9,  CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "Assyst Rx-A", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 10, CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "WinRx", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 11, CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "Shoppers Drug Mart HealthWatch NG", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 12, CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "Commander Group", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Vendor { Code = 13, CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "BDM", Email = "", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                };
            }
        }
    }
}

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
                    new Vendor { Code = 1,  CareSettingCode = (int)CareSettingType.CommunityPractice, Name = "CareConnect",                       Email = "CareConnect@phsa.ca"    },
                    new Vendor { Code = 2,  CareSettingCode = (int)CareSettingType.CommunityPractice, Name = "Excelleris",                        Email = "support@excelleris.com" },
                    new Vendor { Code = 3,  CareSettingCode = (int)CareSettingType.CommunityPractice, Name = "iClinic",                           Email = "help@iclinicemr.com"    },
                    new Vendor { Code = 4,  CareSettingCode = (int)CareSettingType.CommunityPractice, Name = "Medinet",                           Email = "prime@medinet.ca"       },
                    new Vendor { Code = 5,  CareSettingCode = (int)CareSettingType.CommunityPractice, Name = "Plexia",                            Email = "service@plexia.ca"      },

                    new Vendor { Code = 6,  CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "PharmaClik",                        Email = ""                       },
                    new Vendor { Code = 7,  CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "Nexxsys",                           Email = ""                       },
                    new Vendor { Code = 8,  CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "Kroll",                             Email = ""                       },
                    new Vendor { Code = 9,  CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "Assyst Rx-A",                       Email = ""                       },
                    new Vendor { Code = 10, CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "WinRx",                             Email = ""                       },
                    new Vendor { Code = 11, CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "Shoppers Drug Mart HealthWatch NG", Email = ""                       },
                    new Vendor { Code = 12, CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "Commander Group",                   Email = ""                       },
                    new Vendor { Code = 13, CareSettingCode = (int)CareSettingType.CommunityPharmacy, Name = "BDM",                               Email = ""                       },

                    new Vendor { Code = 14, CareSettingCode = (int)CareSettingType.DeviceProvider,    Name = "Assyst Rx-A",                       Email = ""                       },
                    new Vendor { Code = 15, CareSettingCode = (int)CareSettingType.DeviceProvider,    Name = "Commander Group",                   Email = ""                       },
                    new Vendor { Code = 16, CareSettingCode = (int)CareSettingType.DeviceProvider,    Name = "Kroll",                             Email = ""                       },
                    new Vendor { Code = 17, CareSettingCode = (int)CareSettingType.DeviceProvider,    Name = "Nexxsys",                           Email = ""                       },
                    new Vendor { Code = 18, CareSettingCode = (int)CareSettingType.DeviceProvider,    Name = "PharmaClik",                        Email = ""                       },
                    new Vendor { Code = 19, CareSettingCode = (int)CareSettingType.DeviceProvider,    Name = "Shoppers Drug Mart HealthWatch NG", Email = ""                       },
                    new Vendor { Code = 20, CareSettingCode = (int)CareSettingType.DeviceProvider,    Name = "WinRx",                             Email = ""                       },

                    new Vendor { Code = 21, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "CareConnect",                       Email = ""                       },
                    new Vendor { Code = 22, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "Excelleris",                        Email = ""                       },
                    new Vendor { Code = 23, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "iClinic",                           Email = ""                       },
                    new Vendor { Code = 24, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "Medinet",                           Email = ""                       },
                    new Vendor { Code = 25, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "Plexia",                            Email = ""                       },
                    new Vendor { Code = 26, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "PharmaClik",                        Email = ""                       },
                    new Vendor { Code = 27, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "Nexxsys",                           Email = ""                       },
                    new Vendor { Code = 28, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "Kroll",                             Email = ""                       },
                    new Vendor { Code = 29, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "Assyst Rx-A",                       Email = ""                       },
                    new Vendor { Code = 30, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "WinRx",                             Email = ""                       },
                    new Vendor { Code = 31, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "Shoppers Drug Mart HealthWatch NG", Email = ""                       },
                    new Vendor { Code = 32, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "Commander Group",                   Email = ""                       },
                    new Vendor { Code = 33, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "BDM",                               Email = ""                       },
                    new Vendor { Code = 34, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "Meditech",                          Email = ""                       },
                    new Vendor { Code = 35, CareSettingCode = (int)CareSettingType.HealthAuthority,   Name = "Cerner",                            Email = ""                       },
                };
            }
        }
    }
}

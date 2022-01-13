using System.Collections.Generic;
using Prime.Models.Plr;

namespace Prime.Configuration.Database
{
    /// <summary>
    /// PLR currently doesn't collect data for some Role Types (e.g. MOA) and/or
    /// some Role Types (e.g. KN) don't have colleges associated with them.
    /// </summary>
    public class CollegeForPlrRoleTypeConfiguration : SeededTable<CollegeForPlrRoleType>
    {
        public override IEnumerable<CollegeForPlrRoleType> SeedData
        {
            get
            {
                return new[] {
                    new CollegeForPlrRoleType { ProviderRoleType = "RN",     CollegeCode = 3  },
                    new CollegeForPlrRoleType { ProviderRoleType = "RNP",    CollegeCode = 3  },
                    new CollegeForPlrRoleType { ProviderRoleType = "RPN",    CollegeCode = 3  },
                    new CollegeForPlrRoleType { ProviderRoleType = "PHARM",  CollegeCode = 2  },
                    new CollegeForPlrRoleType { ProviderRoleType = "PO",     CollegeCode = 1  },
                    new CollegeForPlrRoleType { ProviderRoleType = "RAC",    CollegeCode = 18 },
                    new CollegeForPlrRoleType { ProviderRoleType = "RM",     CollegeCode = 3  },
                    new CollegeForPlrRoleType { ProviderRoleType = "LPN",    CollegeCode = 3  },
                    new CollegeForPlrRoleType { ProviderRoleType = "MD",     CollegeCode = 1  },
                    new CollegeForPlrRoleType { ProviderRoleType = "OPT",    CollegeCode = 14 },
                    new CollegeForPlrRoleType { ProviderRoleType = "DEN",    CollegeCode = 7  },
                    new CollegeForPlrRoleType { ProviderRoleType = "OT",     CollegeCode = 12 },
                    new CollegeForPlrRoleType { ProviderRoleType = "PSYCH",  CollegeCode = 16 },
                    new CollegeForPlrRoleType { ProviderRoleType = "CHIRO",  CollegeCode = 4  },
                    new CollegeForPlrRoleType { ProviderRoleType = "PHYSIO", CollegeCode = 15 },
                    new CollegeForPlrRoleType { ProviderRoleType = "RMT",    CollegeCode = 10 },
                    new CollegeForPlrRoleType { ProviderRoleType = "PTECH",  CollegeCode = 2  },
                    new CollegeForPlrRoleType { ProviderRoleType = "RD",     CollegeCode = 9  },
                    new CollegeForPlrRoleType { ProviderRoleType = "ND",     CollegeCode = 11 }
                };
            }
        }
    }
}

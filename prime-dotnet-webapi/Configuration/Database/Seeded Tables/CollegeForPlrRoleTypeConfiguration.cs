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
                    new CollegeForPlrRoleType { RoleTypeCode = "RN",     CollegeId = 3  },
                    new CollegeForPlrRoleType { RoleTypeCode = "RNP",    CollegeId = 3  },
                    new CollegeForPlrRoleType { RoleTypeCode = "RPN",    CollegeId = 3  },
                    new CollegeForPlrRoleType { RoleTypeCode = "PHARM",  CollegeId = 2  },
                    new CollegeForPlrRoleType { RoleTypeCode = "PO",     CollegeId = 1  },
                    new CollegeForPlrRoleType { RoleTypeCode = "RAC",    CollegeId = 18 },
                    new CollegeForPlrRoleType { RoleTypeCode = "RM",     CollegeId = 3  },
                    new CollegeForPlrRoleType { RoleTypeCode = "LPN",    CollegeId = 3  },
                    new CollegeForPlrRoleType { RoleTypeCode = "MD",     CollegeId = 1  },
                    new CollegeForPlrRoleType { RoleTypeCode = "OPT",    CollegeId = 14 },
                    new CollegeForPlrRoleType { RoleTypeCode = "DEN",    CollegeId = 7  },
                    new CollegeForPlrRoleType { RoleTypeCode = "OT",     CollegeId = 12 },
                    new CollegeForPlrRoleType { RoleTypeCode = "PSYCH",  CollegeId = 16 },
                    new CollegeForPlrRoleType { RoleTypeCode = "CHIRO",  CollegeId = 4  },
                    new CollegeForPlrRoleType { RoleTypeCode = "PHYSIO", CollegeId = 15 },
                    new CollegeForPlrRoleType { RoleTypeCode = "RMT",    CollegeId = 10 },
                    new CollegeForPlrRoleType { RoleTypeCode = "PTECH",  CollegeId = 2  },
                    new CollegeForPlrRoleType { RoleTypeCode = "RD",     CollegeId = 9  },
                    new CollegeForPlrRoleType { RoleTypeCode = "NAP",    CollegeId = 11 }
                };
            }
        }
    }
}

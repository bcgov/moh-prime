using System.Collections.Generic;
using Prime.Models.Plr;

namespace Prime.Configuration
{
    public class PlrStatusReasonConfiguration : SeededTable<PlrStatusReason>
    {
        public override IEnumerable<PlrStatusReason> SeedData
        {
            get
            {
                return new[] {
                    new PlrStatusReason { Code = "OOP",       Name = "Out of Province" },
                    new PlrStatusReason { Code = "ORG",       Name = "Organization Provider" },
                    new PlrStatusReason { Code = "DEF",       Name = "Deferred" },
                    new PlrStatusReason { Code = "DEN",       Name = "Licensed Denied" },
                    new PlrStatusReason { Code = "ERSRES",    Name = "Erased by Resolution" },
                    new PlrStatusReason { Code = "GS",        Name = "Good Standing" },
                    new PlrStatusReason { Code = "HON",       Name = "Honorary" },
                    new PlrStatusReason { Code = "INNONPRAC", Name = "Initial Non Practicing" },
                    new PlrStatusReason { Code = "MIS",       Name = "Missionary" },
                    new PlrStatusReason { Code = "NR",        Name = "Non-resident" },
                    new PlrStatusReason { Code = "OPEN",      Name = "Open" },
                    new PlrStatusReason { Code = "PRAC",      Name = "Practising" },
                    new PlrStatusReason { Code = "REM",       Name = "Removed" },
                    new PlrStatusReason { Code = "RESDISC",   Name = "Resigned - disciplinary action" },
                    new PlrStatusReason { Code = "SPE",       Name = "Special Registry" },
                    new PlrStatusReason { Code = "SUS",       Name = "Suspended" },
                    new PlrStatusReason { Code = "TEMPPER",   Name = "Temporary Permit" },
                    new PlrStatusReason { Code = "TI",        Name = "Temporary Inactive" },
                    new PlrStatusReason { Code = "TSF",       Name = "Transfer" },
                    new PlrStatusReason { Code = "UNK",       Name = "Unknown" },
                    new PlrStatusReason { Code = "VW",        Name = "Voluntary Withdrawal" },
                    new PlrStatusReason { Code = "ASSOC",     Name = "Associate" },
                    new PlrStatusReason { Code = "AU",        Name = "Address Unknown" },
                    new PlrStatusReason { Code = "CLOSED",    Name = "Closed" },
                    new PlrStatusReason { Code = "NONPRAC",   Name = "Non Practicing" },
                    new PlrStatusReason { Code = "LAP",       Name = "License Lapsed on Request" },
                    new PlrStatusReason { Code = "LTP",       Name = "Left the Province" },
                    new PlrStatusReason { Code = "NONPAY",    Name = "Non Payment of Fee" },
                    new PlrStatusReason { Code = "RET",       Name = "Retired" },
                    new PlrStatusReason { Code = "DEC",       Name = "Deceased" },
                    new PlrStatusReason { Code = "MEDSTUD",   Name = "Medical Student" }
                };
            }
        }
    }
}

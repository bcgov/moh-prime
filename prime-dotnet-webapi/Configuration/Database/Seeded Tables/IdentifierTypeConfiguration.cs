using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class IdentifierTypeConfiguration : SeededTable<IdentifierType>
    {
        /// <summary>
        /// This was populated from the Excel spreadsheet "Appendix-A-BC-PLR-OID-list.xls" obtained from Sekhon, Khushwinder (Vinder) of the PLR team
        /// </summary>
        public override IEnumerable<IdentifierType> SeedData
        {
            get
            {
                return new[] {
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.19", Name = "RNID"     },
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.20", Name = "RNPID"    },
                    new IdentifierType { Code = "2.16.840.1.113883.4.608",     Name = "RPNRC"    },
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.14", Name = "PHID"     },
                    new IdentifierType { Code = "2.16.840.1.113883.4.454",     Name = "RACID"    },
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.18", Name = "RMID"     },
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.10", Name = "LPNID"    },
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.4",  Name = "CPSID"    },
                    new IdentifierType { Code = "2.16.840.1.113883.4.429",     Name = "OPTID"    },
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.6",  Name = "DENID"    },
                    new IdentifierType { Code = "2.16.840.1.113883.4.363",     Name = "CCID"     },
                    new IdentifierType { Code = "2.16.840.1.113883.4.364",     Name = "OTID"     },
                    new IdentifierType { Code = "2.16.840.1.113883.4.362",     Name = "PSYCHID"  },
                    new IdentifierType { Code = "2.16.840.1.113883.4.361",     Name = "SWID"     },
                    new IdentifierType { Code = "2.16.840.1.113883.4.422",     Name = "CHIROID"  },
                    new IdentifierType { Code = "2.16.840.1.113883.4.414",     Name = "PHYSIOID" },
                    new IdentifierType { Code = "2.16.840.1.113883.4.433",     Name = "RMTID"    },
                    new IdentifierType { Code = "2.16.840.1.113883.4.439",     Name = "KNID"     },
                    new IdentifierType { Code = "2.16.840.1.113883.4.401",     Name = "PHTID"    },
                    new IdentifierType { Code = "2.16.840.1.113883.4.477",     Name = "COUNID"   },
                    new IdentifierType { Code = "2.16.840.1.113883.4.452",     Name = "MFTID"    },
                    new IdentifierType { Code = "2.16.840.1.113883.4.530",     Name = "RDID"     },
                };
            }
        }
    }
}

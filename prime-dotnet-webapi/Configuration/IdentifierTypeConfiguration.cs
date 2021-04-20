using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
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
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.19", Name = "RNID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.20", Name = "RNPID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.608",     Name = "RPNRC", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.14", Name = "PHID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.454",     Name = "RACID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.18", Name = "RMID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.10", Name = "LPNID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.4",  Name = "CPSID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.429",     Name = "OPTID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.3.40.2.6",  Name = "DENID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.363",     Name = "CCID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.364",     Name = "OTID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.362",     Name = "PSYCHID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.361",     Name = "SWID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.422",     Name = "CHIROID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.414",     Name = "PHYSIOID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.433",     Name = "RMTID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.439",     Name = "KNID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.401",     Name = "PHTID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.477",     Name = "COUNID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.452",     Name = "MFTID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new IdentifierType { Code = "2.16.840.1.113883.4.530",     Name = "RDID", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                };
            }
        }
    }
}

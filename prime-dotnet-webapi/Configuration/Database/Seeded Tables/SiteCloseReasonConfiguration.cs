using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class SiteCloseReasonConfiguration : SeededTable<SiteCloseReason>
    {
        public override IEnumerable<SiteCloseReason> SeedData
        {
            get
            {
                return [
                    new SiteCloseReason { Code = 1, Name = "Licence cancelled" },
                    new SiteCloseReason { Code = 2, Name = "Closed by Organization" },
                    new SiteCloseReason { Code = 3, Name = "Other" },
                ];
            }
        }
    }
}

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
                return new[] {
                    new SiteCloseReason { Code = 1, Name = "Reason 1" },
                    new SiteCloseReason { Code = 2, Name = "Reason 2" },
                };
            }
        }
    }
}

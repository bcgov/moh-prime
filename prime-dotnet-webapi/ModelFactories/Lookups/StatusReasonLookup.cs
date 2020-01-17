using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class StatusReasonLookup
    {
        private static ICollection<StatusReason> _seedData = new StatusReasonConfiguration().SeedData;

        public static ICollection<StatusReason> All { get { return _seedData; } }
        public static StatusReason Automatic
        {
            get { return _seedData.Single(x => x.Code == StatusReason.AUTOMATIC_CODE); }
        }
    }
}

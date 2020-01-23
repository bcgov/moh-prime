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

        public static IEnumerable<StatusReason> ManualReasons
        {
            get { return All.Where(x => x != Automatic && x != Manual); }
        }

        public static StatusReason Automatic
        {
            get { return All.Single(x => x.Code == StatusReason.AUTOMATIC_CODE); }
        }

        public static StatusReason Manual
        {
            get { return All.Single(x => x.Code == StatusReason.MANUAL_CODE); }
        }
    }
}

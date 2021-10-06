using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class StatusReasonLookup
    {
        private static IEnumerable<StatusReason> _seedData = new StatusReasonConfiguration().SeedData;

        public static IEnumerable<StatusReason> All { get { return _seedData; } }

        public static IEnumerable<StatusReason> ManualReasons
        {
            get { return All.Where(x => x != Automatic && x != Manual); }
        }

        public static StatusReason Automatic
        {
            get { return All.Single(x => x.Code == (int)StatusReasonType.Automatic); }
        }

        public static StatusReason Manual
        {
            get { return All.Single(x => x.Code == (int)StatusReasonType.Manual); }
        }
    }
}

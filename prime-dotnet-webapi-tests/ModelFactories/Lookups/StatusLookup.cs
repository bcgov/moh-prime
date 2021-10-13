using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class StatusLookup
    {
        private static IEnumerable<Status> _seedData = new StatusConfiguration().SeedData;

        public static IEnumerable<Status> All { get { return _seedData; } }

        public static Status ByCode(int statusCode)
        {
            return All.Single(x => x.Code == statusCode);
        }

        public static Status InProgress
        {
            get { return ByCode((int)StatusType.Editable); }
        }
    }
}

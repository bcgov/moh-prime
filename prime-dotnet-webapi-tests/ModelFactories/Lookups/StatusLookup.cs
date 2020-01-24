using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class StatusLookup
    {
        private static ICollection<Status> _seedData = new StatusConfiguration().SeedData;

        public static ICollection<Status> All { get { return _seedData; } }

        public static Status ByCode(short statusCode)
        {
            return All.Single(x => x.Code == statusCode);
        }

        public static Status InProgress
        {
            get { return ByCode(Status.IN_PROGRESS_CODE); }
        }
    }
}

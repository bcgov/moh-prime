using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class StatusLookup
    {
        private static ICollection<Status> _seedData = new StatusConfiguration().SeedData;

        public static ICollection<Status> All { get { return _seedData; } }
        public static Status AcceptedTos
        {
            get { return _seedData.Single(x => x.Code == Status.ACCEPTED_TOS_CODE); }
        }
    }
}

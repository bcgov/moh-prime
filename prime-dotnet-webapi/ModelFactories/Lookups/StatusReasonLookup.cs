using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class StatusReasonLookup
    {
        private static ICollection<StatusReason> _seedData = new StatusReasonConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<StatusReason> All { get { return _seedData; } }

        public static IEnumerable<StatusReason> ManualReasons
        {
            get { return _seedData.Where(x => x != Automatic && x != Manual); }
        }

        public static StatusReason Automatic
        {
            get { return _seedData.Single(x => x.Code == StatusReason.AUTOMATIC_CODE); }
        }

        public static StatusReason Manual
        {
            get { return _seedData.Single(x => x.Code == StatusReason.MANUAL_CODE); }
        }
    }
}

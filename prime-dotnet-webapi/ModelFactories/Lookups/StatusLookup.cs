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

        public static Status ByCode(short statusCode)
        {
            return _seedData.Single(x => x.Code == statusCode);
        }

        public static Status InProgress
        {
            get { return _seedData.Single(x => x.Code == Status.IN_PROGRESS_CODE); }
        }
        public static Status Submitted
        {
            get { return _seedData.Single(x => x.Code == Status.SUBMITTED_CODE); }
        }
        public static Status Approved
        {
            get { return _seedData.Single(x => x.Code == Status.APPROVED_CODE); }
        }
        public static Status AcceptedTos
        {
            get { return _seedData.Single(x => x.Code == Status.ACCEPTED_TOS_CODE); }
        }
        public static Status DeclinedTos
        {
            get { return _seedData.Single(x => x.Code == Status.DECLINED_TOS_CODE); }
        }
    }
}

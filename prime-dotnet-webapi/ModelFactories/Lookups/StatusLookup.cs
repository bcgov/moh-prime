using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class StatusLookup
    {
        private static ICollection<Status> _seedData = new StatusConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<Status> All { get { return _seedData; } }

        public static Status ByCode(short statusCode)
        {
            return _seedData.Single(x => x.Code == statusCode);
        }

        public static Status InProgress
        {
            get { return _seedData.Single(x => x.Code == Status.IN_PROGRESS_CODE); }
        }

        public static IEnumerable<Status> SubmittedSequence
        {
            get { return new[] { Status.IN_PROGRESS_CODE, Status.SUBMITTED_CODE }.Select(ByCode); }
        }

        public static IEnumerable<Status> ApprovedSequence
        {
            get { return new[] { Status.IN_PROGRESS_CODE, Status.SUBMITTED_CODE, Status.APPROVED_CODE }.Select(ByCode); }
        }

        public static IEnumerable<Status> AcceptedTosSequence
        {
            get { return new[] { Status.IN_PROGRESS_CODE, Status.SUBMITTED_CODE, Status.APPROVED_CODE, Status.ACCEPTED_TOS_CODE }.Select(ByCode); }
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class StatusState
    {
        public static readonly StatusState Active = new StatusState(Status.ACTIVE_CODE, Status.UNDER_REVIEW_CODE);
        public static readonly StatusState UnderReview = new StatusState(Status.ACTIVE_CODE, Status.UNDER_REVIEW_CODE);
        public static readonly StatusState RequiresTOA = new StatusState(Status.ACTIVE_CODE, Status.UNDER_REVIEW_CODE);
        public static readonly StatusState Locked = new StatusState(Status.ACTIVE_CODE, Status.UNDER_REVIEW_CODE, Status.LOCKED_CODE);

        public static ICollection<StatusState> States { get; private set; }

        public IEnumerable<Status> Statuses { get; private set; }

        private StatusState(params short[] statusCodes)
        {
            Statuses = statusCodes.Select(code => StatusLookup.ByCode(code));

            if (States == null) { States = new List<StatusState>(); }
            States.Add(this);
        }
    }

    public class StatusStateFactory
    {
        private Enrollee _owner;
        private IEnumerable<Status> _statuses;
        private bool _automatic;

        public StatusStateFactory(Enrollee owner, Faker faker)
        {
            _owner = owner;
            _statuses = faker.PickRandom(StatusState.States).Statuses;
        }

        public StatusStateFactory(Enrollee owner, StatusState state)
        {
            _owner = owner;
            _statuses = state.Statuses;
        }

        public StatusStateFactory Automatic()
        {
            _automatic = true;
            return this;
        }

        public IEnumerable<EnrolmentStatus> Generate()
        {
            var enrolmentStatuses = new EnrolmentStatusFactory(_owner, _statuses).Generate(_statuses.Count());

            var approvedStatus = enrolmentStatuses.SingleOrDefault(s => s.StatusCode == Status.REQUIRES_TOA_CODE);
            if (approvedStatus != null) { approvedStatus.AddStatusReason(_automatic ? StatusReason.AUTOMATIC_CODE : StatusReason.MANUAL_CODE); }

            var pharmanetStatus = enrolmentStatuses.FindLast(s => new[] { Status.LOCKED_CODE, Status.ACTIVE_CODE }.Contains(s.StatusCode));
            if (pharmanetStatus != null) { pharmanetStatus.PharmaNetStatus = true; }

            return enrolmentStatuses;
        }
    }
}

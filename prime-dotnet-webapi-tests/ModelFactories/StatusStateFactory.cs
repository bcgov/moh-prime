using System;
using System.Linq;
using System.Collections.Generic;
using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class StatusState
    {
        public static readonly StatusState Submitted = new StatusState(Status.IN_PROGRESS_CODE, Status.SUBMITTED_CODE);
        public static readonly StatusState Approved = new StatusState(Status.IN_PROGRESS_CODE, Status.SUBMITTED_CODE, Status.APPROVED_CODE);
        public static readonly StatusState Declined = new StatusState(Status.IN_PROGRESS_CODE, Status.SUBMITTED_CODE, Status.DECLINED_CODE);
        public static readonly StatusState AcceptedTos = new StatusState(Status.IN_PROGRESS_CODE, Status.SUBMITTED_CODE, Status.APPROVED_CODE, Status.ACCEPTED_TOS_CODE);
        public static readonly StatusState DeclinedTos = new StatusState(Status.IN_PROGRESS_CODE, Status.SUBMITTED_CODE, Status.APPROVED_CODE, Status.DECLINED_TOS_CODE);
        public static readonly StatusState Unlocked = new StatusState(Status.IN_PROGRESS_CODE, Status.SUBMITTED_CODE, Status.IN_PROGRESS_CODE);
        public static readonly StatusState SecondSubmission = new StatusState(Status.IN_PROGRESS_CODE, Status.SUBMITTED_CODE, Status.IN_PROGRESS_CODE, Status.SUBMITTED_CODE);

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

            var approvedStatus = enrolmentStatuses.SingleOrDefault(s => s.StatusCode == Status.APPROVED_CODE);
            if (approvedStatus != null) { approvedStatus.AddStatusReason(_automatic ? StatusReason.AUTOMATIC_CODE : StatusReason.MANUAL_CODE); }

            var pharmanetStatus = enrolmentStatuses.FindLast(s => new[] { Status.DECLINED_CODE, Status.ACCEPTED_TOS_CODE, Status.DECLINED_CODE }.Contains(s.StatusCode));
            if (pharmanetStatus != null) { pharmanetStatus.PharmaNetStatus = true; }

            return enrolmentStatuses;
        }
    }
}

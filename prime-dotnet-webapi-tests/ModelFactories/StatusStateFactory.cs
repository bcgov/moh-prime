using System;
using System.Linq;
using System.Collections.Generic;
using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class StatusState
    {
        public static readonly StatusState Submitted = new StatusState(StatusType.Editable, StatusType.UnderReview);
        public static readonly StatusState Approved = new StatusState(StatusType.Editable, StatusType.UnderReview, StatusType.RequiresToa);
        public static readonly StatusState Declined = new StatusState(StatusType.Editable, StatusType.UnderReview, StatusType.Locked);
        public static readonly StatusState PassedTos = new StatusState(StatusType.Editable, StatusType.UnderReview, StatusType.RequiresToa, StatusType.Editable);
        public static readonly StatusState Unlocked = new StatusState(StatusType.Editable, StatusType.UnderReview, StatusType.Editable);
        public static readonly StatusState SecondSubmission = new StatusState(StatusType.Editable, StatusType.UnderReview, StatusType.Editable, StatusType.UnderReview);

        public static ICollection<StatusState> States { get; private set; }

        public IEnumerable<Status> Statuses { get; private set; }

        private StatusState(params StatusType[] statusTypes)
        {
            Statuses = statusTypes.Select(type => StatusLookup.ByCode((int)type));

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

            var approvedStatus = enrolmentStatuses.SingleOrDefault(s => s.IsType(StatusType.RequiresToa));
            if (approvedStatus != null) { approvedStatus.AddStatusReason(_automatic ? StatusReasonType.Automatic : StatusReasonType.Manual); }

            return enrolmentStatuses;
        }
    }
}

using System.Linq;
using System.Collections.Generic;
using Bogus;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class EnrolmentStatusFactory : Faker<EnrolmentStatus>
    {
        private static int IdCounter = 1;

        private IEnumerator<Status> _statusEnumerator;

        public EnrolmentStatusFactory(Enrollee owner) : this(owner, Enumerable.Empty<Status>()) { }

        public EnrolmentStatusFactory(Enrollee owner, IEnumerable<Status> statuses)
        {
            _statusEnumerator = statuses.GetEnumerator();

            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.Status, f => GetNextStatus() ?? f.PickRandom(StatusLookup.All));
            RuleFor(x => x.StatusCode, (f, x) => x.Status.Code);
            RuleFor(x => x.StatusDate, f => f.Date.Past());
            RuleFor(x => x.PharmaNetStatus, false);
            RuleFor(x => x.EnrolmentStatusReasons, f => null);

            RuleSet("inProgress", (set) =>
            {
                RuleFor(x => x.Status, f => StatusLookup.InProgress);
                RuleFor(x => x.StatusCode, (f, x) => x.Status.Code);

            });
        }

        private Status GetNextStatus()
        {
            return _statusEnumerator.MoveNext() ? _statusEnumerator.Current : null;
        }
    }
}

using System;
using Bogus;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class EnrolmentStatusFactory : Faker<EnrolmentStatus>
    {
        private static int IdCounter = 1;

        public EnrolmentStatusFactory(Enrollee owner)
        {
            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.Status, f => f.PickRandom(StatusLookup.All));
            RuleFor(x => x.StatusCode, (f, x) => x.Status.Code);
            RuleFor(x => x.StatusDate, f => f.Date.Past());
            RuleFor(x => x.PharmaNetStatus, false);
            RuleFor(x => x.EnrolmentStatusReasons, f => null);
        }
    }
}

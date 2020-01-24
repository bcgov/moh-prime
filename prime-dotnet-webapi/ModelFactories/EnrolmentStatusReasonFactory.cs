using System;
using Bogus;
using Bogus.Extensions;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class EnrolmentStatusReasonFactory : Faker<EnrolmentStatusReason>
    {
        private static int IdCounter = 1;

        public EnrolmentStatusReasonFactory(EnrolmentStatus owner)
        {
            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.EnrolmentStatus, f => owner);
            RuleFor(x => x.EnrolmentStatusId, f => owner.Id);
            RuleFor(x => x.StatusReasonCode, f => f.PickRandom(StatusReasonLookup.All).Code);
            RuleFor(x => x.ReasonNote, f => f.Lorem.Paragraph(1).OrNull(f));

            Ignore(x => x.StatusReason);

            RuleSet("manualReason", (set) =>
            {
                set.RuleFor(x => x.StatusReasonCode, f => f.PickRandom(StatusReasonLookup.ManualReasons).Code);
            });
            RuleSet("manual", (set) =>
            {
                set.RuleFor(x => x.StatusReasonCode, f => StatusReasonLookup.Manual.Code);
            });
            RuleSet("automatic", (set) =>
            {
                set.RuleFor(x => x.StatusReasonCode, f => StatusReasonLookup.Automatic.Code);
            });
        }
    }
}

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
            RuleFor(x => x.StatusReason, f => f.PickRandom(StatusReasonLookup.All));
            RuleFor(x => x.StatusReasonCode, (f, x) => x.StatusReason.Code);
            RuleFor(x => x.ReasonNote, f => f.Lorem.Paragraph(1).OrNull(f));

            RuleSet("manualReason", (set) =>
            {
                set.RuleFor(x => x.StatusReason, f => f.PickRandom(StatusReasonLookup.ManualReasons));
                set.RuleFor(x => x.StatusReasonCode, (f, x) => x.StatusReason.Code);
            });
            RuleSet("manual", (set) =>
            {
                set.RuleFor(x => x.StatusReason, f => StatusReasonLookup.Manual);
                set.RuleFor(x => x.StatusReasonCode, (f, x) => x.StatusReason.Code);
            });
            RuleSet("automatic", (set) =>
            {
                set.RuleFor(x => x.StatusReason, f => StatusReasonLookup.Automatic);
                set.RuleFor(x => x.StatusReasonCode, (f, x) => x.StatusReason.Code);
            });
        }
    }
}

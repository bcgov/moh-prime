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
            RuleFor(x => x.StatusReason, f => f.PickRandom(StatusReasonLookup.All));
            RuleFor(x => x.ReasonNote, f => f.Lorem.Paragraph(1).OrNull(f));

            FinishWith((f, x) =>
            {
                x.EnrolmentStatusId = x.EnrolmentStatus.Id;
                x.StatusReasonCode = x.StatusReason.Code;
            });
        }
    }
}

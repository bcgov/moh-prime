using System;
using Bogus;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class EnrolmentStatusReasonFactory : Faker<EnrolmentStatusReason>
    {
        private static int IdCounter = 1;

        public EnrolmentStatusReasonFactory()
        {
            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter++);

        }
    }
}
// TODO
//   EnrolmentStatusId
//  EnrolmentStatus
//  StatusReasonCode
//  StatusReason
// ReasonNote


using System;
using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class EnrolleeAbsenceFactory : Faker<EnrolleeAbsence>
    {
        private static int IdCounter = 1;

        public EnrolleeAbsenceFactory(Enrollee owner)
        {
//            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.StartTimestamp, f => f.Date.Future());
            RuleFor(x => x.EndTimestamp, f => f.Date.Past());
        }
    }
}

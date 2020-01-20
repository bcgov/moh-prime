using System;
using Bogus;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class OrganizationFactory : Faker<Organization>
    {
        private static int IdCounter = 1;

        public OrganizationFactory(Enrollee owner)
        {
            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.OrganizationType, f => f.PickRandom(OrganizationTypeLookup.All));
            RuleFor(x => x.OrganizationTypeCode, (f, x) => x.OrganizationType.Code);
        }
    }
}

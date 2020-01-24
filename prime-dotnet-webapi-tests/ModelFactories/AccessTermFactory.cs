using System;
using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class AccessTermFactory : Faker<AccessTerm>
    {
        // private static int IdCounter = 1;

        // TODO awaiting finalization of dynamic access agreement
        public AccessTermFactory(Enrollee owner)
        {
            throw new NotImplementedException("");

            // this.SetBaseRules();

            // RuleFor(x => x.Id, f => IdCounter++);
            // RuleFor(x => x.Enrollee, f => owner);
            // RuleFor(x => x.EnrolleeId, f => owner.Id);
            // RuleFor(x => x.GlobalClause, f => f.PickRandom(GlobalClauseLookup.All));
            // RuleFor(x => x.GlobalClauseId, (f, x) => x.GlobalClause.Id);
            // RuleFor(x => x.UserClause, f => f.PickRandom(UserClauseLookup.All));
            // RuleFor(x => x.UserClauseId, (f, x) => x.UserClause.Id);
            // RuleFor(x => x.AccessTermLicenseClassClauses,  => );
            // RuleFor(x => x.LimitsConditionsClause, f => );
            // LimitsConditionsClauseId
            // RuleFor(x => x.CreatedDate, f => f.Date.Past());
            // RuleFor(x => x.AcceptedDate, (f, x) => x.CreatedDate.AddDays(f.Random.Int(0, 5)));
        }
    }
}

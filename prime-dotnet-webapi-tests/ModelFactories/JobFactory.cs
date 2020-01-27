using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class JobFactory : Faker<Job>
    {
        private static int IdCounter = 1;

        public JobFactory(Enrollee owner)
        {
            this.SetBaseRules();

            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.Enrollee, () => owner);
            RuleFor(x => x.EnrolleeId, () => owner.Id);
            RuleFor(x => x.Title, f => f.Name.JobTitle().OrOther(f, f.PickRandom(JobNameLookup.All).Name));
        }
    }
}

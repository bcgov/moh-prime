using Bogus;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class JobFactory : Faker<Job>
    {
        private static int IdCounter = 1;

        public JobFactory(Enrollee owner)
        {
            StrictMode(true);
            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.Enrollee, () => owner);
            RuleFor(x => x.EnrolleeId, () => owner.Id);
            RuleFor(x => x.Title, f => f.PickRandom(JobNameLookup.All).Name);
        }
    }
}

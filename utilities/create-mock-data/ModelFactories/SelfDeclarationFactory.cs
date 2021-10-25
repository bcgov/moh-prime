using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class SelfDeclarationFactory : Faker<SelfDeclaration>
    {
        private static int IdCounter = 1;

        public SelfDeclarationFactory(Enrollee owner)
        {
//            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.SelfDeclarationDetails, f => f.Lorem.Paragraphs(2));
            RuleFor(x => x.SelfDeclarationTypeCode, f => f.PickRandom(SelfDeclarationTypeLookup.All).Code);

            Ignore(x => x.SelfDeclarationType);
            Ignore(x => x.DocumentGuids);
        }
    }
}

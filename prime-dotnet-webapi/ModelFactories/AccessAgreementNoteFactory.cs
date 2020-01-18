using Bogus;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class AccessAgreementNoteFactory : Faker<AccessAgreementNote>
    {
        private static int IdCounter = 1;

        public AccessAgreementNoteFactory(Enrollee owner)
        {
            this.SetBaseRules();

            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.Enrollee, () => owner);
            RuleFor(x => x.EnrolleeId, () => owner.Id);
            RuleFor(x => x.Note, f => f.Lorem.Paragraph(1));
            RuleFor(x => x.NoteDate, f => f.Date.Past());
        }
    }
}

using Bogus;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class EnrolmentCertificateNoteFactory : Faker<EnrolmentCertificateNote>
    {
        private static int IdCounter = 1;

        public EnrolmentCertificateNoteFactory(Enrollee owner)
        {
            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.Note, f => f.Lorem.Paragraph(1));
            RuleFor(x => x.NoteDate, f => f.Date.Past());
        }
    }
}

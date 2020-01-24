using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class AccessAgreementNoteFactory : NoteFactory<AccessAgreementNote>
    {
        public AccessAgreementNoteFactory(Enrollee owner) : base(owner) { }
    }

    public class AdjudicatorNoteFactory : NoteFactory<AdjudicatorNote>
    {
        public AdjudicatorNoteFactory(Enrollee owner) : base(owner) { }
    }

    public class EnrolmentCertificateNoteFactory : NoteFactory<EnrolmentCertificateNote>
    {
        public EnrolmentCertificateNoteFactory(Enrollee owner) : base(owner) { }
    }

    public abstract class NoteFactory<T> : Faker<T> where T : BaseAuditable, IEnrolleeNote
    {
        private static int IdCounter = 1;

        public NoteFactory(Enrollee owner)
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

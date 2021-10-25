using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class AccessAgreementNoteFactory : NoteFactory<AccessAgreementNote>
    {
        public AccessAgreementNoteFactory(Enrollee owner) : base(owner) { }
    }

    public class EnrolleeNoteFactory : NoteFactory<EnrolleeNote>
    {
        public EnrolleeNoteFactory(Enrollee owner) : base(owner)
        {
            Ignore(x => x.Adjudicator);
            Ignore(x => x.AdjudicatorId);
            Ignore(x => x.EnrolmentStatusReference);
            Ignore(x => x.EnrolleeNotification);
        }
    }

    public abstract class NoteFactory<T> : Faker<T> where T : BaseAdjudicatorNote, IBaseEnrolleeNote
    {
        private static int IdCounter = 1;

        public NoteFactory(Enrollee owner)
        {
//            this.SetBaseRules();

//            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.Enrollee, () => owner);
            RuleFor(x => x.EnrolleeId, () => owner.Id);
            RuleFor(x => x.Note, f => f.Lorem.Paragraph(1));
            RuleFor(x => x.NoteDate, f => f.Date.Past());
        }
    }
}

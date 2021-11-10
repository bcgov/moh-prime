using System;

namespace Prime.ViewModels
{
    public class AccessAgreementNoteViewModel
    {
        public int Id { get; set; }
        public int EnrolleeId { get; set; }
        public int AdjudicatorId { get; set; }
        public string Note { get; set; }
        public DateTimeOffset NoteDate { get; set; }
    }
}

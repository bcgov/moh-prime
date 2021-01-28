using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolleeNoteViewModel
    {
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        public int AdjudicatorId { get; set; }

        public Admin Adjudicator { get; set; }

        public string Note { get; set; }

        public DateTimeOffset NoteDate { get; set; }

        public EnrolmentStatusReference EnrolmentStatusReference { get; set; }
        public EnrolleeNotification EnrolleeNotification { get; set; }
    }
}

using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class SiteRegistrationNoteViewModel
    {
        public int Id { get; set; }

        public int SiteId { get; set; }

        public int AdjudicatorId { get; set; }

        public Admin Adjudicator { get; set; }

        public string Note { get; set; }

        public DateTimeOffset NoteDate { get; set; }

        public SiteNotification SiteNotification { get; set; }
    }
}

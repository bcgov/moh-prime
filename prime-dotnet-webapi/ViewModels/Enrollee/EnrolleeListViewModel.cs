using System;

using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolleeListViewModel
    {
        public int Id { get; set; }

        public int DisplayId
        {
            get => Id + Enrollee.DISPLAY_OFFSET;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GivenNames { get; set; }

        public DateTimeOffset? AppliedDate { get; set; }

        public EnrolmentStatus CurrentStatus { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }

        public string CurrentTOAStatus { get; set; }

        public Admin Adjudicator { get; set; }

        public bool AlwaysManual { get; set; }
    }
}

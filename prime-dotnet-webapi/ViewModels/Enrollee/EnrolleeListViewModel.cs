using System;

using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolleeListViewModel
    {
        public int Id { get; set; }

        public int DisplayId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GivenNames { get; set; }

        public int CurrentStatusCode { get; set; }

        public DateTimeOffset? AppliedDate { get; set; }

        public DateTimeOffset? ApprovedDate { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }

        // public string CurrentTOAStatus { get; set; }

        public string AdjudicatorIdir { get; set; }

        public bool AlwaysManual { get; set; }
    }
}

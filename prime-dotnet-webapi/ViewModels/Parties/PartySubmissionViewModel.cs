using System;
using Prime.Models;

namespace Prime.ViewModels.Parties
{
    public class PartySubmissionViewModel
    {
        public int PartyId { get; set; }

        public SubmissionType SubmissionType { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public bool Approved { get; set; }
    }
}

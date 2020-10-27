using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class AgreementViewModel
    {
        public int Id { get; set; }

        public int? EnrolleeId { get; set; }

        public int? OrganizationId { get; set; }

        public int? PartyId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? AcceptedDate { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }

        public Guid? SignedAgreementDocumentGuid { get; set; }

        public AgreementType AgreementType { get; set; }

        public string AgreementContent { get; set; }
    }
}

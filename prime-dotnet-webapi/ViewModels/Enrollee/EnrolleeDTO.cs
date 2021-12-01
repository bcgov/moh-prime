using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolleeDTO : IUserBoundModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string GPID { get; set; }

        public string HPDID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GivenNames { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public MailingAddress MailingAddress { get; set; }

        public VerifiedAddress VerifiedAddress { get; set; }

        public string Email { get; set; }

        public string SmsPhone { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public string DeviceProviderIdentifier { get; set; }

        public bool ProfileCompleted { get; set; }

        public bool AlwaysManual { get; set; }

        public EnrolmentStatusViewModel CurrentStatus { get; set; }

        public EnrolmentStatusViewModel PreviousStatus { get; set; }

        public DateTimeOffset? AppliedDate { get; set; }

        public DateTimeOffset? ApprovedDate { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }

        public int DisplayId { get; set; }

        public bool HasNewestAgreement { get; set; }

        public AgreementType? AssignedTOAType { get; set; }

        public bool Confirmed { get; set; }

        public int LinkedEnrolleeId { get; set; }

        public bool PossiblePaperEnrolmentMatch { get; set; }
    }
}

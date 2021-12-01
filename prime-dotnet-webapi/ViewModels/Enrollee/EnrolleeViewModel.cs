using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolleeViewModel : IUserBoundModel
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

        public AddressViewModel PhysicalAddress { get; set; }

        public AddressViewModel MailingAddress { get; set; }

        public AddressViewModel VerifiedAddress { get; set; }

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

        public bool RequiresConfirmation { get => !Confirmed && PreviousStatus?.IsType(StatusType.UnderReview) == true; }

        public string CurrentTOAStatus => (StatusType)CurrentStatus.StatusCode switch
        {
            StatusType.UnderReview => "",
            StatusType.Locked => "N/A",
            StatusType.Declined => "N/A",
            StatusType.RequiresToa => "Pending",
            StatusType.Editable => EditableToaStatusText(),
            _ => null
        };

        private string EditableToaStatusText()
        {
            if (ExpiryDate == null || ExpiryDate < DateTimeOffset.Now)
            {
                return "";
            }
            else
            {
                return HasNewestAgreement ? "Yes" : "No";
            }
        }
    }
}

//  ---- Removed Properties: ----

// public ICollection<Certification> Certifications { get; set; }
// public ICollection<OboSite> OboSites { get; set; }
// public ICollection<EnrolleeCareSetting> EnrolleeCareSettings { get; set; }
// public ICollection<EnrolleeHealthAuthority> EnrolleeHealthAuthorities { get; set; }
// public AccessAgreementNote AccessAgreementNote { get; set; }
// public ICollection<SelfDeclaration> SelfDeclarations { get; set; }
// public ICollection<SelfDeclarationDocument> SelfDeclarationDocuments { get; set; }
// public ICollection<EnrolleeRemoteUser> EnrolleeRemoteUsers { get; set; }
// public ICollection<RemoteAccessLocation> RemoteAccessLocations { get; set; }
// public ICollection<RemoteAccessSite> RemoteAccessSites { get; set; }


//  ---- Removed with notes ----

// TODO: is this being used on the FE? Ideally remove entireley
// public ICollection<Job> Jobs { get; set; }

// TODO: Remove this entireley? BC eID is not used.
// public ICollection<IdentificationDocument> IdentificationDocuments { get; set; }

// TODO: already in extended controller
// public ICollection<EnrolmentStatus> EnrolmentStatuses { get; set; }

// TODO: there is already an endpoint for this
// public ICollection<EnrolleeNote> AdjudicatorNotes { get; set; }

// TODO: dont think we need this at all
// public int? CredentialId { get; set; }

// TODO: already an endpoint
// public string Base64QRCode { get; set; }

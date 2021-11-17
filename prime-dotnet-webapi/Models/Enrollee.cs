using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using DelegateDecompiler;

namespace Prime.Models
{
    [Table("Enrollee")]
    public class Enrollee : BaseAuditable, IValidatableObject, IUserBoundModel, IAgreeable
    {
        public Enrollee()
        {
            // Initialize collections to prevent null exception on computed properties
            // like CurrentStatus and ExpiryDate
            EnrolmentStatuses = new List<EnrolmentStatus>();
            Agreements = new List<Agreement>();
            Submissions = new List<Submission>();
            Addresses = new List<EnrolleeAddress>();
        }

        public const int DISPLAY_OFFSET = 1000;

        [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }

        [StringLength(20)]
        public string GPID { get; set; }

        [StringLength(255)]
        public string HPDID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string GivenNames { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [JsonIgnore]
        public ICollection<EnrolleeAddress> Addresses { get; set; }

        public string Email { get; set; }

        public string SmsPhone { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public ICollection<Certification> Certifications { get; set; }

        public ICollection<EnrolleeCareSetting> EnrolleeCareSettings { get; set; }

        public string DeviceProviderIdentifier { get; set; }

        public ICollection<SelfDeclaration> SelfDeclarations { get; set; }

        public ICollection<SelfDeclarationDocument> SelfDeclarationDocuments { get; set; }

        public ICollection<IdentificationDocument> IdentificationDocuments { get; set; }

        [JsonIgnore]
        public ICollection<EnrolleeAdjudicationDocument> EnrolleeAdjudicationDocuments { get; set; }

        [JsonIgnore]
        public ICollection<AssignedPrivilege> AssignedPrivileges { get; set; }

        public ICollection<EnrolmentStatus> EnrolmentStatuses { get; set; }

        public int? AdjudicatorId { get; set; }

        public Admin Adjudicator { get; set; }

        public bool ProfileCompleted { get; set; }

        public ICollection<EnrolleeNote> AdjudicatorNotes { get; set; }

        public AccessAgreementNote AccessAgreementNote { get; set; }

        [JsonIgnore]
        public ICollection<Agreement> Agreements { get; set; }

        [JsonIgnore]
        public ICollection<Submission> Submissions { get; set; }

        public bool AlwaysManual { get; set; }

        [JsonIgnore]
        public int IdentityAssuranceLevel { get; set; }

        [JsonIgnore]
        public string IdentityProvider { get; set; }

        public ICollection<EnrolleeRemoteUser> EnrolleeRemoteUsers { get; set; }

        public ICollection<RemoteAccessSite> RemoteAccessSites { get; set; }

        public ICollection<RemoteAccessLocation> RemoteAccessLocations { get; set; }

        public ICollection<OboSite> OboSites { get; set; }

        public ICollection<EnrolleeHealthAuthority> EnrolleeHealthAuthorities { get; set; }

        [JsonIgnore]
        public ICollection<EnrolleeAbsence> EnrolleeAbsences { get; set; }

        [NotMapped]
        [Computed]
        public PhysicalAddress PhysicalAddress
        {
            get => Addresses
                .Select(a => a.Address)
                .OfType<PhysicalAddress>()
                .SingleOrDefault();
        }

        [NotMapped]
        [Computed]
        public MailingAddress MailingAddress
        {
            get => Addresses
                .Select(a => a.Address)
                .OfType<MailingAddress>()
                .SingleOrDefault();
        }

        [NotMapped]
        [Computed]
        public VerifiedAddress VerifiedAddress
        {
            get => Addresses
                .Select(a => a.Address)
                .OfType<VerifiedAddress>()
                .SingleOrDefault();
        }

        /// <summary>
        /// Gets the most recent Enrolment Status on the Enrollee.
        /// </summary>
        [NotMapped]
        [Computed]
        public EnrolmentStatus CurrentStatus
        {
            get => EnrolmentStatuses
                .OrderByDescending(s => s.StatusDate)
                .ThenByDescending(s => s.Id)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the *second* most recent Enrolment Status on the Enrollee.
        /// </summary>
        [NotMapped]
        [Computed]
        public EnrolmentStatus PreviousStatus
        {
            get => EnrolmentStatuses
                .OrderByDescending(s => s.StatusDate)
                .ThenByDescending(s => s.Id)
                .Skip(1)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the most recent TOA that was assigned during submission of the enrolment.
        /// </summary>
        [NotMapped]
        [Computed]
        public AgreementType? AssignedTOAType
        {
            get => Submissions
                .OrderByDescending(s => s.CreatedDate)
                .Select(s => s.AgreementType)
                .FirstOrDefault();
        }

        /// <summary>
        /// The date of the Enrollee's most recent application.
        /// </summary>
        [NotMapped]
        [Computed]
        public DateTimeOffset? AppliedDate
        {
            get => EnrolmentStatuses
                .OrderByDescending(es => es.StatusDate)
                .ThenByDescending(es => es.Id)
                .Where(es => es.StatusCode == (int)StatusType.UnderReview)
                .Select(es => (DateTimeOffset?)es.StatusDate)
                .FirstOrDefault();
        }

        /// <summary>
        /// The date of the Enrollee's most recent manual or automatic approval.
        /// </summary>
        [NotMapped]
        [Computed]
        public DateTimeOffset? ApprovedDate
        {
            get => EnrolmentStatuses
                .OrderByDescending(es => es.StatusDate)
                .ThenByDescending(es => es.Id)
                .Where(es => es.StatusCode == (int)StatusType.RequiresToa)
                .Select(es => (DateTimeOffset?)es.StatusDate)
                .FirstOrDefault();
        }

        /// <summary>
        /// The Id of the Version of the Enrollee's most recently accepted Agreement.
        /// </summary>
        [NotMapped]
        [Computed]
        public int? CurrentAgreementId
        {
            get => Agreements
                .OrderByDescending(a => a.CreatedDate)
                .Where(a => a.AcceptedDate != null)
                .Select(a => (int?)a.AgreementVersionId)
                .FirstOrDefault();
        }

        /// <summary>
        /// The expiry date of the Enrollee's most recently accepted Agreement.
        /// </summary>
        [NotMapped]
        [Computed]
        public DateTimeOffset? ExpiryDate
        {
            get => Agreements
                .OrderByDescending(at => at.CreatedDate)
                .Where(at => at.AcceptedDate.HasValue)
                .Select(at => (DateTimeOffset?)at.ExpiryDate)
                .FirstOrDefault();
        }

        [NotMapped]
        [Computed]
        public int DisplayId
        {
            get => Id + DISPLAY_OFFSET;
        }

        [NotMapped]
        [Computed]
        [JsonIgnore]
        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }

        public EnrolmentStatus AddEnrolmentStatus(StatusType statusType)
        {
            var newStatus = EnrolmentStatus.FromType(statusType, Id);

            if (EnrolmentStatuses == null)
            {
                EnrolmentStatuses = new List<EnrolmentStatus>();
            }
            EnrolmentStatuses.Add(newStatus);

            return newStatus;
        }

        public void AddReasonToCurrentStatus(StatusReasonType type, string statusReasonNote = null)
        {
            if (CurrentStatus == null)
            {
                throw new InvalidOperationException($"Could not add Status Reason, Current Status is null.");
            }

            CurrentStatus.AddStatusReason(type, statusReasonNote);
        }

        public bool HasCareSetting(CareSettingType type)
        {
            if (EnrolleeCareSettings == null)
            {
                throw new InvalidOperationException($"{nameof(EnrolleeCareSettings)} cannot be null");
            }

            return EnrolleeCareSettings.Any(o => o.IsType(type));
        }

        /// <summary>
        /// Returns true if the Enrollee's most recently accepted Agreement has no newer versions.
        /// Makes no determination if said Agreement is of the correct type for the Enrollee.
        /// </summary>
        public bool HasLatestAgreement()
        {
            if (Agreements == null)
            {
                throw new InvalidOperationException($"Cannot determine latest agreement, {nameof(Agreements)} is null");
            }

            var currentAgreement = Agreements
                .OrderByDescending(a => a.CreatedDate)
                .FirstOrDefault(a => a.AcceptedDate != null);

            if (currentAgreement == null)
            {
                return false;
            }

            return AgreementVersion.NewestAgreementVersionIds().Contains(currentAgreement.AgreementVersionId);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Guid.Empty.Equals(UserId))
            {
                yield return new ValidationResult($"UserId cannot be empty");
            }
        }
    }
}

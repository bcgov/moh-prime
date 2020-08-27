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
    public class Enrollee : BaseAuditable, IValidatableObject, IUserBoundModel
    {
        public Enrollee()
        {
            // Initialize collections to prevent null exception on computed properties like CurrrentStatus and ExpiryDate
            EnrolmentStatuses = new List<EnrolmentStatus>();
            AccessTerms = new List<AccessTerm>();
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

        [Required]
        public PhysicalAddress PhysicalAddress { get; set; }

        public MailingAddress MailingAddress { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string VoicePhone { get; set; }

        public string VoiceExtension { get; set; }

        public ICollection<Certification> Certifications { get; set; }

        public ICollection<Job> Jobs { get; set; }

        public ICollection<EnrolleeCareSetting> EnrolleeCareSettings { get; set; }

        public string DeviceProviderNumber { get; set; }

        public bool? IsInsulinPumpProvider { get; set; }

        public ICollection<SelfDeclaration> SelfDeclarations { get; set; }

        public ICollection<SelfDeclarationDocument> SelfDeclarationDocuments { get; set; }

        [JsonIgnore]
        public ICollection<AssignedPrivilege> AssignedPrivileges { get; set; }

        public ICollection<EnrolmentStatus> EnrolmentStatuses { get; set; }

        [NotMapped]
        [JsonIgnore]
        public bool? isAdminView { get; set; }

        public int? AdjudicatorId { get; set; }

        public Admin Adjudicator { get; set; }

        public bool ProfileCompleted { get; set; }

        public ICollection<AdjudicatorNote> AdjudicatorNotes { get; set; }

        public AccessAgreementNote AccessAgreementNote { get; set; }

        [JsonIgnore]
        public ICollection<AccessTerm> AccessTerms { get; set; }

        [JsonIgnore]
        public ICollection<EnrolleeProfileVersion> EnrolleeProfileVersions { get; set; }

        public bool AlwaysManual { get; set; }

        public bool RequestingRemoteAccess { get; set; }

        [JsonIgnore]
        public int IdentityAssuranceLevel { get; set; }

        [JsonIgnore]
        public string IdentityProvider { get; set; }

        public int? CredentialId { get; set; }

        [JsonIgnore]
        public Credential Credential { get; set; }

        [NotMapped]
        public string Base64QRCode
        {
            get => Credential?.Base64QRCode;
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
        /// The date of the Enrollee's most recent applicaiton.
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
        /// The expiry date of the Enrollee's most recently accepted Access Term.
        /// </summary>
        [NotMapped]
        [Computed]
        public DateTimeOffset? ExpiryDate
        {
            get => AccessTerms
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

        /// <summary>
        /// Under Review -> ""
        /// Locked, Declined -> "NA"
        /// Required TOA -> "Pending"
        /// Editable (AND on/after their renewal date) -> ""
        /// Editable (AND before their renewal date) -> Is their signed TOA the most current version -> "Yes"/"No"
        /// </summary>
        [NotMapped]
        public string CurrentTOAStatus
        {
            get
            {
                if (CurrentStatus == null)
                {
                    // Bail if Statuses are not loaded
                    return null;
                }

                switch (CurrentStatus.GetStatusType())
                {
                    case StatusType.UnderReview:
                        return "";
                    case StatusType.Locked:
                    case StatusType.Declined:
                        return "N/A";
                    case StatusType.RequiresToa:
                        return "Pending";
                    case StatusType.Editable:
                        if (ExpiryDate == null || ExpiryDate <= DateTimeOffset.Now)
                        {
                            return "";
                        }
                        else
                        {
                            return HasLatestAgreement() ? "Yes" : "No";
                        }

                    default:
                        return null;
                }
            }
        }

        public EnrolmentStatus AddEnrolmentStatus(StatusType statusType)
        {
            var newStatus = EnrolmentStatus.FromType(statusType, this.Id);

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
            if (AccessTerms == null)
            {
                throw new InvalidOperationException($"Cannot determine latest agreement, {nameof(AccessTerms)} is null");
            }

            var currentAgreement = AccessTerms
                .OrderByDescending(a => a.CreatedDate)
                .FirstOrDefault(a => a.AcceptedDate != null);

            if (currentAgreement == null)
            {
                return false;
            }

            return Agreement.NewestAgreementIds().Contains(currentAgreement.AgreementId);
        }

        /// <summary>
        /// Returns true if the Enrollee has at least one Certification with a regulated Licence
        /// </summary>
        public bool IsRegulatedUser()
        {
            if (Certifications == null)
            {
                throw new InvalidOperationException($"{nameof(Certifications)} cannnot be null");
            }

            return Certifications.Any(cert => cert.License?.RegulatedUser == true);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Guid.Empty.Equals(this.UserId))
            {
                yield return new ValidationResult($"UserId cannot be the empty value: {this.UserId.ToString()}");
            }
        }
    }
}

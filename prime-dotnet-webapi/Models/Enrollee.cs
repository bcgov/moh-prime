using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    public enum ProgressStatusType
    {
        STARTED,
        SUBMITTED,
        FINISHED,
        EDITING
    }

    [Table("Enrollee")]
    public class Enrollee : BaseAuditable, IValidatableObject
    {
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

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

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

        public ICollection<Organization> Organizations { get; set; }

        public string DeviceProviderNumber { get; set; }

        public bool? IsInsulinPumpProvider { get; set; }

        public bool? HasConviction { get; set; }

        public string HasConvictionDetails { get; set; }

        public bool? HasRegistrationSuspended { get; set; }

        public string HasRegistrationSuspendedDetails { get; set; }

        public bool? HasDisciplinaryAction { get; set; }

        public string HasDisciplinaryActionDetails { get; set; }

        public bool? HasPharmaNetSuspended { get; set; }

        public string HasPharmaNetSuspendedDetails { get; set; }

        [JsonIgnore]
        public ICollection<AssignedPrivilege> AssignedPrivileges { get; set; }

        [NotMapped]
        public ICollection<Privilege> Privileges { get; set; }

        public ICollection<EnrolmentStatus> EnrolmentStatuses { get; set; }

        public int? AdjudicatorId { get; set; }

        public Admin Adjudicator { get; set; }

        [NotMapped]
        public EnrolmentStatus CurrentStatus
        {
            get => this.EnrolmentStatuses?
                .OrderByDescending(es => es.StatusDate)
                .ThenByDescending(es => es.Id)
                .FirstOrDefault();
        }

        [NotMapped]
        public EnrolmentStatus PreviousStatus
        {
            get => this.EnrolmentStatuses?
                .OrderByDescending(es => es.StatusDate)
                .ThenByDescending(es => es.Id)
                .Skip(1)
                .FirstOrDefault();
        }

        public bool ProfileCompleted { get; set; }

        [NotMapped]
        public DateTimeOffset? AppliedDate
        {
            get => this.EnrolmentStatuses?
                .OrderByDescending(en => en.StatusDate)
                .FirstOrDefault(es => es.IsType(StatusType.UnderReview))
                ?.StatusDate;
        }

        [NotMapped]
        public DateTimeOffset? ApprovedDate
        {
            get
            {
                return this.EnrolmentStatuses?
                    .OrderByDescending(en => en.StatusDate)
                    .Where(es => es.IsType(StatusType.RequiresToa))
                    .Where(es => es.StatusDate > this.AppliedDate)
                    .FirstOrDefault()
                    ?.StatusDate;
            }
        }

        [NotMapped]
        public DateTimeOffset? ExpiryDate
        {
            // This applies to the expiry date of the most recent accepted ToA
            get => this.AccessTerms?
                .OrderByDescending(at => at.AcceptedDate)
                .FirstOrDefault(at => at.ExpiryDate != null)?
                .ExpiryDate;
        }

        [NotMapped]
        public int DisplayId
        {
            get => Id + DISPLAY_OFFSET;
        }

        public ICollection<AdjudicatorNote> AdjudicatorNotes { get; set; }

        public AccessAgreementNote AccessAgreementNote { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Guid.Empty.Equals(this.UserId))
            {
                yield return new ValidationResult($"UserId cannot be the empty value: {this.UserId.ToString()}");
            }
        }

        [JsonIgnore]
        public ICollection<AccessTerm> AccessTerms { get; set; }

        [JsonIgnore]
        public ICollection<EnrolleeProfileVersion> EnrolleeProfileVersions { get; set; }

        public bool AlwaysManual { get; set; }

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
    }
}

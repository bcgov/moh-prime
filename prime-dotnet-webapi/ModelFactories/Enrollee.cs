using FactoryGirlCore;
using system;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Infrastructure;

namespace Prime.ModelFactories
{
    public enum ProgressStatusType
    {
        STARTED,
        SUBMITTED,
        FINISHED
    }

    [Table("Enrollee")]
    public class Enrollee : IDefinable, IValidatableObject
    {
        [Key]
        public int? Id ,

        [Required]
        public Guid UserId ,

        [StringLength(20)]
        public string LicensePlate ,

        [Required]
        public string FirstName ,

        public string MiddleName ,

        [Required]
        public string LastName ,

        public string PreferredFirstName ,

        public string PreferredMiddleName ,

        public string PreferredLastName ,

        [Required]
        public DateTime DateOfBirth ,

        public PhysicalAddress PhysicalAddress ,

        public MailingAddress MailingAddress ,

        public string ContactEmail ,

        public string ContactPhone ,

        public string VoicePhone ,

        public string VoiceExtension ,

        public ICollection<Certification> Certifications ,

        public ICollection<Job> Jobs ,

        public ICollection<Organization> Organizations ,

        [RegularExpression(@"([0-9]+)", ErrorMessage = "Device Provider Number should not contain characters")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Device Provider Number must be 5 digits")]
        [JsonConverter(typeof(EmptyStringToNullJsonConverter))]
        public string DeviceProviderNumber ,

        public bool? IsInsulinPumpProvider ,

        public bool? HasConviction ,

        public string HasConvictionDetails ,

        public bool? HasRegistrationSuspended ,

        public string HasRegistrationSuspendedDetails ,

        public bool? HasDisciplinaryAction ,

        public string HasDisciplinaryActionDetails ,

        public bool? HasPharmaNetSuspended ,

        public string HasPharmaNetSuspendedDetails ,

        [JsonIgnore]
        public ICollection<AssignedPrivilege> AssignedPrivileges ,

        [NotMapped]
        public ICollection<Privilege> Privileges ,

        public ICollection<EnrolmentStatus> EnrolmentStatuses ,

        [NotMapped]
        public EnrolmentStatus CurrentStatus
        {
            get => this.EnrolmentStatuses?
                .OrderByDescending(es => es.StatusDate)
                .ThenByDescending(es => es.Id)
                .FirstOrDefault();
        }

        [NotMapped]
        public EnrolmentStatus PharmaNetStatus { get => this.EnrolmentStatuses?.SingleOrDefault(es => es.PharmaNetStatus); }

        [NotMapped]
        public ProgressStatusType ProgressStatus
        {
            get
            {
                // Indicates the position of the enrollee within their initial enrolment, which
                // provides a status hook with greater granularity than the enrolment statuses
                var statuses = this.EnrolmentStatuses?.Select(es => es.StatusCode);
                return (statuses != null && statuses.Contains(Status.ACCEPTED_TOS_CODE))
                    ? ProgressStatusType.FINISHED
                    : (statuses != null && statuses.Contains(Status.SUBMITTED_CODE))
                        ? ProgressStatusType.SUBMITTED
                        : ProgressStatusType.STARTED;
            }
        }

        public bool ProfileCompleted ,

        [NotMapped]
        public ICollection<Status> AvailableStatuses ,


        [NotMapped]
        public DateTime? AppliedDate
        {
            get => this.EnrolmentStatuses?
                .OrderByDescending(en => en.StatusDate)
                .FirstOrDefault(es => es.StatusCode == Status.SUBMITTED_CODE)?
                .StatusDate;
        }

        [NotMapped]
        public DateTime? ApprovedDate
        {
            get => this.EnrolmentStatuses?
                .OrderByDescending(en => en.StatusDate)
                .FirstOrDefault(es => es.StatusCode == Status.APPROVED_CODE)?
                .StatusDate;
        }

        public ICollection<AdjudicatorNote> AdjudicatorNotes ,

        public AccessAgreementNote AccessAgreementNote ,

        public EnrolmentCertificateNote EnrolmentCertificateNote ,

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Guid.Empty.Equals(this.UserId))
            {
                yield return new ValidationResult($"UserId cannot be the empty value: {this.UserId.ToString()}");
            }
        }

        [NotMapped]
        public string EnrolleeClassification
        {
            get
            {
                ICollection<EnrolmentStatusReason> enrolmentStatusReasons = this.CurrentStatus?.EnrolmentStatusReasons;

                if (enrolmentStatusReasons != null && enrolmentStatusReasons.Count > 0)
                {
                    return (this.Certifications.Count > 0)
                        ? PrimeConstants.PRIME_MOA
                        : PrimeConstants.PRIME_RU;
                }

                return null;
            }
        }

        [JsonIgnore]
        public ICollection<TermsOfAccess> TermsOfAccess ,
    }
}

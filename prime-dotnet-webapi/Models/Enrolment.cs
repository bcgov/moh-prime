using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using Prime.Infrastructure;

namespace Prime.Models
{
    [Table("Enrolment")]
    public class Enrolment : BaseAuditable
    {
        [Key]
        public int? Id { get; set; }

        [JsonIgnore]
        public int EnrolleeId { get; set; }

        public Enrollee Enrollee { get; set; }

        [NotMapped]
        public DateTime? AppliedDate { get { return this.EnrolmentStatuses?.SingleOrDefault(es => es.StatusCode == Status.SUBMITTED_CODE)?.StatusDate; } }

        [NotMapped]
        public DateTime? ApprovedDate { get { return this.EnrolmentStatuses?.SingleOrDefault(es => es.StatusCode == Status.APPROVED_CODE)?.StatusDate; } }

        // public bool? HasCertification { get; set; }

        public ICollection<Certification> Certifications { get; set; }

        // public bool? IsDeviceProvider { get; set; }

        [RegularExpression(@"([0-9]+)", ErrorMessage = "Device Provider Number should not contain characters")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Device Provider Number must be 5 digits")]
        [JsonConverter(typeof(EmptyStringToNullJsonConverter))]
        public string DeviceProviderNumber { get; set; }

        public bool? IsInsulinPumpProvider { get; set; }

        // public bool? IsAccessingPharmaNetOnBehalfOf { get; set; }

        public ICollection<Job> Jobs { get; set; }

        public bool? HasConviction { get; set; }

        public string HasConvictionDetails { get; set; }

        public bool? HasRegistrationSuspended { get; set; }

        public string HasRegistrationSuspendedDetails { get; set; }

        public bool? HasDisciplinaryAction { get; set; }

        public string HasDisciplinaryActionDetails { get; set; }

        public bool? HasPharmaNetSuspended { get; set; }

        public string HasPharmaNetSuspendedDetails { get; set; }

        public ICollection<Organization> Organizations { get; set; }

        public ICollection<EnrolmentStatus> EnrolmentStatuses { get; set; }

        [NotMapped]
        public EnrolmentStatus CurrentStatus { get { return this.EnrolmentStatuses?.SingleOrDefault(es => es.IsCurrent); } }

        [NotMapped]
        public ICollection<Status> AvailableStatuses { get; set; }
    }
}

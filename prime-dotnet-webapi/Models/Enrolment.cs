using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Prime.Models
{
    [Table("Enrolment")]
    public class Enrolment
    {
        [Key]
        public int? Id { get; set; }

        [JsonIgnore]
        public int EnrolleeId { get; set; }

        public Enrollee Enrollee { get; set; }

        public DateTime AppliedDate { get; set; }

        public bool? Approved { get; set; }

        public string ApprovedReason { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public bool? HasCertification { get; set; }

        public ICollection<Certification> Certifications { get; set; }

        public bool? IsDeviceProvider { get; set; }

        public string DeviceProviderNumber { get; set; }

        public bool? IsInsulinPumpProvider { get; set; }

        public bool? IsAccessingPharmaNetOnBehalfOf { get; set; }

        public ICollection<Job> Jobs { get; set; }

        public bool? HasConviction { get; set; }

        public bool? HasRegistrationSuspended { get; set; }

        public bool? HasDisciplinaryAction { get; set; }

        public bool? HasPharmaNetSuspended { get; set; }

        public ICollection<Organization> Organizations { get; set; }
    }
}
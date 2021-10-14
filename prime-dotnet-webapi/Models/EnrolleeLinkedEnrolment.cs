using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolleeLinkedEnrolment")]
    public class EnrolleeLinkedEnrolment : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int? PaperEnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee PaperEnrollee { get; set; }

        public string UserProvidedGpid { get; set; }

        public DateTime? EnrolmentLinkDate { get; set; }
    }
}

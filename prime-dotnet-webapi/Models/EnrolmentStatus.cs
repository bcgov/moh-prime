using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolmentStatuses")]
    public class EnrolmentStatus : BaseAuditable
    {
        public int EnrolmentId { get; set; }

        [JsonIgnore]
        public Enrolment Enrolment { get; set; }

        public short StatusCode { get; set; }

        public Status Status { get; set; }

        [Required]
        public DateTime StatusDate { get; set; }

        [Required]
        public bool IsCurrent { get; set; }
    }
}
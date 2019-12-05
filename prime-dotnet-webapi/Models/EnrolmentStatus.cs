using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolmentStatuses")]
    public class EnrolmentStatus : BaseAuditable
    {
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public short StatusCode { get; set; }

        public Status Status { get; set; }

        [Required]
        public DateTime StatusDate { get; set; }

        [Required]
        public bool IsCurrent { get; set; }

        public ICollection<EnrolmentStatusReason> EnrolmentStatusReasons { get; set; }
    }
}

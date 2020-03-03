using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolmentStatus")]
    public class EnrolmentStatus : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public short StatusCode { get; set; }

        public Status Status { get; set; }

        public DateTime StatusDate { get; set; }

        public bool PharmaNetStatus { get; set; }

        public ICollection<EnrolmentStatusReason> EnrolmentStatusReasons { get; set; }

        public void AddStatusReason(short reasonCode, string reasonNote = null)
        {
            if (EnrolmentStatusReasons == null)
            {
                EnrolmentStatusReasons = new List<EnrolmentStatusReason>(1);
            }

            EnrolmentStatusReasons.Add(new EnrolmentStatusReason
            {
                EnrolmentStatus = this,
                StatusReasonCode = reasonCode,
                ReasonNote = reasonNote
            });
        }
    }
}

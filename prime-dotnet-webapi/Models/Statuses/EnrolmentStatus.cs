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

        public int StatusCode { get; set; }

        public Status Status { get; set; }

        public DateTimeOffset StatusDate { get; set; }

        public ICollection<EnrolmentStatusReason> EnrolmentStatusReasons { get; set; }

        public void AddStatusReason(int reasonCode, string reasonNote = null)
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

        public bool IsType(StatusType statusType)
        {
            return this.StatusCode == (int)statusType;
        }

        public static EnrolmentStatus FromStatusType(StatusType statusType, int enrolleeId)
        {
            return new EnrolmentStatus
            {
                EnrolleeId = enrolleeId,
                StatusCode = (int)statusType,
                StatusDate = DateTimeOffset.Now
            };
        }
    }
}

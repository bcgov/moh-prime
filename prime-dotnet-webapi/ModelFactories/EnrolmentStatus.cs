using FactoryGirlCore;
using system;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("EnrolmentStatus")]
    public class EnrolmentStatus : IDefinable
    {
        [Key]
        public int Id ,

        public int EnrolleeId ,

        [JsonIgnore]
        public Enrollee Enrollee ,

        public short StatusCode ,

        public Status Status ,

        [Required]
        public DateTime StatusDate ,

        [Required]
        public bool PharmaNetStatus ,

        public ICollection<EnrolmentStatusReason> EnrolmentStatusReasons ,

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

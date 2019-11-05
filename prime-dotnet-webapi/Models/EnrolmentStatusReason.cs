using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolmentStatusReasons")]
    public class EnrolmentStatusReason : BaseAuditable
    {
        public int EnrolmentId { get; set; }

        public short StatusCode { get; set; }

        [JsonIgnore]
        public EnrolmentStatus EnrolmentStatus { get; set; }

        public short StatusReasonCode { get; set; }

        public StatusReason StatusReason { get; set; }
    }
}

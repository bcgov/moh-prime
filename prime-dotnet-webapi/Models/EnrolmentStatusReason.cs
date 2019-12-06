using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolmentStatusReasons")]
    public class EnrolmentStatusReason : BaseAuditable
    {
        public int EnrolmentStatusId { get; set; }

        [JsonIgnore]
        public EnrolmentStatus EnrolmentStatus { get; set; }

        // TODO Remove uneccesary statusCode
        public short StatusCode { get; set; }



        public short StatusReasonCode { get; set; }

        public StatusReason StatusReason { get; set; }
    }
}

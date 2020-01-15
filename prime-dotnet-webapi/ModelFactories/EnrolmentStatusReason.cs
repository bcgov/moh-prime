using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("EnrolmentStatusReason")]
    public class EnrolmentStatusReason : IDefinable
    {
        [Key]
        public int Id ,

        public int EnrolmentStatusId ,

        [JsonIgnore]
        public EnrolmentStatus EnrolmentStatus ,

        public short StatusReasonCode ,

        public StatusReason StatusReason ,

        public string ReasonNote ,
    }
}

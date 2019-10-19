using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("StatusLookup")]
    public class Status : BaseAuditable, ILookup
    {
        public const short IN_PROGRESS_CODE = 1;
        public const short SUBMITTED_CODE = 2;
        public const short APPROVED_CODE = 3;
        public const short DECLINED_CODE = 4;
        public const short ACCEPTED_TOS_CODE = 5;
        public const short DECLINED_TOS_CODE = 6;

        [Key]
        public short Code { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<EnrolmentStatus> EnrolmentStatuses { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals(obj as Status);
        }

        public bool Equals(Status other)
        {
            return other != null && Code == other.Code;
        }

        public override int GetHashCode()
        {
            var hashCode = 352033288;
            hashCode = hashCode * -1521134295 + Code.GetHashCode();
            return hashCode;
        }
    }
}
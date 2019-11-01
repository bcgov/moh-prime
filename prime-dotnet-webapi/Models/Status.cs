using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("StatusLookup")]
    public sealed class Status : BaseAuditable, ILookup<short>, IEquatable<Status>
    {
        public readonly static short IN_PROGRESS_CODE = 1;
        public readonly static short SUBMITTED_CODE = 2;
        public readonly static short APPROVED_CODE = 3;
        public readonly static short DECLINED_CODE = 4;
        public readonly static short ACCEPTED_TOS_CODE = 5;
        public readonly static short DECLINED_TOS_CODE = 6;

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
            var hashCode = 352_033_288;
            hashCode = hashCode * -1_521_134_295 + Code.GetHashCode();
            return hashCode;
        }
    }
}

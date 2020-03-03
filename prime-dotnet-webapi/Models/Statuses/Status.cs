using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("StatusLookup")]
    public sealed class Status : BaseAuditable, ILookup<int>, IEquatable<Status>
    {
        public const int ACTIVE_CODE = 1;
        public const int UNDER_REVIEW_CODE = 2;
        public const int REQUIRES_TOA_CODE = 3;
        public const int LOCKED_CODE = 4;

        [Key]
        public int Code { get; set; }

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

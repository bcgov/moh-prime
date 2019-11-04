using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("StatusReasonLookup")]
    public sealed class StatusReason : BaseAuditable, ILookup<short>, IEquatable<StatusReason>
    {
        public readonly static short AUTOMATIC_CODE = 1;
        public readonly static short MANUAL_CODE = 2;
        public readonly static short NAME_DISCREPANCY_CODE = 3;
        public readonly static short NOT_IN_PHARMANET_CODE = 4;
        public readonly static short PUMP_PROVIDER_CODE = 5;
        public readonly static short LICENCE_CLASS_CODE = 6;
        public readonly static short SELF_DECLARATION_CODE = 7;
        public readonly static short ADDRESS_CODE = 8;

        [Key]
        public short Code { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<EnrolmentStatusReason> EnrolmentStatusReasons { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals(obj as StatusReason);
        }

        public bool Equals(StatusReason other)
        {
            return other != null && Code == other.Code;
        }

        public override int GetHashCode()
        {
            var hashCode = 1_655_539_742;
            hashCode = hashCode * -1_521_134_295 + Code.GetHashCode();
            return hashCode;
        }
    }
}
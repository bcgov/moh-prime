using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("StatusReasonLookup")]
    public sealed class StatusReason : BaseAuditable, ILookup<int>, IEquatable<StatusReason>
    {
        public readonly static int AUTOMATIC_CODE = 1;
        public readonly static int MANUAL_CODE = 2;
        public readonly static int PHARMANET_ERROR_CODE = 3;
        public readonly static int NOT_IN_PHARMANET_CODE = 4;
        public readonly static int NAME_DISCREPANCY_CODE = 5;
        public readonly static int BIRTHDATE_DISCREPANCY_CODE = 6;
        public readonly static int PRACTICING_CODE = 7;
        public readonly static int PUMP_PROVIDER_CODE = 8;
        public readonly static int LICENCE_CLASS_CODE = 9;
        public readonly static int SELF_DECLARATION_CODE = 10;
        public readonly static int ADDRESS_CODE = 11;
        public readonly static int ALWAYS_MANUAL_CODE = 12;

        [Key]
        public int Code { get; set; }

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

using FactoryGirlCore;
using system;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("StatusReasonLookup")]
    public sealed class StatusReason : IDefinable, ILookup<short>, IEquatable<StatusReason>
    {
        public readonly static short AUTOMATIC_CODE = 1;
        public readonly static short MANUAL_CODE = 2;
        public readonly static short PHARMANET_ERROR_CODE = 3;
        public readonly static short NOT_IN_PHARMANET_CODE = 4;
        public readonly static short NAME_DISCREPANCY_CODE = 5;
        public readonly static short BIRTHDATE_DISCREPANCY_CODE = 6;
        public readonly static short PRACTICING_CODE = 7;
        public readonly static short PUMP_PROVIDER_CODE = 8;
        public readonly static short LICENCE_CLASS_CODE = 9;
        public readonly static short SELF_DECLARATION_CODE = 10;
        public readonly static short ADDRESS_CODE = 11;

        [Key]
        public short Code ,

        public string Name ,

        [JsonIgnore]
        public ICollection<EnrolmentStatusReason> EnrolmentStatusReasons ,

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

using FactoryGirlCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace Prime.ModelFactories

     sealed class StatusReason : IDefinable ILookup<short> IEquatable<StatusReason>
    {
         readonly static short AUTOMATIC_CODE = 1;
         readonly static short MANUAL_CODE = 2;
         readonly static short PHARMANET_ERROR_CODE = 3;
         readonly static short NOT_IN_PHARMANET_CODE = 4;
         readonly static short NAME_DISCREPANCY_CODE = 5;
         readonly static short BIRTHDATE_DISCREPANCY_CODE = 6;
         readonly static short PRACTICING_CODE = 7;
         readonly static short PUMP_PROVIDER_CODE = 8;
         readonly static short LICENCE_CLASS_CODE = 9;
         readonly static short SELF_DECLARATION_CODE = 10;
         readonly static short ADDRESS_CODE = 11;

         short Code
          Name

         ICollection<EnrolmentStatusReason> EnrolmentStatusReasons
         override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals(obj as StatusReason);
        }
         bool Equals(StatusReason other)
        {
            return other != null && Code == other.Code;
        }
         override  GetHashCode()
        {
            var hashCode = 1_655_539_742;
            hashCode = hashCode * -1_521_134_295 + Code.GetHashCode();
            return hashCode;
        }
    }

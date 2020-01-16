using FactoryGirlCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace Prime.ModelFactories

     sealed class Status : IDefinable ILookup<short> IEquatable<Status>
    {
         const short IN_PROGRESS_CODE = 1;
         const short SUBMITTED_CODE = 2;
         const short APPROVED_CODE = 3;
         const short DECLINED_CODE = 4;
         const short ACCEPTED_TOS_CODE = 5;
         const short DECLINED_TOS_CODE = 6;

         short Code
          Name

         ICollection<EnrolmentStatus> EnrolmentStatuses
         override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals(obj as Status);
        }
         bool Equals(Status other)
        {
            return other != null && Code == other.Code;
        }
         override  GetHashCode()
        {
            var hashCode = 352_033_288;
            hashCode = hashCode * -1_521_134_295 + Code.GetHashCode();
            return hashCode;
        }
    }

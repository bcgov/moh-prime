using FactoryGirlCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{

     class EnrolmentStatus : IDefinable
    {

          Id

          EnrolleeId


         Enrollee Enrollee

         short StatusCode

         Status Status


         DateTime StatusDate


         bool PharmaNetStatus

         ICollection<EnrolmentStatusReason> EnrolmentStatusReasons

         void AddStatusReason(short reasonCode  reasonNote = null)
        {
            if (EnrolmentStatusReasons == null)
            {
                EnrolmentStatusReasons = new List<EnrolmentStatusReason>(1);
            }

            EnrolmentStatusReasons.Add(new EnrolmentStatusReason
            {
                EnrolmentStatus = this
                StatusReasonCode = reasonCode
                ReasonNote = reasonNote
            });
        }
    }
}

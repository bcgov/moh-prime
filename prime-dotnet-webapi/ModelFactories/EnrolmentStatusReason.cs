using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace Prime.ModelFactories

     class EnrolmentStatusReason : IDefinable
    {
          Id
          EnrolmentStatusId

         EnrolmentStatus EnrolmentStatus
         short StatusReasonCode
         StatusReason StatusReason
          ReasonNote
    }

using FactoryGirlCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{

     sealed class EnrolmentCertificateAccessToken : IDefinable
    {


         Guid Id


         ? EnrolleeId


         Enrollee Enrollee

         DateTime Expires

          ViewCount

         Boolean Active
    }
}

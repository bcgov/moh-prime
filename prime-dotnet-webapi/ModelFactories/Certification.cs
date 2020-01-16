using FactoryGirlCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Infrastructure;

namespace Prime.ModelFactories
{


     class Certification : IDefinable IEnrolleeNavigationProperty
    {

         ? Id


          EnrolleeId


         Enrollee Enrollee


         short CollegeCode


         College College





          LicenseNumber


         short LicenseCode


         License License


         DateTime RenewalDate

         short? PracticeCode


         Practice Practice



          FullLicenseNumber { get { return $"{College?.Prefix}-{LicenseNumber}"; } }
    }
}

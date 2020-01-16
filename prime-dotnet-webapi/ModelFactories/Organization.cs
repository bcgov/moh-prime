using FactoryGirlCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{

     class Organization : IDefinable IEnrolleeNavigationProperty
    {

         ? Id


          EnrolleeId


         Enrollee Enrollee


         short OrganizationTypeCode


         OrganizationType OrganizationType
    }
}

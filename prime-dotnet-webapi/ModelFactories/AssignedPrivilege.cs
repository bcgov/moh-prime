using FactoryGirlCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace Prime.ModelFactories

     class AssignedPrivilege : IDefinable
    {
          EnrolleeId

         Enrollee Enrollee
          PrivilegeId
         Privilege Privilege
    }

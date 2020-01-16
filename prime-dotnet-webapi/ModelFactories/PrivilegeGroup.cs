using FactoryGirlCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
     class PrivilegeGroup : IDefinable
    {

          Id

          Name

         ICollection<Privilege> Privileges

    }
}

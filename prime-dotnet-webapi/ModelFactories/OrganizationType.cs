using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace Prime.ModelFactories

     class OrganizationType : IDefinable ILookup<short>
    {
         short Code

          Name

         ICollection<Organization> Organizations
    }

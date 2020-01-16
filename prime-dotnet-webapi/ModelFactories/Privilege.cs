using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.ModelFactories
{

     class Privilege : IDefinable
    {

          Id

          TransactionType

          Description

          PrivilegeGroupId


         PrivilegeGroup PrivilegeGroup


         ICollection<DefaultPrivilege> DefaultPrivileges


         ICollection<AssignedPrivilege> AssignedPrivileges

    }
}

using FactoryGirlCore;
using system;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("AssignedPrivilege")]
    public class AssignedPrivilege : IDefinable
    {
        public int EnrolleeId ,

        [JsonIgnore]
        public Enrollee Enrollee ,

        public int PrivilegeId ,

        public Privilege Privilege ,
    }
}

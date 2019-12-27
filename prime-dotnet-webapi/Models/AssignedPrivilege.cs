using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
<<<<<<< HEAD
    [Table("AssignedPrivilege")]
=======
>>>>>>> develop
    public class AssignedPrivilege : BaseAuditable
    {
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int PrivilegeId { get; set; }

<<<<<<< HEAD
        public Privilege Privilege { get; set; }


=======
        [JsonIgnore]
        public Privilege Privilege { get; set; }

>>>>>>> develop
    }
}

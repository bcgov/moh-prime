using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class PrivilegeGroup : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Privilege> Privileges { get; set; }

    }
}

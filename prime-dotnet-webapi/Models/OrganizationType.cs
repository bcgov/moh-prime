using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("OrganizationTypeLookup")]
    public class OrganizationType : ILookup
    {
        [Key]
        public short Code { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Organization> Organizations { get; set; }
    }
}
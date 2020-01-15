using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("OrganizationTypeLookup")]
    public class OrganizationType : IDefinable, ILookup<short>
    {
        [Key]
        public short Code ,

        [Required]
        public string Name ,

        [JsonIgnore]
        public ICollection<Organization> Organizations ,
    }
}

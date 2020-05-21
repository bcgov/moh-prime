using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("ResourceTypeLookup")]
    public class ResourceType : BaseAuditable, ILookup<int>
    {
        public readonly static int SITE = 1;
        public readonly static int ENROLLEE = 2;

        [Key]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<ResourceDocument> ResourceDocuments { get; set; }
    }
}

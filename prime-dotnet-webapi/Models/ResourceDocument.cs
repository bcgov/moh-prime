using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("ResourceDocument")]
    public class ResourceDocument : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public Guid DocumentGuid { get; set; }

        public int ResourceId { get; set; }

        public int ResourceTypeCode { get; set; }

        [JsonIgnore]
        public ResourceType ResourceType { get; set; }
    }
}

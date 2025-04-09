using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Prime.Models
{
    [Table("SiteSubmission")]
    public class SiteSubmission : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }

        [Required]
        public JObject ProfileSnapshot { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

    }
}

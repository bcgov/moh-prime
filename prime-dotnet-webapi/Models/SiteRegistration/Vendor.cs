using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Prime.Models
{
    [Table("Vendor")]
    public class Vendor : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public IEnumerable<Site> Sites { get; set; }

    }
}

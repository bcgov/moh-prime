using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Prime.Models
{
    [Table("EnrolleeProfileVersion")]
    public class EnrolleeProfileVersion : BaseAuditable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EnrolleeId { get; set; }
        [JsonIgnore]
        public Enrollee Enrollee { get; set; }
        [Required]
        public JObject ProfileSnapshot { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public int Version { get; set; }
    }
}

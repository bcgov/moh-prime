using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolleeProfileHistory")]
    public class EnrolleeProfileHistory : BaseAuditable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EnrolleeId { get; set; }
        [JsonIgnore]
        public Enrollee Enrollee { get; set; }
        [Required]
        public Enrollee ProfileSnapshot { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}

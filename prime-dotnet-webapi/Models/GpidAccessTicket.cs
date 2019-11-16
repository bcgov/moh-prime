using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("GpidAccessTicket")]
    public sealed class GpidAccessTicket : BaseAuditable
    {
        [Key]
        public Guid Id { get; set; }

        [JsonIgnore]
        [Required]
        public int? EnrolleeId { get; set; }

        [JsonIgnore]
        public int ViewCount { get; set; }

        public Boolean Active { get; set; }
    }
}

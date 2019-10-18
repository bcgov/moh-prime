using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class BaseAuditable : IAuditable
    {
        [JsonIgnore]
        [Required]
        public Guid CreatedUserId { get; set; }

        [JsonIgnore]
        [Required]
        public DateTime CreatedTimeStamp { get; set; }

        [JsonIgnore]
        [Required]
        public Guid UpdatedUserId { get; set; }

        [JsonIgnore]
        [Required]
        public DateTime UpdatedTimeStamp { get; set; }
    }
}
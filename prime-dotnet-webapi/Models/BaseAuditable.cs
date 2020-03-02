using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class BaseAuditable : IAuditable
    {
        protected BaseAuditable()
        { }

        [JsonIgnore]
        public Guid CreatedUserId { get; set; }

        [JsonIgnore]
        public DateTime CreatedTimeStamp { get; set; }

        [JsonIgnore]
        public Guid UpdatedUserId { get; set; }

        [JsonIgnore]
        public DateTime UpdatedTimeStamp { get; set; }
    }
}

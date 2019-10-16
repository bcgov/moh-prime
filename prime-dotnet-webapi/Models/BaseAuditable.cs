using System;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class BaseAuditable : IAuditable
    {
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
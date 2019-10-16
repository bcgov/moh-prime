using System;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class BaseAuditable : IAuditable
    {
        [JsonIgnore]
        public string CreatedUserId { get; set; }

        [JsonIgnore]
        public DateTime CreatedTimeStamp { get; set; }

        [JsonIgnore]
        public string UpdatedUserId { get; set; }

        [JsonIgnore]
        public DateTime UpdatedTimeStamp { get; set; }
    }
}
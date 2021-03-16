using System;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class BaseAuditable
    {
        protected BaseAuditable()
        { }

        [JsonIgnore]
        public Guid CreatedUserId { get; set; }

        [JsonIgnore]
        public DateTimeOffset CreatedTimeStamp { get; set; }

        [JsonIgnore]
        public Guid UpdatedUserId { get; set; }

        [JsonIgnore]
        public DateTimeOffset UpdatedTimeStamp { get; set; }
    }
}

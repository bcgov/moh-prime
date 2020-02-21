
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Prime.Models
{
    [Table("BusinessEventTypeLookup")]
    public sealed class BusinessEventType : BaseAuditable, ILookup<short>
    {
        public const short STATUS_CHANGE_CODE = 1;


        [Key]
        public short Code { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<BusinessEvent> BusinessEvents { get; set; }
    }
}

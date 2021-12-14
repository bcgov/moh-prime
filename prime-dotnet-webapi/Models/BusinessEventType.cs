
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("BusinessEventTypeLookup")]
    public sealed class BusinessEventType : ILookup<int>
    {
        public const int StatusChange = 1;
        public const int Email = 2;
        public const int Note = 3;
        public const int AdminAction = 4;
        public const int Enrollee = 5;
        public const int Site = 6;
        public const int AdminView = 7;
        public const int Organization = 8;
        public const int PharmanetApiCall = 9;
        public const int PaperEnrolmentLink = 10;

        [Key]
        public int Code { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<BusinessEvent> BusinessEvents { get; set; }
    }
}

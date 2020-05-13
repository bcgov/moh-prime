
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("BusinessEventTypeLookup")]
    public sealed class BusinessEventType : BaseAuditable, ILookup<int>
    {
        public const int STATUS_CHANGE_CODE = 1;
        public const int EMAIL_CODE = 2;
        public const int NOTE_CODE = 3;
        public const int ADMIN_CLAIM_CODE = 4;
        public const int ENROLLEE_CODE = 5;
        public const int SITE_CODE = 6;
        public const int ADMIN_VIEW_CODE = 7;

        [Key]
        public int Code { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<BusinessEvent> BusinessEvents { get; set; }
    }
}

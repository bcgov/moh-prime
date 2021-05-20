
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("BusinessEventTypeLookup")]
    public sealed class BusinessEventType : ILookup<int>
    {
        public const int STATUS_CHANGE_CODE = 1;
        public const int EMAIL_CODE = 2;
        public const int NOTE_CODE = 3;
        public const int ADMIN_ACTION_CODE = 4;
        public const int ENROLLEE_CODE = 5;
        public const int SITE_CODE = 6;
        public const int ADMIN_VIEW_CODE = 7;
        public const int ORGANIZATION_CODE = 8;
        public const int PHARMANET_API_CALL_CODE = 9;

        [Key]
        public int Code { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<BusinessEvent> BusinessEvents { get; set; }
    }
}

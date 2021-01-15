using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SiteRegistrationNote")]
    public class SiteRegistrationNote : BaseAdjudicatorNote
    {
        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }

        [JsonIgnore]
        public SiteEscalation SiteEscalation { get; set; }
    }
}

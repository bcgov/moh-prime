using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("SiteAdjudicationDocument")]
    public class SiteAdjudicationDocument : BaseDocumentUpload, IAdjudicationDocument
    {
        public int AdjudicatorId { get; set; }

        public Admin Adjudicator { get; set; }

        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }
    }
}

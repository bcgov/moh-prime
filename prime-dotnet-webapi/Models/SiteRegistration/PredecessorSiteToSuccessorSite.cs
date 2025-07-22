using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("PredecessorSiteToSuccessorSite")]
    public class PredecessorSiteToSuccessorSite : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int PredecessorSiteId { get; set; }

        public int SuccessorSiteId { get; set; }

        [JsonIgnore]
        public Site PredecessorSite { get; set; }

        [JsonIgnore]
        public Site SuccessorSite { get; set; }

    }
}

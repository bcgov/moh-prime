using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.HealthAuthorities
{
    [Table("PredecessorSiteToSuccessorSite")]
    public class PredecessorSiteToSuccessorSite : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int PredecessorSiteId { get; set; }

        [JsonIgnore]
        public Site PredecessorSite { get; set; }

        public int SuccessorSiteId { get; set; }

        [JsonIgnore]
        public Site SuccessorSite { get; set; }
    }
}

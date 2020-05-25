using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SiteOrganizationType")]
    public class SiteOrganizationType : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }

        public int OrganizationTypeCode { get; set; }

        [JsonIgnore]
        public OrganizationType OrganizationType { get; set; }
    }
}

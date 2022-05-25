using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SiteClaim")]
    public class SiteClaim : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }

        public int NewOrganizationId { get; set; }

        [JsonIgnore]
        public Organization NewOrganization { get; set; }

        public int NewSigningAuthorityId { get; set; }

        [JsonIgnore]
        public Party NewSigningAuthority { get; set; }

        public string ProvidedSiteId { get; set; }

        public string Details { get; set; }
    }
}

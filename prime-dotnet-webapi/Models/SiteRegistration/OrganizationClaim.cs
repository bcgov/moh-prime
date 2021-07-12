using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("OrganizationClaim")]
    public class OrganizationClaim : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int OrganizationId { get; set; }

        [JsonIgnore]
        public Organization Organization { get; set; }

        public int NewSigningAuthorityId { get; set; }

        [JsonIgnore]
        public Party NewSigningAuthority { get; set; }

        public string ProvidedSiteId { get; set; }

        public string Details { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("LicenseClassClauseMapping")]
    public class LicenseClassClauseMapping : BaseAuditable
    {
        public int LicenseCode { get; set; }

        [JsonIgnore]
        public License License { get; set; }

        public int OrganizatonTypeCode { get; set; }

        [JsonIgnore]
        public OrganizationType OrganizationType { get; set; }

        public int LicenseClassClauseId { get; set; }

        [JsonIgnore]
        public LicenseClassClause LicenseClassClause { get; set; }
    }
}

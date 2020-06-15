using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("AccessTermLicenseClassClause")]
    public class AccessTermLicenseClassClause : BaseAuditable
    {
        public int AccessTermId { get; set; }

        [JsonIgnore]
        public AccessTerm AccessTerm { get; set; }

        public int LicenseClassClauseId { get; set; }

        [JsonIgnore]
        public LicenseClassClause LicenseClassClause { get; set; }
    }
}

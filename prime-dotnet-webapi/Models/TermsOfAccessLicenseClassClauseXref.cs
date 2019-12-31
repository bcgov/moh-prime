using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("TermsOfAccessLicenseClassClausesXref")]
    public class TermsOfAccessLicenseClassClauseXref
    {
        [Required]
        public int TermsOfAccessId;

        [JsonIgnore]
        public TermsOfAccess TermsOfAccess;

        [Required]
        public int LicenseClassClauseId;

        [JsonIgnore]
        public LicenceClassClause LicenseClassClause;
    }
}

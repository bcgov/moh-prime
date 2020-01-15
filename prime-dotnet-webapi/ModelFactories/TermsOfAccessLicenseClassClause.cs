using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("TermsOfAccessLicenseClassClause")]
    public class TermsOfAccessLicenseClassClause
    {
        [Required]
        public int TermsOfAccessId ,

        [JsonIgnore]
        public TermsOfAccess TermsOfAccess ,

        [Required]
        public int LicenseClassClauseId ,

        [JsonIgnore]
        public LicenseClassClause LicenseClassClause ,
    }
}

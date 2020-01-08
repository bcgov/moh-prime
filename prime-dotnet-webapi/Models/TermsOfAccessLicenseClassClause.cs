using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("TermsOfAccessLicenseClassClauseXref")]
    public class TermsOfAccessLicenseClassClause
    {
        [Required]
        public int TermsOfAccessId { get; set; }

        public TermsOfAccess TermsOfAccess { get; set; }

        [Required]
        public int LicenseClassClauseId { get; set; }

        public LicenseClassClause LicenseClassClause { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prime.Models.AccessAgreement;

namespace Prime.Models
{
    [Table("LicenseClassClauses")]
    public class LicenseClassClause : BaseAuditable, IAccessClause
    {
        public LicenseClassClause()
        {
            // Create lists so they don't have be instantiated when items need to be added
            TermsOfAccessLicenseClassClauses = new List<TermsOfAccessLicenseClassClause>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Clause { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        public List<TermsOfAccessLicenseClassClause> TermsOfAccessLicenseClassClauses { get; set; }
    }
}

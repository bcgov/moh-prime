using FactoryGirlCore;
using system;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Models.AccessAgreement;

namespace Prime.ModelFactories
{
    [Table("LicenseClassClause")]
    public class LicenseClassClause : IDefinable, IAccessClause
    {
        public LicenseClassClause()
        {
            // Create lists so they don't have be instantiated when items need to be added
            TermsOfAccessLicenseClassClauses = new List<TermsOfAccessLicenseClassClause>();
        }

        [Key]
        public int Id ,

        [Required]
        public string Clause ,

        [Required]
        public DateTime EffectiveDate ,

        [JsonIgnore]
        public List<TermsOfAccessLicenseClassClause> TermsOfAccessLicenseClassClauses ,
    }
}

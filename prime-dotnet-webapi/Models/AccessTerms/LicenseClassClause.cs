using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Models.AccessAgreement;

namespace Prime.Models
{
    [Table("LicenseClassClause")]
    public class LicenseClassClause : BaseAuditable, IAccessClause
    {
        public LicenseClassClause()
        {
            // Create lists so they don't have be instantiated when items need to be added
            AccessTermLicenseClassClauses = new List<AccessTermLicenseClassClause>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Clause { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        [JsonIgnore]
        public List<AccessTermLicenseClassClause> AccessTermLicenseClassClauses { get; set; }
    }
}

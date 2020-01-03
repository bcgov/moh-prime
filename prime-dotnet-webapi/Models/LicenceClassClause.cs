using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prime.Models.AccessAgreement;

namespace Prime.Models
{
    [Table("LicenceClassClauses")]
    public class LicenceClassClause : BaseAuditable, IAccessClause
    {
        public LicenceClassClause()
        {
            // Create lists so they don't have be instantiated when items need to be added
            TermsOfAccesses = new List<TermsOfAccess>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Clause { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        public ICollection<TermsOfAccess> TermsOfAccesses { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prime.Models.AccessAgreement;

namespace Prime.Models
{
    [Table("UserClause")]
    public class UserClause : BaseAuditable, IAccessClause
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Clause { get; set; }

        public DateTime EffectiveDate { get; set; }

        [Required]
        public string EnrolleeClassification { get; set; }
    }
}

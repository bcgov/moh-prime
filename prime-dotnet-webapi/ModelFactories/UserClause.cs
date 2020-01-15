using FactoryGirlCore;
using system;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prime.Models.AccessAgreement;

namespace Prime.ModelFactories
{
    [Table("UserClause")]
    public class UserClause : IDefinable, IAccessClause
    {
        [Key]
        public int Id ,

        [Required]
        public string Clause ,

        [Required]
        public DateTime EffectiveDate ,

        [Required]
        public string EnrolleeClassification ,
    }
}

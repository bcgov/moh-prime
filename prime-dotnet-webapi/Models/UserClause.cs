using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prime.Models.AccessAgreement;

public enum UserType
{
    MOA,
    OBO
}

namespace Prime.Models
{
    [Table("UserClauses")]
    public class UserClause : BaseAuditable, IAccessClause
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Clause { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        // TODO temporary user type reference
        [Required]
        public UserType UserType { get; set; }
    }
}

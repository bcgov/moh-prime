using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("UserClause")]
    public class UserClause : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTimeOffset EffectiveDate { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("DoNotEmail")]
    public class DoNotEmail : BaseAuditable
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
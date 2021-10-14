using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pip.Models
{
    [Table("FirstModel")]
    public class FirstModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}

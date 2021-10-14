using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pip.Models
{
    [Table("NewModelLookup")]
    public class NewModel : ILookup<int>
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; } = "default name";
    }
}

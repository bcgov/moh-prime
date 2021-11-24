using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pidp.Models
{
    [Table("NewModelLookup")]
    public class NewModel : ILookup<int>
    {
        [Key]
        public int Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

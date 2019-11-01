using System.ComponentModel.DataAnnotations;

namespace Prime.Models
{
    public interface ILookup
    {
        [Key]
        short Code { get; set; }
        
        [Required]
        string Name { get; set; }
    }
}

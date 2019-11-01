using System.ComponentModel.DataAnnotations;

namespace Prime.Models
{
    public interface ILookup<T>
    {
        [Key]
        T Code { get; set; }
        
        [Required]
        string Name { get; set; }
    }
}

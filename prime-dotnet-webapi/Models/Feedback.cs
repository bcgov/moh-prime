using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("Feedback")]
    public class Feedback : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EnrolleeId { get; set; }

        [Required]
        public string route { get; set; }

        public string Comment { get; set; }
    }
}

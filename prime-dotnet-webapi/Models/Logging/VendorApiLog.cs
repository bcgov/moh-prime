using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("VendorApiLog")]
    public class VendorApiLog : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ServiceAccountUsername { get; set; }

        [Required]
        public string EndPoint { get; set; }

        public string Input { get; set; }
        public string Output { get; set; }
        public string ErrorMessage { get; set; }
    }
}

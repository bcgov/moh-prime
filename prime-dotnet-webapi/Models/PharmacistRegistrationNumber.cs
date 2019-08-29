using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prime.Models
{
    public class PharmacistRegistrationNumber
    {
        [Key]
        [Required]
        public int? Id { get; set; }
        [Required]
        public string Number { get; set; }

    }
}
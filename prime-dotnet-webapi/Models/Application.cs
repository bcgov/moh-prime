using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prime.Models
{
    public class Application
    {
        [Key]
        [Required]
        public int? Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string ApplicantName { get; set; }
        [Required]
        public string ApplicantId { get; set; }
        public bool? Approved { get; set; }
        public string ApprovedReason { get; set; }
        [ForeignKey("PharmacistRegistrationNumber")]
        public string PharmacistRegistrationNumberId { get; set; }

    }
}
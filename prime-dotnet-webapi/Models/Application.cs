using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prime.Models
{
    public class Application
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string ApplicantName { get; set; }
        [Required]
        public string ApplicantId { get; set; }
        public DateTime AppliedDate { get; set; }
        public bool? Approved { get; set; }
        public string ApprovedReason { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string PharmacistRegistrationNumber { get; set; }

    }
}
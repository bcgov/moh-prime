using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("IndividualDeviceProvider")]
    public class IndividualDeviceProvider : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CommunitySiteId { get; set; }

        [JsonIgnore]
        public CommunitySite CommunitySite { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}

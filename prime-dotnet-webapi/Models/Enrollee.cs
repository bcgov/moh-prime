using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Enrollee")]
    public class Enrollee
    {
        [Key]
        public int? Id { get; set; }

        [JsonIgnore]
        public ICollection<Enrolment> Enrolments { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public MailingAddress MailingAddress { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string VoicePhone { get; set; }

        public string VoiceExtension { get; set; }
    }
}
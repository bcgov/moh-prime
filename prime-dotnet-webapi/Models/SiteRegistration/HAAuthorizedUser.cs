using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("HAAuthorizedUser")]
    public class HAAuthorizedUser : BaseAuditable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        [Required]
        public HealthAuthorityCode HealthAuthorityCode { get; set; }
        [JsonIgnore]
        public HealthAuthority HealthAuthority { get; set; }
    }
}

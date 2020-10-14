using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("RemoteAccessLocation")]
    public class RemoteAccessLocation : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        [Required]
        public string InternetProvider { get; set; }

        [Required]
        public PhysicalAddress PhysicalAddress { get; set; }
    }
}

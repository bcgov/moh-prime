using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("RemoteUserLocation")]
    public class RemoteUserLocation : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int RemoteUserId { get; set; }

        [JsonIgnore]
        public RemoteUser RemoteUser { get; set; }

        [Required]
        public string InternetProvider { get; set; }

        [Required]
        public PhysicalAddress PhysicalAddress { get; set; }
    }
}

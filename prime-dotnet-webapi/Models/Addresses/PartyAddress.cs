using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class PartyAddress : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int PartyId { get; set; }

        [JsonIgnore]
        public Party Party { get; set; }

        public int AddressId { get; set; }

        [JsonIgnore]
        public Address Address { get; set; }
    }
}

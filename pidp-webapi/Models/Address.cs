using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Pidp.Models.Lookups;

namespace Pidp.Models
{
    public enum AddressType
    {
        Physical = 1,
        Mailng,
        Verified
    }

    [Table(nameof(Address))]
    public abstract class Address
    {
        [Key]
        public int Id { get; set; }

        public AddressType AddressType { get; set; }

        public CountryCode CountryCode { get; set; }

        [Required]
        public Country? Country { get; set; }

        public ProvinceCode ProvinceCode { get; set; }

        [Required]
        public Province? Province { get; set; }

        public string Street { get; set; } = string.Empty;

        public string Street2 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Postal { get; set; } = string.Empty;

        public bool IsInBC()
        {
            return ProvinceCode == ProvinceCode.BritishColumbia;
        }
    }

    public class PartyAddress : Address
    {
        public int PartyId { get; set; }

        [Required]
        public Party? Party { get; set; }
    }
}

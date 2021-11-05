using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SiteVendor")]
    public class SiteVendor : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int SiteId { get; set; }

        [JsonIgnore]
        public CommunitySite Site { get; set; }

        public int VendorCode { get; set; }

        [JsonIgnore]
        public Vendor Vendor { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("VendorLookup")]
    public class Vendor : BaseAuditable, ILookup<int>
    {
        [Key]
        public int Code { get; set; }

        public int OrganizationTypeCode { get; set; }

        public OrganizationType OrganizationType { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public string Email { get; set; }

        [JsonIgnore]
        public IEnumerable<SiteVendor> SiteVendors { get; set; }
    }
}

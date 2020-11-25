using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("BusinessLicence")]
    public class BusinessLicence : BaseAuditable
    {
        [Key]
        public int Id { get; set; }
        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }

        public string DeferredLicenceReason { get; set; }

        public bool Completed { get; set; }

        public BusinessLicenceDocument BusinessLicenceDocument { get; set; }
    }
}

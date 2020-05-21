using System;
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

        public Guid DocumentGuid { get; set; }

        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }
    }
}

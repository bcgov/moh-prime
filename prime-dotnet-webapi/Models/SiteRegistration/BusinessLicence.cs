using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("BusinessLicence")]
    public class BusinessLicence : BaseDocumentUpload
    {
        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }
    }
}

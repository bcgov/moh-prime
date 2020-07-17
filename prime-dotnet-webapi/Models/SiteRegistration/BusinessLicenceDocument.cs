using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("BusinessLicenceDocument")]
    public class BusinessLicenceDocument : BaseDocumentUpload
    {
        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }
    }
}

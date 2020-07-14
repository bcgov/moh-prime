using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SiteRegistrationReview")]
    public class SiteRegistrationReview : BaseDocumentUpload
    {
        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }

        public SiteRegistrationReview(int siteId, Guid documentGuid, string filename)
        {
            this.SiteId = siteId;
            this.DocumentGuid = documentGuid;
            this.Filename = filename;
            this.UploadedDate = DateTimeOffset.Now;
        }
    }
}

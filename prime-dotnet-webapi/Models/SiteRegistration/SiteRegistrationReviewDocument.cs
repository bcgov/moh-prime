using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SiteRegistrationReviewDocument")]
    public class SiteRegistrationReviewDocument : BaseDocumentUpload
    {
        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }

        public SiteRegistrationReviewDocument(int siteId, Guid documentGuid, string filename)
        {
            this.SiteId = siteId;
            this.DocumentGuid = documentGuid;
            this.Filename = filename;
            this.UploadedDate = DateTimeOffset.Now;
        }
    }
}

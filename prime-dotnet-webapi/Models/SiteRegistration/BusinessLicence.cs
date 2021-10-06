using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DelegateDecompiler;
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

        public DateTimeOffset? UploadedDate { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }

        public BusinessLicenceDocument BusinessLicenceDocument { get; set; }

        [NotMapped]
        [Computed]
        public bool Completed
        {
            get => BusinessLicenceDocument != null;
        }
    }
}

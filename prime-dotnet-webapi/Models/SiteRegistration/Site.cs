using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Prime.Models
{
    [Table("Site")]
    public class Site : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string PEC { get; set; }

        public bool Completed { get; set; }

        public DateTimeOffset? SubmittedDate { get; set; }

        public DateTimeOffset? ApprovedDate { get; set; }

        public int? ProvisionerId { get; set; }

        public Party Provisioner { get; set; }

        public int? LocationId { get; set; }

        public Location Location { get; set; }

        public int? VendorCode { get; set; }

        public Vendor Vendor { get; set; }

        [JsonIgnore]
        public ICollection<BusinessLicenceDocument> BusinessLicenceDocuments { get; set; }

        public IEnumerable<RemoteUser> RemoteUsers { get; set; }

        [JsonIgnore]
        public SiteRegistrationReviewDocument SiteRegistrationReviewDocument { get; set; }
    }
}

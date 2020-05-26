using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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

        public int? VendorId { get; set; }

        public Vendor Vendor { get; set; }

        public int? OrganizationTypeCode { get; set; }

        [JsonIgnore]
        public OrganizationType OrganizationType { get; set; }
    }
}

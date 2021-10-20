using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DelegateDecompiler;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("CommunitySite")]
    public class CommunitySite : Site
    {
        public CommunitySite()
        {
            BusinessLicences = new List<BusinessLicence>();
        }

        public int OrganizationId { get; set; }

        [JsonIgnore]
        public Organization Organization { get; set; }

        public int? AdministratorPharmaNetId { get; set; }

        public Contact AdministratorPharmaNet { get; set; }

        public int? PrivacyOfficerId { get; set; }

        public Contact PrivacyOfficer { get; set; }

        public int? TechnicalSupportId { get; set; }

        public Contact TechnicalSupport { get; set; }

        public int? ProvisionerId { get; set; }

        public Party Provisioner { get; set; }

        public ICollection<SiteVendor> SiteVendors { get; set; }

        public ICollection<BusinessLicence> BusinessLicences { get; set; }

        /// <summary>
        /// Gets the most recently uploaded business licence
        /// </summary>
        [NotMapped]
        [Computed]
        public BusinessLicence BusinessLicence
        {
            get => BusinessLicences
                .OrderByDescending(l => l.UploadedDate.HasValue)
                .ThenByDescending(l => l.UploadedDate)
                .FirstOrDefault();
        }

        /// <summary>
        /// Site submissions are considered renewals starting 90 days before the expiry of its current Business Licence.
        /// For sites without expiry dates on thier BL, expiry is considered to be one year after the Site's submitted date.
        /// </summary>
        public bool IsWithinRenewalPeriod()
        {
            if (SubmittedDate == null)
            {
                return false;
            }

            var expiryDate = BusinessLicence?.ExpiryDate ?? SubmittedDate.Value.AddYears(1);

            return DateTimeOffset.Now >= expiryDate.AddDays(-90);
        }
    }
}

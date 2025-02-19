using System;
using System.Collections.Generic;
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
        /// Expected to be in format P1-90XXX (last three characters are numbers).
        /// Applicable only for Device Provider care setting.
        /// </summary>
        public string DeviceProviderId { get; set; }

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
        /// Site submissions are considered renewals starting 30 days before the expiry of its current Business Licence.
        /// For sites without expiry dates on their BL, expiry is considered to be one year after the Site's submitted date.
        /// </summary>
        public bool IsWithinRenewalPeriod()
        {
            if (SubmittedDate == null)
            {
                return false;
            }

            var expiryDate = BusinessLicence?.ExpiryDate ?? SubmittedDate.Value.AddYears(1);

            return DateTimeOffset.Now >= expiryDate.AddDays(-30);
        }

        [NotMapped]
        public int? RemoteAccessTypeCode { get; set; }
    }
}

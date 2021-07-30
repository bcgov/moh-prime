using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DelegateDecompiler;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Site")]
    public class Site : BaseAuditable
    {
        public Site()
        {
            // Initialize collections to prevent null exception on computed properties
            // like `Status`
            SiteStatuses = new List<SiteStatus>();
            BusinessLicences = new List<BusinessLicence>();
        }


        [Key]
        public int Id { get; set; }

        public int OrganizationId { get; set; }

        [JsonIgnore]
        public Organization Organization { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public int? AdministratorPharmaNetId { get; set; }

        public Contact AdministratorPharmaNet { get; set; }

        public int? PrivacyOfficerId { get; set; }

        public Contact PrivacyOfficer { get; set; }

        public int? TechnicalSupportId { get; set; }

        public Contact TechnicalSupport { get; set; }

        public int? ProvisionerId { get; set; }

        public Party Provisioner { get; set; }

        public int? CareSettingCode { get; set; }

        [JsonIgnore]
        public CareSetting CareSetting { get; set; }

        public string PEC { get; set; }

        public string DoingBusinessAs { get; set; }

        public bool Completed { get; set; }

        public bool Flagged { get; set; }

        public DateTimeOffset? SubmittedDate { get; set; }

        public ICollection<SiteStatus> SiteStatuses { get; set; }

        public DateTimeOffset? ApprovedDate { get; set; }

        public ICollection<SiteVendor> SiteVendors { get; set; }

        public ICollection<BusinessLicence> BusinessLicences { get; set; }

        public ICollection<RemoteUser> RemoteUsers { get; set; }

        public int? AdjudicatorId { get; set; }

        public Admin Adjudicator { get; set; }

        [JsonIgnore]
        public ICollection<SiteRegistrationReviewDocument> SiteRegistrationReviewDocuments { get; set; }

        [JsonIgnore]
        public ICollection<SiteAdjudicationDocument> SiteAdjudicationDocuments { get; set; }

        public ICollection<SiteRegistrationNote> SiteRegistrationNotes { get; set; }

        public ICollection<BusinessDay> BusinessHours { get; set; }
        /// <summary>
        /// Days in which the business has any business hours.
        /// Only the time portion of the input parameter is considered.
        /// </summary>
        public IEnumerable<DayOfWeek> DaysOpen(DateTimeOffset? atTime = null)
        {
            return BusinessHours
                .Where(h => atTime == null || h.IsOpen(atTime.Value))
                .Select(b => b.Day)
                .Distinct();
        }

        public SiteStatus AddStatus(SiteStatusType siteStatusType)
        {
            var newStatus = SiteStatus.FromType(siteStatusType, Id);

            if (SiteStatuses == null)
            {
                SiteStatuses = new List<SiteStatus>();
            }
            SiteStatuses.Add(newStatus);

            return newStatus;
        }

        /// <summary>
        /// Gets the most recent Status of the Site.
        /// </summary>
        [NotMapped]
        [Computed]
        public SiteStatusType Status
        {
            get => SiteStatuses.Count > 0
                ? SiteStatuses
                    .OrderByDescending(s => s.StatusDate)
                    .ThenByDescending(s => s.Id)
                    .FirstOrDefault().StatusType
                : SiteStatusType.Editable;
        }

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
    }
}

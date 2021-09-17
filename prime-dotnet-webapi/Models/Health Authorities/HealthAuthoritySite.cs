using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Newtonsoft.Json;

namespace Prime.Models.HealthAuthorities
{
    [Table("HealthAuthoritySite")]
    public class HealthAuthoritySite : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int HealthAuthorityOrganizationId { get; set; }

        [JsonIgnore]
        public HealthAuthorityOrganization Organization { get; set; }

        public int VendorCode { get; set; }

        // public int? HealthAuthorityVendorId { get; set; }
        //
        // [JsonIgnore]
        // public HealthAuthorityVendor HealthAuthorityVendor { get; set; }

        public string SiteName { get; set; }

        public string SiteId { get; set; }

        public int SecurityGroupCode { get; set; }

        public string CareType { get; set; }

        // TODO list of care types?
        // public int? HealthAuthorityCareTypeId { get; set; }

        // [JsonIgnore]
        // public HealthAuthorityCareType HealthAuthorityCareType { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public ICollection<BusinessDay> BusinessHours { get; set; }

        public ICollection<RemoteUser> RemoteUsers { get; set; }

        public int? HealthAuthorityPharmanetAdministratorId { get; set; }

        public HealthAuthorityPharmanetAdministrator HealthAuthorityPharmanetAdministrator { get; set; }

        public int? HealthAuthorityTechnicalSupportId { get; set; }

        public HealthAuthorityTechnicalSupport HealthAuthorityTechnicalSupport { get; set; }

        public bool Completed { get; set; }

        public DateTimeOffset? SubmittedDate { get; set; }

        public DateTimeOffset? ApprovedDate { get; set; }

        public SiteStatusType Status { get; set; }

        public int? AdjudicatorId { get; set; }

        public Admin Adjudicator { get; set; }

        public string PEC { get; set; }

        [JsonIgnore]
        public ICollection<SiteRegistrationReviewDocument> SiteRegistrationReviewDocuments { get; set; }

        [JsonIgnore]
        public ICollection<SiteAdjudicationDocument> SiteAdjudicationDocuments { get; set; }

        public ICollection<SiteRegistrationNote> SiteRegistrationNotes { get; set; }

        public int? ProvisionerId { get; set; }

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
    }
}

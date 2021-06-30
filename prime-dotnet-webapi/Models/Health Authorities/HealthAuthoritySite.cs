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

        public PhysicalAddress PhysicalAddress { get; set; }

        public int? ProvisionerId { get; set; }

        public HealthAuthorityContact SiteAdministrator { get; set; }

        public string PEC { get; set; }

        public string SecurityGroup { get; set; }

        public HealthAuthorityVendor Vendor { get; set; }

        public bool Completed { get; set; }

        public DateTimeOffset? SubmittedDate { get; set; }

        public SiteStatusType Status { get; set; }

        public DateTimeOffset? ApprovedDate { get; set; }

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
    }
}

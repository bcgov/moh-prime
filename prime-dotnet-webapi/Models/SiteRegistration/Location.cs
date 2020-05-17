using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Location")]
    public class Location : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string DoingBusinessAs { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public int? AdministratorPharmaNetId { get; set; }

        public Party AdministratorPharmaNet { get; set; }

        public int? PrivacyOfficerId { get; set; }

        public Party PrivacyOfficer { get; set; }

        public int? TechnicalSupportId { get; set; }

        public Party TechnicalSupport { get; set; }

        public int? OrganizationId { get; set; }

        public Organization Organization { get; set; }

        [JsonIgnore]
        public IEnumerable<Site> Sites { get; set; }

        public ICollection<BusinessDay> BusinessDays { get; set; }

        /// <summary>
        /// Days in which the business has any business hours.
        /// Only the time portion of the input parameter is considered.
        /// </summary>
        public IEnumerable<DayOfWeek> DaysOpen(DateTimeOffset? atTime = null)
        {
            return BusinessDays
                .Where(h => atTime == null || h.IsOpen(atTime.Value))
                .Select(b => b.Day)
                .Distinct();
        }
    }
}

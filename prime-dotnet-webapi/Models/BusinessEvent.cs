using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("BusinessEvent")]
    public class BusinessEvent : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int? EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int? AdminId { get; set; }

        [NotMapped]
        public string AdminIDIR
        {
            get => this.Admin?.IDIR;
        }

        [JsonIgnore]
        public Admin Admin { get; set; }

        public int? PartyId { get; set; }

        [JsonIgnore]
        public Party Party { get; set; }

        public int? SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }

        public int? OrganizationId { get; set; }

        [JsonIgnore]
        public Organization Organization { get; set; }

        public int BusinessEventTypeCode { get; set; }

        [JsonIgnore]
        public BusinessEventType BusinessEventType { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? EventDate { get; set; }
    }
}

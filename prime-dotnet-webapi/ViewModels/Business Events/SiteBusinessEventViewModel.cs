using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class SiteBusinessEventViewModel
    {
        public int Id { get; set; }

        public int? AdminId { get; set; }

        public string AdminIDIR
        {
            get => Admin?.IDIR;
        }

        public Admin Admin { get; set; }

        public int? PartyId { get; set; }

        public Party Party { get; set; }

        public string PartyName { get; set; }

        public int? SiteId { get; set; }

        public Site Site { get; set; }

        public int? OrganizationId { get; set; }

        public Organization Organization { get; set; }

        public int BusinessEventTypeCode { get; set; }

        public BusinessEventType BusinessEventType { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? EventDate { get; set; }
    }
}

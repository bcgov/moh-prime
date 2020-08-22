using System;
using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels
{
    public class SiteListViewModel
    {
        public int Id { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public string DoingBusinessAs { get; set; }

        public DateTimeOffset? SubmittedDate { get; set; }

        public int? OrganizationTypeCode { get; set; }

        public IEnumerable<SiteVendor> SiteVendors { get; set; }

        public string PEC { get; set; }
    }
}

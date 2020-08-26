using System;
using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels
{
    public class SiteListViewModel
    {
        public int Id { get; set; }

        public int? CareSettingCode { get; set; }

        public IEnumerable<SiteVendor> SiteVendors { get; set; }

        public string DoingBusinessAs { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public DateTimeOffset? SubmittedDate { get; set; }

        public bool Completed { get; set; }

        public string PEC { get; set; }
    }
}

using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels
{
    public class PaperEnrolleeOboSiteViewModel
    {
        public ICollection<Job> Jobs { get; set; }

        public ICollection<OboSite> OboSites { get; set; }
    }
}

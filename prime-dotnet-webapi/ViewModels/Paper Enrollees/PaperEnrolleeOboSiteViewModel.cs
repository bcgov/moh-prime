using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels.PaperEnrollee
{
    public class PaperEnrolleeOboSiteViewModel
    {
        public ICollection<Job> Jobs { get; set; }

        public ICollection<OboSite> OboSites { get; set; }
    }
}

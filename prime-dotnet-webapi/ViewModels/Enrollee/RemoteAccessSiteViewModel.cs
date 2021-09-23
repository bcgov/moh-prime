using Prime.Models;

namespace Prime.ViewModels
{
    public class RemoteAccessSiteViewModel
    {
        public int Id { get; set; }
        public int EnrolleeId { get; set; }
        public int SiteId { get; set; }
        public Site Site { get; set; }
    }
}

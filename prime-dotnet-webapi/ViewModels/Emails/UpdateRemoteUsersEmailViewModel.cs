using System.Collections.Generic;

namespace Prime.ViewModels.Emails
{
    public class RemoteUsersUpdatedEmailViewModel
    {
        public string SiteStreetAddress { get; set; }
        public string OrganizationName { get; set; }
        public string SitePec { get; set; }
        public IEnumerable<string> RemoteUsers { get; set; }
        public string DocumentUrl { get; set; }
    }
}

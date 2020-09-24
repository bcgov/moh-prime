using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolleeRemoteAccessSiteViewModel
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public PhysicalAddress PhysicalAddress { get; set; }
        public IEnumerable<RemoteUser> RemoteUsers { get; set; }
        public IEnumerable<SiteVendor> SiteVendors { get; set; }
    }
}

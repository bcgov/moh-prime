using System;

namespace Prime.ViewModels
{
    public class OrganizationAdminListViewModel
    {
        public int Id { get; set; }
        public int DisplayId { get; set; }
        public string Name { get; set; }
        public string DoingBusinessAs { get; set; }
        public string SigningAuthorityName { get; set; }
        public DateTime CreatedDate { get; set; }

        //Number of site with care setting set
        public int ValidSiteCount { get; set; }

        //First Site ID with PEC
        public int? SiteId { get; set; }
        public bool HasClaim { get; set; }
        public bool HasSubmittedSite { get; set; }
    }
}

using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class CommunitySiteAdminListViewModel
    {
        public int Id { get; set; }
        public bool HasNotification { get; set; }
        public int DisplayId { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string SigningAuthorityName { get; set; }
        public string DoingBusinessAs { get; set; }
        public DateTimeOffset? SubmittedDate { get; set; }
        public DateTimeOffset? ApprovedDate { get; set; }
        public string AdjudicatorIdir { get; set; }
        public SiteStatusType Status { get; set; }
        public string PEC { get; set; }
        public int? CareSettingCode { get; set; }
        public bool MissingBusinessLicence { get; set; }
        public int RemoteUserCount { get; set; }
        public bool Flagged { get; set; }
        public bool IsNew { get; set; }
        public int DuplicatePecSiteCount { get; set; }
        public bool HasClaim { get; set; }
    }
}

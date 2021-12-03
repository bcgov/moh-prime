using System.Collections.Generic;
using Prime.Contracts;

namespace Prime.ViewModels
{
    public class SendSiteEmailModel : SendSiteEmail
    {
        public SiteEmailType EmailType { get; set; }
        public int Id { get; set; }
        public int BusinessLicenceId { get; set; }
        public int CareSettingCode { get; set; }
        public string DoingBusinessAs { get; set; }
        public string PhysicalAddressStreet { get; set; }
        public string PhysicalAddressCity { get; set; }
        public string OrganizationName { get; set; }
        public string AdministratorPharmaNetEmail { get; set; }
        public string AdjudicatorEmail { get; set; }
        public string ProvisionerEmail { get; set; }
        public string OrganizationSigningAuthorityEmail { get; set; }
        public string PEC { get; set; }
        public IEnumerable<string> RemoteUserNames { get; set; }
        public IEnumerable<string> RemoteUserEmails { get; set; }
        public string Note { get; set; }
    }
}

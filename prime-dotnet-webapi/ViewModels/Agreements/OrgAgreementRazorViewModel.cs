using System;

namespace Prime.ViewModels.Agreements
{
    public class OrgAgreementRazorViewModel
    {
        public string OrganizationName { get; set; }
        public DateTimeOffset AcceptedDate { get; set; }

        public OrgAgreementRazorViewModel(string organizationName, DateTimeOffset acceptedDate)
        {
            OrganizationName = organizationName;
            AcceptedDate = acceptedDate;
        }
    }
}

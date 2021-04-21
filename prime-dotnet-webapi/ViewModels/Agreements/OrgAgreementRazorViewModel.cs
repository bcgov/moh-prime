using System;

namespace Prime.ViewModels.Agreements
{
    public class OrgAgreementRazorViewModel
    {
        public string OrganizationName { get; set; }
        public DateTimeOffset AcceptedDate { get; set; }
        public bool WithSignature { get; set; }

        public OrgAgreementRazorViewModel(string organizationName, DateTimeOffset acceptedDate, bool withSignature)
        {
            OrganizationName = organizationName;
            AcceptedDate = acceptedDate;
            WithSignature = withSignature;
        }
    }
}

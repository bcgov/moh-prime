using System;

namespace Prime.ViewModels.Agreements
{
    public class OrgAgreementRazorViewModel
    {
        public DateTimeOffset AcceptedDate { get; set; }
        public bool WithSignature { get; set; }
        public string AgreementContent { get; set; }
        public string ScheduleContent { get; set; }

        public OrgAgreementRazorViewModel(DateTimeOffset acceptedDate, bool withSignature, string agreementContent, string scheduleContent)
        {
            AcceptedDate = acceptedDate;
            WithSignature = withSignature;
            AgreementContent = agreementContent;
            ScheduleContent = scheduleContent;
        }
    }
}

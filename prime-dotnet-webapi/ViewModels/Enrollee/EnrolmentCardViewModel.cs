using System;
using Newtonsoft.Json;
using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolmentCardViewModel
    {
        public int AgreementId { get; set; }

        public DateTimeOffset? AgreementAcceptedDate { get; set; }

        [JsonIgnore]
        public Submission Submission { get; set; }

        public bool RequestedRemoteAccess
        {
            get
            {
                return Submission.RequestedRemoteAccess;
            }
        }
    }
}

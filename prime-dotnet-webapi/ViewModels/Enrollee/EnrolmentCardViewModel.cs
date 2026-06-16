using System;
using Newtonsoft.Json;
using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolmentCardViewModel
    {
        public int AgreementId { get; set; }
        public int SubmissionId { get; set; }
        public string AgreementType { get; set; }

        public DateTimeOffset? AgreementAcceptedDate { get; set; }

        public DateTimeOffset? EnrolmentApprovedDate { get; set; }

        public bool IsCurrent { get; set; }

        [JsonIgnore]
        public Submission Submission { get; set; }


        public bool RequestedRemoteAccess
        {
            get
            {
                return Submission.RequestedRemoteAccess;
            }
        }

        public DateTimeOffset? SubmissionCreatedDate
        {
            get
            {
                return Submission.CreatedDate;
            }
        }
    }
}

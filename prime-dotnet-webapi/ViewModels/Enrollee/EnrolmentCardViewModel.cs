using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolmentCardViewModel
    {
        public int AgreementId { get; set; }

        public DateTimeOffset? AgreementAcceptedDate { get; set; }

        [JsonIgnore]
        public Submission Submission { get; set; }

        public string RemoteAccess
        {
            get
            {
                var enrolleeRemoteUsers = Submission.ProfileSnapshot.Value<JArray>("enrolleeRemoteUsers");

                if (enrolleeRemoteUsers != null)
                {
                    if (enrolleeRemoteUsers.Count > 0 && AgreementAcceptedDate != null)
                    {
                        return (AgreementAcceptedDate == null)
                            ? "User Requested Remote Access"
                            : "User Approved Remote Access";
                    }
                }
                return null;
            }
        }
    }
}

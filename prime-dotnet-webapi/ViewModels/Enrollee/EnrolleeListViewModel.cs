using System;
using System.Collections.Generic;

using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolleeListViewModel
    {
        public int Id { get; set; }

        public int DisplayId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GivenNames { get; set; }

        public int CurrentStatusCode { get; set; }

        public DateTimeOffset? AppliedDate { get; set; }

        public DateTimeOffset? ApprovedDate { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }

        public bool HasNewestAgreement { get; set; }

        public string CurrentTOAStatus
        {
            get
            {
                switch ((StatusType)CurrentStatusCode)
                {
                    case StatusType.UnderReview:
                        return "";
                    case StatusType.Locked:
                    case StatusType.Declined:
                        return "N/A";
                    case StatusType.RequiresToa:
                        return "Pending";
                    case StatusType.Editable:
                        if (ExpiryDate == null || DateTimeOffset.Now >= ExpiryDate)
                        {
                            return "";
                        }
                        else
                        {
                            return HasNewestAgreement ? "Yes" : "No";
                        }
                    default:
                        return null;
                }
            }
        }

        public string AdjudicatorIdir { get; set; }

        public bool AlwaysManual { get; set; }

        public bool RemoteAccess { get; set; }

        public ICollection<int> CareSettingCodes { get; set; }

        public bool HasNotification { get; set; }

        public bool RequiresConfirmation { get; set; }

        public bool Confirmed { get; set; }

        public string GPID { get; set; }

        public int LinkedEnrolleeId { get; set; }

        public bool PossiblePaperEnrolmentMatch { get; set; }
    }
}

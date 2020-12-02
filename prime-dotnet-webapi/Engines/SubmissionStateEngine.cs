using System;

using Prime.Models;
using Prime.Models.Api;

namespace Prime.Engines
{
    public static class SubmissionStateEngine
    {
        public static bool AllowableAction(SubmissionAction action, Enrollee enrollee, bool asAdmin)
        {
            enrollee.ThrowIfNull(nameof(enrollee));
            if (enrollee.CurrentStatus == null)
            {
                throw new ArgumentException("Enrollee must have a CurrentStatus", nameof(enrollee));
            }

            var status = enrollee.CurrentStatus.GetStatusType();

            return (status, action) switch
            {
                (StatusType.Editable, SubmissionAction.LockProfile)       => asAdmin,
                (StatusType.Editable, SubmissionAction.DeclineProfile)    => asAdmin,

                (StatusType.UnderReview, SubmissionAction.Approve)        => asAdmin,
                (StatusType.UnderReview, SubmissionAction.EnableEditing)  => asAdmin,
                (StatusType.UnderReview, SubmissionAction.LockProfile)    => asAdmin,
                (StatusType.UnderReview, SubmissionAction.DeclineProfile) => asAdmin,
                (StatusType.UnderReview, SubmissionAction.RerunRules)     => asAdmin,

                (StatusType.RequiresToa, SubmissionAction.AcceptToa)      => !asAdmin,
                (StatusType.RequiresToa, SubmissionAction.DeclineToa)     => !asAdmin,
                (StatusType.RequiresToa, SubmissionAction.EnableEditing)  => asAdmin,
                (StatusType.RequiresToa, SubmissionAction.LockProfile)    => asAdmin,
                (StatusType.RequiresToa, SubmissionAction.DeclineProfile) => asAdmin,

                (StatusType.Locked, SubmissionAction.EnableEditing)       => asAdmin,
                (StatusType.Locked, SubmissionAction.DeclineProfile)      => asAdmin,

                (StatusType.Declined, SubmissionAction.EnableEditing)     => asAdmin,

                _ => false
            };
        }
    }
}

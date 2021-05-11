using Prime.Models;
using Prime.Models.Api;

namespace Prime.Engines
{
    public static class SubmissionStateEngine
    {
        public static bool AllowableAction(SubmissionAction action, EnrolmentStatus currentStatus)
        {
            currentStatus.ThrowIfNull(nameof(currentStatus));

            return (currentStatus.GetStatusType(), action) switch
            {
                (StatusType.Editable, SubmissionAction.LockProfile)             => true,
                (StatusType.Editable, SubmissionAction.DeclineProfile)          => true,

                (StatusType.UnderReview, SubmissionAction.Approve)              => true,
                (StatusType.UnderReview, SubmissionAction.EnableEditing)        => true,
                (StatusType.UnderReview, SubmissionAction.LockProfile)          => true,
                (StatusType.UnderReview, SubmissionAction.DeclineProfile)       => true,
                (StatusType.UnderReview, SubmissionAction.RerunRules)           => true,

                (StatusType.RequiresToa, SubmissionAction.AcceptToa)            => true,
                (StatusType.RequiresToa, SubmissionAction.DeclineToa)           => true,
                (StatusType.RequiresToa, SubmissionAction.EnableEditing)        => true,
                (StatusType.RequiresToa, SubmissionAction.LockProfile)          => true,
                (StatusType.RequiresToa, SubmissionAction.DeclineProfile)       => true,
                (StatusType.RequiresToa, SubmissionAction.CancelToaAssignment)  => true,

                (StatusType.Locked, SubmissionAction.EnableEditing)             => true,
                (StatusType.Locked, SubmissionAction.DeclineProfile)            => true,

                (StatusType.Declined, SubmissionAction.EnableEditing)           => true,

                _ => false
            };
        }
    }
}

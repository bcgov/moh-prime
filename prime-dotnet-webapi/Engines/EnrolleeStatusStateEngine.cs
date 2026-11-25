using Prime.Models;
using Prime.Models.Api;

namespace Prime.Engines
{
    public static class EnrolleeStatusStateEngine
    {
        public static bool AllowableAction(EnrolleeStatusAction action, EnrolmentStatus currentStatus)
        {
            currentStatus.ThrowIfNull(nameof(currentStatus));

            return (currentStatus.GetStatusType(), action) switch
            {
                (StatusType.Editable, EnrolleeStatusAction.LockProfile) => true,
                (StatusType.Editable, EnrolleeStatusAction.DeclineProfile) => true,
                (StatusType.Editable, EnrolleeStatusAction.ChangeToa) => true,
                (StatusType.Editable, EnrolleeStatusAction.ArchiveProfile) => true,

                (StatusType.UnderReview, EnrolleeStatusAction.Approve) => true,
                (StatusType.UnderReview, EnrolleeStatusAction.EnableEditing) => true,
                (StatusType.UnderReview, EnrolleeStatusAction.LockProfile) => true,
                (StatusType.UnderReview, EnrolleeStatusAction.DeclineProfile) => true,
                (StatusType.UnderReview, EnrolleeStatusAction.RerunRules) => true,
                (StatusType.UnderReview, EnrolleeStatusAction.ArchiveProfile) => true,

                (StatusType.RequiresToa, EnrolleeStatusAction.AcceptToa) => true,
                (StatusType.RequiresToa, EnrolleeStatusAction.DeclineToa) => true,
                (StatusType.RequiresToa, EnrolleeStatusAction.EnableEditing) => true,
                (StatusType.RequiresToa, EnrolleeStatusAction.LockProfile) => true,
                (StatusType.RequiresToa, EnrolleeStatusAction.DeclineProfile) => true,
                (StatusType.RequiresToa, EnrolleeStatusAction.CancelToaAssignment) => true,
                (StatusType.RequiresToa, EnrolleeStatusAction.ArchiveProfile) => true,

                (StatusType.Locked, EnrolleeStatusAction.UnlockedProfile) => true,
                (StatusType.Locked, EnrolleeStatusAction.DeclineProfile) => true,

                (StatusType.Declined, EnrolleeStatusAction.EnableEditing) => true,

                (StatusType.Archived, EnrolleeStatusAction.RestoreProfile) => true,

                _ => false
            };
        }
    }
}

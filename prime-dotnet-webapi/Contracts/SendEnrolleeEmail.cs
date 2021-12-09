using Prime.Models;

namespace Prime.Contracts
{
    public interface SendEnrolleeEmail
    {
        EnrolleeEmailType EmailType { get; }
        int EnrolleeId { get; }
    }

    public enum EnrolleeEmailType
    {
        Reminder,
        PaperEnrolmentSubmission,
        EnrolleeRenewal,
        UnsignedToaReminder
    }
}

namespace Prime.ViewModels
{
    public class EnrolmentStatusReasonViewModel
    {
        public int StatusReasonCode { get; set; }

        public StatusReasonViewModel StatusReason { get; set; }

        public string ReasonNote { get; set; }
    }
}

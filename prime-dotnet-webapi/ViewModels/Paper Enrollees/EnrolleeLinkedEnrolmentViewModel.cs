using System;
namespace Prime.ViewModels.PaperEnrollees
{
    public class EnrolleeLinkedEnrolmentViewModel
    {
        public int EnrolleeId { get; set; }
        public int? PaperEnrolleeId { get; set; }
        public string UserProvidedGpid { get; set; }
        public DateTime? EnrolmentLinkDate { get; set; }
    }
}

namespace Prime.Models.Api
{
    public class EnrolleeSearchOptions
    {
        public bool? IsLinkedPaperEnrolment { get; set; }
        public int? StatusCode { get; set; }
        public string TextSearch { get; set; }
    }
}

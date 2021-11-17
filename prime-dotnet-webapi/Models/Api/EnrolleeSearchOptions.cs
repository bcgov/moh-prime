namespace Prime.Models.Api
{
    public class EnrolleeSearchOptions
    {
        public int? StatusCode { get; set; }
        public bool? IsLinkedPaperEnrolment { get; set; }
        public string TextSearch { get; set; }
    }
}

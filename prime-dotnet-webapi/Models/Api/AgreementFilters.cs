namespace Prime.Models.Api
{
    public class AgreementFilters
    {
        public int? YearAccepted { get; set; }
        public bool OnlyLatest { get; set; }
        public bool? Accepted { get; set; }
        public bool IncludeText { get; set; }
    }
}

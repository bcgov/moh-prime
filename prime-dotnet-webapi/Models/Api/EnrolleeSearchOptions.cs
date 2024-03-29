using System;

namespace Prime.Models.Api
{
    public class EnrolleeSearchOptions
    {
        public bool? IsLinkedPaperEnrolment { get; set; }
        public bool? IsRenewedManualEnrolment { get; set; }
        public int? StatusCode { get; set; }
        public string TextSearch { get; set; }
        public int? Page { get; set; }
        public string SortOrder { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? RenewalDateRangeStart { get; set; }
        public DateTime? RenewalDateRangeEnd { get; set; }
        public DateTime? AppliedDateRangeStart { get; set; }
        public DateTime? AppliedDateRangeEnd { get; set; }
    }
}

using System;

namespace Prime.Models.Api
{
    public class HealthAuthoritySiteSearchOptions
    {
        public string TextSearch { get; set; }
        public bool AssignToMe { get; set; }
        public string AdminUserName { get; set; }
        public int? VendorId { get; set; }
        public string CareType { get; set; }
        public int? StatusId { get; set; }
    }
}

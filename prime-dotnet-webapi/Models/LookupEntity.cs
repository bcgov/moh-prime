using System.Collections.Generic;

namespace Prime.Models
{
    public class LookupEntity
    {
        public List<College> Colleges { get; set; }
        public List<JobName> JobNames { get; set; }
        public List<License> Licenses { get; set; }
        public List<OrganizationType> OrganizationTypes { get; set; }
        public List<Practice> Practices { get; set; }
        public List<Status> Statuses { get; set; }
        public List<Country> Countries { get; set; }
        public List<Province> Provinces { get; set; }
        public List<StatusReason> StatusReasons { get; set; }
    }
}

using System.Collections.Generic;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels;

namespace Prime.Models.Api
{
    public class LookupEntity
    {
        public List<College> Colleges { get; set; }
        public List<JobName> JobNames { get; set; }
        public List<LicenseViewModel> Licenses { get; set; }
        public List<CareSetting> CareSettings { get; set; }
        public List<Practice> Practices { get; set; }
        public List<Status> Statuses { get; set; }
        public List<Country> Countries { get; set; }
        public List<Province> Provinces { get; set; }
        public List<StatusReason> StatusReasons { get; set; }
        public List<Vendor> Vendors { get; set; }
        public List<HealthAuthority> HealthAuthorities { get; set; }
        public List<Facility> Facilities { get; set; }
        public List<CollegeLicenseGrouping> CollegeLicenseGroupings { get; set; }
        public List<CareType> CareTypes { get; set; }
        public List<SecurityGroup> SecurityGroups { get; set; }
        public List<DeviceProviderRole> DeviceProviderRoles { get; set; }
    }
}

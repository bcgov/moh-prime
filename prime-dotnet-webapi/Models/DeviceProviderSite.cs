using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("DeviceProviderSite")]
    public class DeviceProviderSite : BaseAuditable
    {
        [Key]
        public string SiteId { get; set; }

        public string DpId { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public string City { get; set; }
        public string Prov { get; set; }
        public string PC { get; set; }
        public string MgrFirst { get; set; }
        public string ManagerLast { get; set; }
        public string PhoneNumber { get; set; }
    }
}

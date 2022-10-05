using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.Api
{
    [NotMapped]
    public class HpdidLookup : EnrolleeLookup
    {
        public string Hpdid { get; set; }
    }

    [NotMapped]
    public class EnrolleeLookup
    {
        public string Gpid { get; set; }
        public DateTimeOffset? RenewalDate { get; set; }

        /// <summary>
        /// Something like "Independent User" or "On-behalf-of User", or <c>null</c> if no TOA has been assigned yet
        /// </summary>
        public string AccessType { get; set; }
        public IEnumerable<EnrolleeCertDto> Licences { get; set; }
    }
}

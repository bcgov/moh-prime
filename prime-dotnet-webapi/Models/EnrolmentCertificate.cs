using System;
using System.Linq;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [NotMapped]
    public sealed class EnrolmentCertificate
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string GPID { get; set; }
        public DateTimeOffset? ExpiryDate { get; set; }
        public IEnumerable<OrganizationType> OrganizationTypes { get; set; }

        public static EnrolmentCertificate Create(Enrollee enrollee)
        {
            // Display information from profile history from last submission before most recently accepted TOA
            // Display GPID and Expiry Date from current enrollee object because inital submission will
            // not have these fields until after accepting the TOA
            return new EnrolmentCertificate
            {
                FirstName = enrollee.FirstName,
                MiddleName = enrollee.MiddleName,
                LastName = enrollee.LastName,
                GPID = enrollee.GPID,
                ExpiryDate = enrollee.ExpiryDate,
                OrganizationTypes = enrollee.EnrolleeOrganizationTypes.Select(org => org.OrganizationType)
            };
        }
    }
}

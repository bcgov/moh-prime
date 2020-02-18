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
        public string PreferredFirstName { get; set; }
        public string PreferredMiddleName { get; set; }
        public string PreferredLastName { get; set; }
        public string GPID { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public IEnumerable<OrganizationType> OrganizationTypes { get; set; }

        public static EnrolmentCertificate Create(Enrollee enrolleeHistory, Enrollee enrollee)
        {
            // Display information from profile history from last submission before more recently accepted TOA
            // Display GPID and Expiry Date from current enrollee object because inital submission will
            // not have these fields until after accepting the TOA
            return new EnrolmentCertificate
            {
                FirstName = enrolleeHistory.FirstName,
                MiddleName = enrolleeHistory.MiddleName,
                LastName = enrolleeHistory.LastName,
                PreferredFirstName = enrolleeHistory.PreferredFirstName,
                PreferredMiddleName = enrolleeHistory.PreferredMiddleName,
                PreferredLastName = enrolleeHistory.PreferredLastName,
                GPID = enrollee.GPID,
                ExpiryDate = enrollee.ExpiryDate,
                OrganizationTypes = enrolleeHistory.Organizations.Select(org => org.OrganizationType)
            };
        }
    }
}

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

        public static EnrolmentCertificate Create(Enrollee enrollee)
        {
            return new EnrolmentCertificate
            {
                FirstName = enrollee.FirstName,
                MiddleName = enrollee.MiddleName,
                LastName = enrollee.LastName,
                PreferredFirstName = enrollee.PreferredFirstName,
                PreferredMiddleName = enrollee.PreferredMiddleName,
                PreferredLastName = enrollee.PreferredLastName,
                GPID = enrollee.GPID,
                ExpiryDate = enrollee.ExpiryDate,
                OrganizationTypes = enrollee.Organizations.Select(org => org.OrganizationType)
            };
        }
    }
}

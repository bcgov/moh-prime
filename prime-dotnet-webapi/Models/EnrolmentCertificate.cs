using System;
using System.Linq;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

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
        public DateTime DateOfBirth { get; set; }
        public string LicensePlate { get; set; }
        public IEnumerable<Privilege> Privileges { get; set; }
        public ICollection<OrganizationType> OrganizationTypes { get; set; }

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
                DateOfBirth = enrollee.DateOfBirth,
                LicensePlate = enrollee.LicensePlate,
                Privileges = enrollee.AssignedPrivileges.Select(ap => ap.Privilege)
            };
        }
    }
}

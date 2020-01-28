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
        public DateTime DateOfBirth { get; set; }
        public string LicensePlate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public IEnumerable<OrganizationType> OrganizationTypes { get; set; }

        public EnrolmentCertificateNote EnrolmentCertificateNote { get; set; }
        public IEnumerable<Privilege> Transactions { get; set; }
        public Privilege UserType { get; set; }
        public Privilege CanHaveOBOs { get; set; }

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
                LicensePlate = enrollee.LicensePlate,
                ExpiryDate = enrollee.ExpiryDate,
                OrganizationTypes = enrollee.Organizations.Select(org => org.OrganizationType),
                EnrolmentCertificateNote = enrollee.EnrolmentCertificateNote,
                Transactions = enrollee.AssignedPrivileges
                    .Select(ap => ap.Privilege)
                    .Where(p => p.PrivilegeGroup.PrivilegeTypeCode == PrivilegeType.PHARMANET_TRANSACTIONS),
                UserType = enrollee.AssignedPrivileges
                    .Select(ap => ap.Privilege)
                    .Where(p => p.PrivilegeGroupCode == PrivilegeGroup.USER_TYPE)
                    .SingleOrDefault(),
                CanHaveOBOs = enrollee.AssignedPrivileges
                    .Select(ap => ap.Privilege)
                    .Where(p => p.PrivilegeGroupCode == PrivilegeGroup.CAN_HAVE_OBOS)
                    .SingleOrDefault(),
            };
        }
    }
}

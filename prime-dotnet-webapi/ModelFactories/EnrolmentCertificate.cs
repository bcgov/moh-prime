using FactoryGirlCore;
using system;
using System.Linq;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.ModelFactories
{
    [NotMapped]
    public sealed class EnrolmentCertificate
    {
        public string FirstName ,
        public string MiddleName ,
        public string LastName ,
        public string PreferredFirstName ,
        public string PreferredMiddleName ,
        public string PreferredLastName ,
        public DateTime DateOfBirth ,
        public string LicensePlate ,
        public IEnumerable<Privilege> Privileges ,
        public IEnumerable<OrganizationType> OrganizationTypes ,

        public EnrolmentCertificateNote EnrolmentCertificateNote ,

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
                Privileges = enrollee.AssignedPrivileges.Select(ap => ap.Privilege),
                OrganizationTypes = enrollee.Organizations.Select(org => org.OrganizationType),
                EnrolmentCertificateNote = enrollee.EnrolmentCertificateNote
            };
        }
    }
}

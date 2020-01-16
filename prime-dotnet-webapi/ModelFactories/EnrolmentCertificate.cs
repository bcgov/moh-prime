using FactoryGirlCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Prime.ModelFactories

     sealed class EnrolmentCertificate
    {
          FirstName
          MiddleName
          LastName
          PreferredFirstName
          PreferredMiddleName
          PreferredLastName
         DateTime DateOfBirth
          LicensePlate
         IEnumerable<Privilege> Privileges
         IEnumerable<OrganizationType> OrganizationTypes
         EnrolmentCertificateNote EnrolmentCertificateNote
         static EnrolmentCertificate Create(Enrollee enrollee)
        {
            return new EnrolmentCertificate
            {
                FirstName = enrollee.FirstName
                MiddleName = enrollee.MiddleName
                LastName = enrollee.LastName
                PreferredFirstName = enrollee.PreferredFirstName
                PreferredMiddleName = enrollee.PreferredMiddleName
                PreferredLastName = enrollee.PreferredLastName
                DateOfBirth = enrollee.DateOfBirth
                LicensePlate = enrollee.LicensePlate
                Privileges = enrollee.AssignedPrivileges.Select(ap => ap.Privilege)
                OrganizationTypes = enrollee.Organizations.Select(org => org.OrganizationType)
                EnrolmentCertificateNote = enrollee.EnrolmentCertificateNote
            };
        }
    }

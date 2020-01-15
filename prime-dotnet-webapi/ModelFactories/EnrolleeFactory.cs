using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using FactoryGirlCore;

using Prime.Models;

namespace Prime.ModelFactories
{
    public enum ProgressStatusType
    {
        STARTED,
        SUBMITTED,
        FINISHED
    }

    public class EnrolleeFactory : IDefinable
    {
        public void Define()
        {
            FactoryGirl.Define<Enrollee>(() => new Enrollee
            {
                Id = 0,
                UserId = Guid.NewGuid(),
                //string LicensePlate ,
                FirstName = "First",
                //string MiddleName ,
                LastName = "Last",
                // string PreferredFirstName ,
                // string PreferredMiddleName ,
                // string PreferredLastName ,
                // DateTime DateOfBirth ,
                // PhysicalAddress PhysicalAddress ,
                // MailingAddress MailingAddress ,
                // string ContactEmail ,
                // string ContactPhone ,
                // string VoicePhone ,
                // string VoiceExtension ,
                // ICollection<Certification> Certifications ,
                // ICollection<Job> Jobs ,
                // ICollection<Organization> Organizations ,
                // string DeviceProviderNumber ,
                // bool? IsInsulinPumpProvider ,
                // bool? HasConviction ,
                // string HasConvictionDetails ,
                // bool? HasRegistrationSuspended ,
                // string HasRegistrationSuspendedDetails ,
                // bool? HasDisciplinaryAction ,
                // string HasDisciplinaryActionDetails ,
                // bool? HasPharmaNetSuspended ,
                // string HasPharmaNetSuspendedDetails ,
                // ICollection<AssignedPrivilege> AssignedPrivileges ,
                // ICollection<Privilege> Privileges ,
                // ICollection<EnrolmentStatus> EnrolmentStatuses ,
                // bool ProfileCompleted ,
                // ICollection<Status> AvailableStatuses ,
                // ICollection<AdjudicatorNote> AdjudicatorNotes ,
                // AccessAgreementNote AccessAgreementNote ,
                // EnrolmentCertificateNote EnrolmentCertificateNote ,
                // ICollection<TermsOfAccess> TermsOfAccess ,
            });
        }
    }
}

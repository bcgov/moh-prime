using System;
using System.Collections.Generic;

using Bogus;
using Bogus.Extensions;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class EnrolleeFactory : Faker<Enrollee>
    {
        private static int IdCounter = 1;

        public EnrolleeFactory()
        {
            this.SetBaseRules();

            RuleFor(e => e.Id, () => IdCounter++);
            RuleFor(e => e.UserId, () => Guid.NewGuid());
            RuleFor(e => e.FirstName, f => f.Name.FirstName());
            RuleFor(e => e.MiddleName, f => f.Name.FirstName());
            RuleFor(e => e.LastName, f => f.Name.LastName());
            RuleFor(e => e.DateOfBirth, f => f.Date.Past(20, DateTime.Now.AddYears(-19)));
            //RuleFor(e => e.PhysicalAddress, f => PhysicalAddressFaker.Generate())
            //RuleFor(e => e.MailingAddress, f => MailingAddressFaker.Generate())
            //RuleFor(e => e.Certifications, f => CertificationFaker.Generate(2))
            //RuleFor(e => e.IsInsulinPumpProvider, f => f.Random.Bool())
            //RuleFor(e => e.Jobs, f => JobFaker.Generate(2))
            RuleFor(e => e.HasConviction, f => f.Random.Bool());
            RuleFor(e => e.HasConvictionDetails, f => f.Lorem.Paragraphs(2));
            RuleFor(e => e.HasRegistrationSuspended, f => f.Random.Bool());
            RuleFor(e => e.HasRegistrationSuspendedDetails, f => f.Lorem.Paragraphs(2));
            RuleFor(e => e.HasDisciplinaryAction, f => f.Random.Bool());
            RuleFor(e => e.HasDisciplinaryActionDetails, f => f.Lorem.Paragraphs(2));
            RuleFor(e => e.HasPharmaNetSuspended, f => f.Random.Bool());
            RuleFor(e => e.HasPharmaNetSuspendedDetails, f => f.Lorem.Paragraphs(2));
            RuleFor(e => e.AccessAgreementNote, (f, e) => new AccessAgreementNoteFactory(e).Generate());
        }
    }
}

// Id = 0,
// UserId = Guid.NewGuid(),
// LicensePlate ,
// FirstName = "First",
// MiddleName ,
// LastName = "Last",
// PreferredFirstName ,
// PreferredMiddleName ,
// PreferredLastName ,
// DateOfBirth ,
// PhysicalAddress PhysicalAddress ,
// MailingAddress MailingAddress ,
// ContactEmail ,
// ContactPhone ,
// VoicePhone ,
// VoiceExtension ,
// Certifications ,
// Jobs ,
// Organizations ,
// DeviceProviderNumber ,
// IsInsulinPumpProvider ,
// HasConviction ,
// HasConvictionDetails ,
// HasRegistrationSuspended ,
// HasRegistrationSuspendedDetails ,
// HasDisciplinaryAction ,
// HasDisciplinaryActionDetails ,
// HasPharmaNetSuspended ,
// HasPharmaNetSuspendedDetails ,
// AssignedPrivileges ,
// Privileges ,
// EnrolmentStatuses ,
// ProfileCompleted ,
// AvailableStatuses ,
// AdjudicatorNotes ,
// AccessAgreementNote ,
// EnrolmentCertificateNote ,
// TermsOfAccess ,

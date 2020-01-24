using System;
using System.Linq;
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

            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.UserId, f => Guid.NewGuid());
            RuleFor(x => x.FirstName, f => f.Name.FirstName());
            RuleFor(x => x.MiddleName, f => f.Name.FirstName());
            RuleFor(x => x.LastName, f => f.Name.LastName());
            RuleFor(x => x.PreferredFirstName, f => f.Name.FirstName().OrNull(f));
            RuleFor(x => x.PreferredMiddleName, (f, x) => x.PreferredFirstName == null ? null : f.Name.FirstName());
            RuleFor(x => x.PreferredLastName, (f, x) => x.PreferredFirstName == null ? null : f.Name.LastName());
            RuleFor(x => x.DateOfBirth, f => f.Date.Past(50, DateTime.Now.AddYears(-19)));
            RuleFor(x => x.ContactEmail, f => f.Internet.Email());
            RuleFor(x => x.VoicePhone, f => f.Phone.PhoneNumber());
            RuleFor(x => x.VoiceExtension, f => f.Random.Replace("###").OrNull(f));
            RuleFor(x => x.ContactPhone, f => f.Phone.PhoneNumber().OrNull(f));
            RuleFor(x => x.DeviceProviderNumber, f => null);
            RuleFor(x => x.IsInsulinPumpProvider, f => null);
            RuleFor(x => x.LicensePlate, f => null);
            RuleFor(x => x.ProfileCompleted, f => false);

            RuleFor(x => x.HasConviction, false);
            RuleFor(x => x.HasConvictionDetails, f => null);
            RuleFor(x => x.HasRegistrationSuspended, false);
            RuleFor(x => x.HasRegistrationSuspendedDetails, f => null);
            RuleFor(x => x.HasDisciplinaryAction, false);
            RuleFor(x => x.HasDisciplinaryActionDetails, f => null);
            RuleFor(x => x.HasPharmaNetSuspended, false);
            RuleFor(x => x.HasPharmaNetSuspendedDetails, f => null);

            RuleFor(x => x.EnrolmentStatuses, (f, x) => new EnrolmentStatusFactory(x).Generate(1, "default,inProgress"));
            RuleFor(x => x.PhysicalAddress, (f, x) => new PhysicalAddressFactory(x).Generate());
            RuleFor(x => x.MailingAddress, (f, x) => new MailingAddressFactory(x).Generate().OrNull(f));
            RuleFor(x => x.Certifications, (f, x) => new CertificationFactory(x).GenerateBetween(1, 2).OrNull(f, .75f));
            RuleFor(x => x.Jobs, (f, x) => x.Certifications == null ? new JobFactory(x).Generate(1) : null);
            RuleFor(x => x.Organizations, (f, x) => new OrganizationFactory(x).Generate(1));
            RuleFor(x => x.AccessAgreementNote, (f, x) => new AccessAgreementNoteFactory(x).Generate().OrNull(f));
            RuleFor(x => x.EnrolmentCertificateNote, (f, x) => new EnrolmentCertificateNoteFactory(x).Generate().OrNull(f));
            RuleFor(x => x.AdjudicatorNotes, (f, x) => new AdjudicatorNoteFactory(x).GenerateBetween(1, 4).OrNull(f));
            RuleFor(x => x.AssignedPrivileges, f => null);
            RuleFor(x => x.Privileges, f => null);
            RuleFor(x => x.EnrolleeProfileVersions, f => null);
            Ignore(x => x.AccessTerms); // Awaiting finialization of access terms

            RuleSet("status.submitted", (set) =>
            {
                set.RuleFor(x => x.EnrolmentStatuses, (f, x) => new StatusStateFactory(x, StatusState.Submitted).Generate());
            });
            RuleSet("status.approved", (set) =>
            {
                set.RuleFor(x => x.EnrolmentStatuses, (f, x) => new StatusStateFactory(x, StatusState.Approved).Generate());
            });
            RuleSet("status.acceptedTos", (set) =>
            {
                set.RuleFor(x => x.EnrolmentStatuses, (f, x) => new StatusStateFactory(x, StatusState.AcceptedTos).Generate());
            });
            RuleSet("status.random", (set) =>
            {
                set.RuleFor(x => x.EnrolmentStatuses, (f, x) => new StatusStateFactory(x, f).Generate());
            });

            RuleSet("deviceProvider", (set) =>
            {
                set.RuleFor(x => x.DeviceProviderNumber, f => f.Random.Replace("#####"));
                set.RuleFor(x => x.IsInsulinPumpProvider, f => f.Random.Bool());
            });
            RuleSet("selfDeclaration", (set) =>
            {
                set.RuleFor(x => x.HasConviction, f => f.Random.Bool());
                set.RuleFor(x => x.HasRegistrationSuspended, f => f.Random.Bool());
                set.RuleFor(x => x.HasDisciplinaryAction, f => f.Random.Bool());
                set.RuleFor(x => x.HasPharmaNetSuspended, f => f.Random.Bool());
                set.RuleFor(x => x.HasConvictionDetails, (f, x) => x.HasConviction == true ? f.Lorem.Paragraphs(2) : null);
                set.RuleFor(x => x.HasRegistrationSuspendedDetails, (f, x) => x.HasRegistrationSuspended == true ? f.Lorem.Paragraphs(2) : null);
                set.RuleFor(x => x.HasDisciplinaryActionDetails, (f, x) => x.HasDisciplinaryAction == true ? f.Lorem.Paragraphs(2) : null);
                set.RuleFor(x => x.HasPharmaNetSuspendedDetails, (f, x) => x.HasPharmaNetSuspended == true ? f.Lorem.Paragraphs(2) : null);
            });

            FinishWith((f, x) =>
            {
                x.ProfileCompleted = x.EnrolmentStatuses.Count > 1 ? true : f.Random.Bool();

                if (x.ProgressStatus == ProgressStatusType.FINISHED)
                {
                    x.LicensePlate = f.Random.AlphaNumeric(20);

                    if (x.Certifications != null)
                    {
                        var licenceCodes = x.Certifications.Select(cert => cert.LicenseCode);
                        x.AssignedPrivileges = DefaultPrivilegeLookup.All
                            .Where(def => licenceCodes.Contains(def.LicenseCode))
                            .Select(def => def.PrivilegeId)
                            .Distinct()
                            .Select(privilegeId => new AssignedPrivilegeFactory(x, privilegeId).Generate())
                            .ToList();
                        x.Privileges = x.AssignedPrivileges.Select(p => p.Privilege).ToList();
                    }
                }
            });
        }
    }
}

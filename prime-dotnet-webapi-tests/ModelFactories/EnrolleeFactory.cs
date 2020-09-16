using System;
using System.Linq;
using System.Collections.Generic;

using Bogus;
using Bogus.Extensions;
using Prime.Models;

namespace PrimeTests.ModelFactories
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
            RuleFor(x => x.LastName, f => f.Name.LastName());
            RuleFor(x => x.GivenNames, (f, x) => $"{x.FirstName} {f.Name.FirstName()}");
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
            RuleFor(x => x.GPID, f => null);
            RuleFor(x => x.HPDID, f => null);
            RuleFor(x => x.ProfileCompleted, f => false);
            RuleFor(x => x.IdentityAssuranceLevel, 3);

            RuleFor(x => x.SelfDeclarations, (f, x) => new SelfDeclarationFactory(x).GenerateBetween(0, 1));
            RuleFor(x => x.SelfDeclarationDocuments, f => null);

            RuleFor(x => x.EnrolmentStatuses, (f, x) => new EnrolmentStatusFactory(x).Generate(1, "default,inProgress"));
            RuleFor(x => x.PhysicalAddress, f => new PhysicalAddressFactory().Generate());
            RuleFor(x => x.MailingAddress, f => new MailingAddressFactory().Generate().OrNull(f));
            RuleFor(x => x.Certifications, (f, x) => new CertificationFactory(x).GenerateBetween(1, 2).OrDefault(f, .75f, new List<Certification>()));
            RuleFor(x => x.Jobs, (f, x) => x.Certifications.Any() ? new List<Job>() : new JobFactory(x).Generate(1));
            RuleFor(x => x.EnrolleeCareSettings, (f, x) => new EnrolleeCareSettingFactory(x).Generate(1));
            RuleFor(x => x.AccessAgreementNote, f => null);
            RuleFor(x => x.AdjudicatorNotes, (f, x) => new AdjudicatorNoteFactory(x).GenerateBetween(1, 4).OrNull(f));
            RuleFor(x => x.AssignedPrivileges, f => null);
            RuleFor(x => x.EnrolleeProfileVersions, f => null);
            RuleFor(x => x.isAdminView, f => true);
            RuleFor(x => x.RequestingRemoteAccess, f => false);
            // TODO: fix these ignores
            Ignore(x => x.AccessTerms);
            Ignore(x => x.Adjudicator);
            Ignore(x => x.AdjudicatorId);
            Ignore(x => x.AlwaysManual);
            Ignore(x => x.IdentityProvider);
            Ignore(x => x.Credential);
            Ignore(x => x.CredentialId);

            RuleSet("status.submitted", (set) =>
            {
                set.RuleFor(x => x.EnrolmentStatuses, (f, x) => new StatusStateFactory(x, StatusState.Submitted).Generate());
            });
            RuleSet("status.editable", (set) =>
            {
                set.RuleFor(x => x.EnrolmentStatuses, (f, x) => new StatusStateFactory(x, StatusState.PassedTos).Generate());
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

            FinishWith((f, x) =>
            {
                x.ProfileCompleted = x.EnrolmentStatuses.Count > 1 ? true : f.Random.Bool();

                if (x.CurrentStatus.IsType(StatusType.Editable) && x.PreviousStatus?.IsType(StatusType.RequiresToa) == true)
                {
                    x.GPID = f.Random.AlphaNumeric(20);

                    if (x.Certifications != null)
                    {
                        var licenceCodes = x.Certifications.Select(cert => cert.LicenseCode);
                        x.AssignedPrivileges = DefaultPrivilegeLookup.All
                            .Where(def => licenceCodes.Contains(def.LicenseCode))
                            .Select(def => def.PrivilegeId)
                            .Distinct()
                            .Select(privilegeId => new AssignedPrivilegeFactory(x, privilegeId).Generate())
                            .ToList();
                    }
                }
            });
        }
    }
}

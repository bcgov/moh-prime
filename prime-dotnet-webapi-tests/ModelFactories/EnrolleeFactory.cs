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
            RuleFor(x => x.Email, f => f.Internet.Email());
            RuleFor(x => x.Phone, f => f.Phone.PhoneNumber());
            RuleFor(x => x.PhoneExtension, f => f.Random.Replace("###").OrNull(f));
            RuleFor(x => x.SmsPhone, f => f.Phone.PhoneNumber().OrNull(f));
            RuleFor(x => x.DeviceProviderIdentifier, f => null);
            RuleFor(x => x.GPID, f => null);
            RuleFor(x => x.HPDID, f => null);
            RuleFor(x => x.ProfileCompleted, f => false);
            RuleFor(x => x.IdentityAssuranceLevel, 3);

            RuleFor(x => x.SelfDeclarations, (f, x) => new SelfDeclarationFactory(x).GenerateBetween(0, 1));
            RuleFor(x => x.SelfDeclarationDocuments, f => null);
            RuleFor(x => x.IdentificationDocuments, f => null);

            RuleFor(x => x.Addresses, (f, x) => new EnrolleeAddressFactory(x).Generate(1));
            RuleFor(x => x.EnrolmentStatuses, (f, x) => new EnrolmentStatusFactory(x).Generate(1, "default,inProgress"));
            RuleFor(x => x.Certifications, (f, x) => new CertificationFactory(x).GenerateBetween(1, 2));
            RuleFor(x => x.EnrolleeCareSettings, (f, x) => new EnrolleeCareSettingFactory(x).Generate(1));
            RuleFor(x => x.EnrolleeHealthAuthorities, (f, x) => new EnrolleeHealthAuthorityFactory(x).Generate(1));
            RuleFor(x => x.AccessAgreementNote, f => null);
            RuleFor(x => x.AdjudicatorNotes, (f, x) => new EnrolleeNoteFactory(x).GenerateBetween(1, 4).OrNull(f));
            RuleFor(x => x.AssignedPrivileges, f => null);
            RuleFor(x => x.OboSites, f => new List<OboSite>());
            RuleFor(x => x.EnrolleeAbsences, (f, x) => new EnrolleeAbsenceFactory(x).GenerateBetween(0, 1));
            RuleFor(x => x.RemoteAccessSites, f => new List<RemoteAccessSite>());
            // TODO: create rule sets for these ignores?
            Ignore(x => x.Agreements);
            Ignore(x => x.Adjudicator);
            Ignore(x => x.AdjudicatorId);
            Ignore(x => x.AlwaysManual);
            Ignore(x => x.IdentityProvider);
            Ignore(x => x.EnrolleeRemoteUsers);
            Ignore(x => x.RemoteAccessLocations);
            Ignore(x => x.EnrolleeAdjudicationDocuments);
            Ignore(x => x.Submissions);

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
                set.RuleFor(x => x.DeviceProviderIdentifier, f => f.Random.Replace("#####"));
            });

            RuleSet("obo", (set) =>
            {
                set.RuleFor(x => x.Certifications, f => new List<Certification>());
                set.RuleFor(x => x.OboSites, (f, x) => new OboSiteFactory(x).GenerateBetween(1, 4));
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

                // An enrollee with certifications shouldn't have OboSites
                if (x.Certifications != null && x.Certifications.Count > 0)
                {
                    x.OboSites = new List<OboSite>();
                }
            });
        }
    }
}

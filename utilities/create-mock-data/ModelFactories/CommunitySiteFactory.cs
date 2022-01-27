using System;
using System.Collections.Generic;
using Bogus;
using Bogus.Extensions;

using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class CommunitySiteFactory : Faker<CommunitySite>
    {
        private static int IdCounter = 1;

        public CommunitySiteFactory(Organization org = null)
        {
//            this.SetBaseRules();

//            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.PhysicalAddress, f => new PhysicalAddressFactory().Generate());
            RuleFor(x => x.Organization, f => org != null ? org : new OrganizationFactory(new PartyFactory().Generate()).Generate());
            RuleFor(x => x.OrganizationId, (f, x) => x.Organization.Id);

            RuleFor(x => x.AdministratorPharmaNet, f => new ContactFactory().Generate());
            RuleFor(x => x.PrivacyOfficer, f => new ContactFactory().Generate());
            RuleFor(x => x.TechnicalSupport, f => new ContactFactory().Generate());
            RuleFor(x => x.CareSettingCode, f => f.PickRandom(CareSettingLookup.SiteCareSettings).Code);
            // TODO: Signing Authority now must provide?
            RuleFor(x => x.PEC, f => f.Lorem.Letter(3).OrNull(f));
            RuleFor(x => x.DoingBusinessAs, f => f.Company.CompanyName());

            RuleFor(x => x.ActiveBeforeRegistration, f => f.Random.Bool());
            // TODO: What is this property about?
            RuleFor(x => x.Completed, f => f.Random.Bool());
            RuleFor(x => x.Flagged, f => f.Random.Bool(0.1f));
            // TODO: What dates are generated?
            RuleFor(x => x.SubmittedDate, f => f.Date.Past(1).OrNull(f, 0.2f));
            RuleFor(x => x.SiteVendors, (f, x) => new SiteVendorFactory(x).GenerateBetween(1, 3));
            // TODO: is BusinessLicenceFactory complete?
            RuleFor(x => x.BusinessLicences, (f, x) => new BusinessLicenceFactory(x).GenerateBetween(1, 3));

            Ignore(x => x.AdministratorPharmaNetId);
            Ignore(x => x.PrivacyOfficerId);
            Ignore(x => x.TechnicalSupportId);
            Ignore(x => x.ProvisionerId);
            Ignore(x => x.CareSetting);
            Ignore(x => x.AdjudicatorId);
            // The following are not used in the application?
            Ignore(x => x.SiteRegistrationReviewDocuments);
            Ignore(x => x.SiteAdjudicationDocuments);
            Ignore(x => x.SiteRegistrationNotes);
            // TODO:
            Ignore(x => x.Provisioner);
            Ignore(x => x.RemoteUsers);
            Ignore(x => x.Adjudicator);
            Ignore(x => x.BusinessHours);

            // The following are populated in FinishWith section
            Ignore(x => x.SiteStatuses);
            Ignore(x => x.ApprovedDate);

            FinishWith((f, x) =>
            {
                x.SiteStatuses = new List<SiteStatus>();
                if (x.SubmittedDate != null)
                {
                    var submittedDate = x.SubmittedDate ?? default(DateTimeOffset);
                    x.SiteStatuses.Add(
                        new SiteStatus
                        {
                            Site = x,
                            StatusDate = submittedDate,
                            StatusType = SiteStatusType.InReview
                        });
                    // A Submitted site may also be Approved, a few days after submission
                    if (f.Random.Bool(0.6f))
                    {
                        x.ApprovedDate = submittedDate.AddDays(f.Random.Int(1, 5));
                        x.SiteStatuses.Add(
                        new SiteStatus
                        {
                            Site = x,
                            StatusDate = x.ApprovedDate ?? default(DateTimeOffset),
                            StatusType = SiteStatusType.Editable
                        });
                    }
                }
            });
        }
    }
}

using System;
using Bogus;
using Bogus.Extensions;
using Prime.Contracts;
using Prime.ViewModels;

namespace PrimeTests.ModelFactories
{
    public class SendSiteEmailModelFactory : Faker<SendSiteEmailModel>
    {
        private static int IdCounter = 1;

        public SendSiteEmailModelFactory()
        {
            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.CareSettingCode, f => f.PickRandom(CareSettingLookup.SiteCareSettings).Code);
            RuleFor(x => x.AdjudicatorEmail, f => f.Internet.Email());
            RuleFor(x => x.ProvisionerEmail, f => f.Internet.Email());
            RuleFor(x => x.AdministratorPharmaNetEmail, f => f.Internet.Email());
            RuleFor(x => x.OrganizationSigningAuthorityEmail, f => f.Internet.Email());
            RuleFor(x => x.BusinessLicenceId, f => f.IndexFaker);
            RuleFor(x => x.OrganizationName, f => f.Company.CompanyName());
            RuleFor(x => x.PEC, f => f.Lorem.Letter(3).OrNull(f));
            RuleFor(x => x.DoingBusinessAs, f => f.Company.CompanyName());
            RuleFor(x => x.PhysicalAddressCity, f => f.Address.City());
            RuleFor(x => x.PhysicalAddressStreet, f => f.Address.StreetAddress());
            RuleFor(x => x.Note, f => f.Lorem.Paragraph(1));
            RuleFor(x => x.EmailType, f => f.PickRandom(Enum.GetValues<SiteEmailType>()));
            RuleFor(x => x.RemoteUserNames, f => f.Make(f.Random.Number(0, 3), () => f.Name.FullName()));
            RuleFor(x => x.RemoteUserEmails, f => f.Make(f.Random.Number(0, 3), () => f.Person.Email));
        }
    }
}

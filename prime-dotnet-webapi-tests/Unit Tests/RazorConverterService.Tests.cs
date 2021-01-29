using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using Prime.Services.Razor;
using Prime.ViewModels.Emails;
using Prime.ViewModels.Agreements;
using Prime.Models.Documents;
using Prime.ViewModels.SiteRegistration;

namespace PrimeTests.UnitTests
{
    public class RazorConverterServiceTests
    {
        public static RazorConverterService CreateService()
        {
            var provider = new WebApplicationFactory<Startup>().Services.CreateScope().ServiceProvider;

            return new RazorConverterService(
                provider.GetService<IRazorViewEngine>(),
                A.Fake<ITempDataProvider>(),
                provider,
                A.Fake<IHttpContextAccessor>()
            );
        }

        [Theory]
        [MemberData(nameof(AgreementTemplates))]
        public async void TestRender_Agreement(RazorTemplate<Agreement> template)
        {
            var service = CreateService();
            var agreementText = "AGREEMENT TEXT";
            var model = new Agreement
            {
                AgreementVersion = new AgreementVersion
                {
                    Text = agreementText
                }
            };

            var html = await service.RenderTemplateToStringAsync(template, model);

            Assert.NotNull(html);
            Assert.Contains(agreementText, html);
        }

        [Theory]
        [MemberData(nameof(AgreementTemplates))]
        public async void TestRender_Agreement_WithLimits(RazorTemplate<Agreement> template)
        {
            var service = CreateService();
            var agreementText = "AGREEMENT TEXT";
            var limitsText = "ThIs iS a LiMIt";
            var model = new Agreement
            {
                AgreementVersion = new AgreementVersion
                {
                    Text = "AGREEMENT TEXT"
                },
                LimitsConditionsClause = new LimitsConditionsClause
                {
                    Text = limitsText
                }
            };

            var html = await service.RenderTemplateToStringAsync(template, model);

            Assert.NotNull(html);
            Assert.Contains(agreementText, html);
            Assert.Contains(limitsText, html);
        }

        [Theory]
        [MemberData(nameof(OrgAgreementTemplates))]
        public async void TestRender_OrgAgreement(RazorTemplate<OrgAgreementRazorViewModel> template)
        {
            var service = CreateService();
            var model = new OrgAgreementRazorViewModel("My Cool Site", DateTimeOffset.Now);

            var html = await service.RenderTemplateToStringAsync(template, model);

            Assert.NotNull(html);
            Assert.Contains(model.OrganizationName, html);
            Assert.Contains(model.AcceptedDate.Day.ToString(), html);
        }

        [Theory]
        [MemberData(nameof(LinkedEmailTemplates))]
        public async void TestRender_LinkedEmails(RazorTemplate<LinkedEmailViewModel> template)
        {
            var service = CreateService();
            var model = new LinkedEmailViewModel("www.TEST.com");

            var html = await service.RenderTemplateToStringAsync(template, model);

            Assert.NotNull(html);
            Assert.Contains(model.Url, html);
        }

        [Theory]
        [MemberData(nameof(ProvisionerAccessEmailTemplates))]
        public async void TestRender_ProvisionerAccessEmails(RazorTemplate<ProvisionerAccessEmailViewModel> template)
        {
            var service = CreateService();
            var model = new ProvisionerAccessEmailViewModel
            {
                EnrolleeFullName = "NAme",
                TokenUrl = "www.TEST.com",
                ExpiresInDays = 3
            };

            var html = await service.RenderTemplateToStringAsync(template, model);

            Assert.NotNull(html);
            Assert.Contains(model.EnrolleeFullName, html);
            Assert.Contains(model.TokenUrl, html);
            Assert.Contains(model.ExpiresInDays.ToString(), html);
        }

        [Theory]
        [MemberData(nameof(EnrolleeRenewalEmailTemplates))]
        public async void TestRender_EnrolleeRenewalEmails(RazorTemplate<EnrolleeRenewalEmailViewModel> template)
        {
            var service = CreateService();
            var model = new EnrolleeRenewalEmailViewModel("first", "last", DateTimeOffset.Now);

            var html = await service.RenderTemplateToStringAsync(template, model);

            Assert.NotNull(html);
            Assert.Contains(model.EnrolleeName, html);
            // Not all emails contain the renewal date or URL despite sharing a view mmodel.
        }

        [Theory]
        [MemberData(nameof(SiteApprovalEmailTemplates))]
        public async void TestRender_SiteApprovalEmails(RazorTemplate<SiteApprovalEmailViewModel> template)
        {
            var service = CreateService();
            var model = new SiteApprovalEmailViewModel
            {
                DoingBusinessAs = "dba",
                Pec = "pec"
            };

            var html = await service.RenderTemplateToStringAsync(template, model);

            Assert.NotNull(html);
            Assert.Contains(model.DoingBusinessAs, html);
            // Not all emails contain the PEC despite sharing a View Model.
        }

        [Fact]
        public async void TestRender_RemoteUserNotificationEmail()
        {
            var service = CreateService();
            var model = new RemoteUserNotificationEmailViewModel
            {
                OrganizationName = "NAme",
                SiteStreetAddress = "2134 first street",
                SiteCity = "Acity",
                PrimeUrl = "www.TEST.com"
            };

            var html = await service.RenderTemplateToStringAsync(RazorTemplates.Emails.RemoteUserNotification, model);

            Assert.NotNull(html);
            Assert.Contains(model.OrganizationName, html);
            Assert.Contains(model.SiteStreetAddress, html);
            Assert.Contains(model.SiteCity, html);
            Assert.Contains(model.PrimeUrl, html);
        }

        [Fact]
        public async void TestRender_RemoteUsersUpdatedEmail()
        {
            var service = CreateService();
            var model = new RemoteUsersUpdatedEmailViewModel
            {
                SiteStreetAddress = "123 street",
                OrganizationName = "NAME",
                SitePec = "PecC",
                RemoteUserNames = new[] { "Alice", "bob bobward" },
                DocumentUrl = "A.URL.com"
            };

            var html = await service.RenderTemplateToStringAsync(RazorTemplates.Emails.RemoteUsersUpdated, model);

            Assert.NotNull(html);
            Assert.Contains(model.SiteStreetAddress, html);
            Assert.Contains(model.OrganizationName, html);
            Assert.Contains(model.SitePec, html);
            Assert.Contains(model.DocumentUrl, html);
            foreach (var name in model.RemoteUserNames)
            {
                Assert.Contains(name, html);
            }
        }

        [Fact]
        public async void TestRender_SiteSummary()
        {
            var service = CreateService();
            var model = new SiteRegistrationReviewViewModel
            {
                SiteAddress = new PhysicalAddress(),
                BusinessHours = new BusinessHourViewModel[] { },
                Vendors = new VendorViewModel[] { },
                RemoteUsers = new RemoteUserViewModel[]
                {
                    new RemoteUserViewModel
                    {
                        Certifications = new CertViewModel[] {}
                    }
                },
                SigningAuthority = new ContactViewModel(),
                AdministratorOfPharmaNet = new ContactViewModel(),
                PrivacyOfficer = new ContactViewModel(),
                TechnicalSupport = new ContactViewModel()
            };

            var html = await service.RenderTemplateToStringAsync(RazorTemplates.SiteRegistrationReview, model);

            Assert.NotNull(html);
        }

        [Theory]
        [MemberData(nameof(DocumentTemplates))]
        public async void TestRender_Documents(RazorTemplate<File> template)
        {
            var service = CreateService();
            var model = new File("filename.ext", new byte[] { });

            var html = await service.RenderTemplateToStringAsync(template, model);

            Assert.NotNull(html);
        }

        public static IEnumerable<object[]> AgreementTemplates()
        {
            yield return new[] { RazorTemplates.Agreements.Base };
            yield return new[] { RazorTemplates.Agreements.Pdf };
        }

        public static IEnumerable<object[]> OrgAgreementTemplates()
        {
            yield return new[] { RazorTemplates.OrgAgreements.CommunityPharmacy };
            yield return new[] { RazorTemplates.OrgAgreements.CommunityPharmacyPdf };
            yield return new[] { RazorTemplates.OrgAgreements.CommunityPractice };
            yield return new[] { RazorTemplates.OrgAgreements.CommunityPracticePdf };
        }

        public static IEnumerable<object[]> LinkedEmailTemplates()
        {
            yield return new[] { RazorTemplates.Emails.BusinessLicenceUploaded };
            yield return new[] { RazorTemplates.Emails.Reminder };
            yield return new[] { RazorTemplates.Emails.SiteRegistrationSubmission };
        }

        public static IEnumerable<object[]> ProvisionerAccessEmailTemplates()
        {
            yield return new[] { RazorTemplates.Emails.CommunityPharmacyManager };
            yield return new[] { RazorTemplates.Emails.CommunityPractice };
            yield return new[] { RazorTemplates.Emails.HealthAuthority };
        }
        public static IEnumerable<object[]> EnrolleeRenewalEmailTemplates()
        {
            yield return new[] { RazorTemplates.Emails.RenewalPassed };
            yield return new[] { RazorTemplates.Emails.RenewalRequired };
        }

        public static IEnumerable<object[]> SiteApprovalEmailTemplates()
        {
            yield return new[] { RazorTemplates.Emails.SiteApprovedHibcEmailTemplate };
            yield return new[] { RazorTemplates.Emails.SiteApprovedPharmaNetAdministratorEmailTemplate };
            yield return new[] { RazorTemplates.Emails.SiteApprovedSigningAuthorityEmailTemplate };
        }

        public static IEnumerable<object[]> DocumentTemplates()
        {
            yield return new[] { RazorTemplates.Document };
            yield return new[] { RazorTemplates.ApologyDocument };
        }
    }
}

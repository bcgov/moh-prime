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
using Prime.HttpClients.Mail;

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
        public async void TestRender_OrgAgreement(RazorTemplate<Tuple<string, DateTimeOffset>> template)
        {
            var service = CreateService();
            var siteName = "My Cool Site";
            var date = DateTimeOffset.Now;
            var model = new Tuple<string, DateTimeOffset>(siteName, date);

            var html = await service.RenderTemplateToStringAsync(template, model);

            Assert.NotNull(html);
            Assert.Contains(siteName, html);
            Assert.Contains(date.Day.ToString(), html);
        }

        // TODO Emails are about to be refactored, write better tests at that point?
        [Theory]
        [MemberData(nameof(EmailTemplates))]
        public async void TestRender_Emails(RazorTemplate<EmailParams> template)
        {
            var service = CreateService();
            var model = new EmailParams
            {
                FirstName = "",
                LastName = "",
                TokenUrl = "",
                ProvisionerName = "",
                RenewalDate = DateTimeOffset.Now,
                Site = new Site
                {
                    Organization = new Organization(),
                    PhysicalAddress = new PhysicalAddress(),
                    RemoteUsers = new RemoteUser[] { }
                },
                DocumentUrl = "",
            };

            var html = await service.RenderTemplateToStringAsync(template, model);

            Assert.NotNull(html);
        }

        [Fact]
        public async void TestRender_SiteSummary()
        {
            var service = CreateService();
            var model = new Site
            {
                Organization = new Organization(),
                PhysicalAddress = new PhysicalAddress(),
                BusinessHours = new BusinessDay[] { },
                SiteVendors = new SiteVendor[] { },
                RemoteUsers = new RemoteUser[] { },
                Provisioner = new Party(),
                AdministratorPharmaNet = new Contact(),
                PrivacyOfficer = new Contact(),
                TechnicalSupport = new Contact(),
            };

            var html = await service.RenderTemplateToStringAsync(RazorTemplates.SiteRegistrationReview, model);

            Assert.NotNull(html);
        }

        [Theory]
        [MemberData(nameof(DocumentTemplates))]
        public async void TestRender_Documents(RazorTemplate<Document> template)
        {
            var service = CreateService();
            var model = new Document("filename.ext", new byte[] { }, "");

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

        public static IEnumerable<object[]> EmailTemplates()
        {
            yield return new[] { RazorTemplates.Emails.BusinessLicenceUploaded };
            yield return new[] { RazorTemplates.Emails.CommunityPharmacyManager };
            yield return new[] { RazorTemplates.Emails.CommunityPractice };
            yield return new[] { RazorTemplates.Emails.HealthAuthority };
            yield return new[] { RazorTemplates.Emails.Reminder };
            yield return new[] { RazorTemplates.Emails.RemoteUserNotification };
            yield return new[] { RazorTemplates.Emails.RenewalPassed };
            yield return new[] { RazorTemplates.Emails.RenewalRequired };
            yield return new[] { RazorTemplates.Emails.SiteApprovedHIBCEmailTemplate };
            yield return new[] { RazorTemplates.Emails.SiteApprovedPharmaNetAdministratorEmailTemplate };
            yield return new[] { RazorTemplates.Emails.SiteApprovedSigningAuthorityEmailTemplate };
            yield return new[] { RazorTemplates.Emails.SiteRegistrationSubmission };
            yield return new[] { RazorTemplates.Emails.UpdateRemoteUsers };
        }

        public static IEnumerable<object[]> DocumentTemplates()
        {
            yield return new[] { RazorTemplates.Document };
            yield return new[] { RazorTemplates.ApologyDocument };
        }
    }
}

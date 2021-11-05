using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

using Prime;
using Prime.Models;
using Prime.Models.Documents;
using Prime.Services;
using Prime.Services.Razor;
using Prime.ViewModels.Agreements;
using Prime.ViewModels.SiteRegistration.ReviewDocument;

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
                A.Fake<IEmailTemplateService>(),
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
            var model = new OrgAgreementRazorViewModel("My Cool Site", DateTimeOffset.Now, false);

            var html = await service.RenderTemplateToStringAsync(template, model);

            Assert.NotNull(html);
            Assert.Contains(model.OrganizationName, html);
            Assert.Contains(model.AcceptedDate.Day.ToString(), html);
        }

        [Fact]
        public async void TestRender_SiteSummary()
        {
            var service = CreateService();

            const string streetAddress = "221B Baker Street";
            const string cityAddress = "London";
            const DayOfWeek businessDay = DayOfWeek.Monday;
            const string vendorName = "Geeks R Us";
            const string certificationCollege = "University of Los Angeles";
            const string signingAuthorityName = "Fred Flintstone";
            const string pharmanetAdminName = "Barney Rubble";
            const string privacyOfficerName = "Wilma Flintstone";
            const string techSupportName = "Betty Rubble";

            var model = new SiteRegistrationReviewViewModel
            {
                SiteAddress = new PhysicalAddress()
                {
                    Street = streetAddress,
                    City = cityAddress
                },
                BusinessHours = new BusinessHourViewModel[]
                {
                    new BusinessHourViewModel
                    {
                        Day = businessDay
                    }
                },
                Vendors = new VendorViewModel[]
                {
                    new VendorViewModel
                    {
                        Name = vendorName
                    }
                },
                RemoteUsers = new RemoteUserViewModel[]
                {
                    new RemoteUserViewModel
                    {
                        Certifications = new CertViewModel[]
                        {
                            new CertViewModel
                            {
                                CollegeName = certificationCollege
                            }
                        }
                    }
                },
                SigningAuthority = new ContactViewModel()
                {
                    FullName = signingAuthorityName
                },
                AdministratorOfPharmaNet = new ContactViewModel()
                {
                    FullName = pharmanetAdminName
                },
                PrivacyOfficer = new ContactViewModel()
                {
                    FullName = privacyOfficerName
                },
                TechnicalSupport = new ContactViewModel()
                {
                    FullName = techSupportName
                }
            };

            var html = await service.RenderTemplateToStringAsync(RazorTemplates.SiteRegistrationReview, model);

            Assert.NotNull(html);
            Assert.Contains(streetAddress, html);
            Assert.Contains(cityAddress, html);
            Assert.Contains(businessDay.ToString(), html);
            Assert.Contains(vendorName, html);
            Assert.Contains(certificationCollege, html);
            Assert.Contains(signingAuthorityName, html);
            Assert.Contains(pharmanetAdminName, html);
            Assert.Contains(privacyOfficerName, html);
            Assert.Contains(techSupportName, html);
        }

        [Fact]
        public async void TestRender_Document()
        {
            var service = CreateService();
            var model = new File("filename.ext", new byte[] { });

            var html = await service.RenderTemplateToStringAsync(RazorTemplates.Document, model);

            Assert.NotNull(html);
        }

        [Fact]
        public async void TestRender_ApologyDocument()
        {
            var service = CreateService();
            var model = new File("filename.ext", new byte[] { });

            var html = await service.RenderTemplateToStringAsync(RazorTemplates.ApologyDocument, model);

            Assert.NotNull(html);
            Assert.Contains(model.Filename, html);
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
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using FakeItEasy;

using Prime.Models;
using Prime.Services;
using Prime.Services.EmailInternal;
using PrimeTests.Utils;
using Prime.HttpClients.Mail;
using Prime.ViewModels.Emails;
using PrimeTests.ModelFactories;

namespace PrimeTests.UnitTests
{
    public class EmailServiceTests : InMemoryDbTest
    {
        [Theory]
        [MemberData(nameof(RenewalScheduleTestCases))]
        public async void TestSendEnrolleeRenewalEmails(int daysUntilExpiry, ExpectedEmail expected)
        {
            // Arrange
            var requiredEmail = new Email("f@f", "t@t", "Required", "");
            var passedEmail = new Email("f@f", "t@t", "Passed", "");

            var smtpEmailClient = A.Fake<ISmtpEmailClient>();
            var emailRenderingService = A.Fake<IEmailRenderingService>();
            A.CallTo(() => emailRenderingService.RenderRenewalRequiredEmailAsync(A<string>._, A<EnrolleeRenewalEmailViewModel>._)).Returns(requiredEmail);
            A.CallTo(() => emailRenderingService.RenderRenewalPassedEmailAsync(A<string>._, A<EnrolleeRenewalEmailViewModel>._)).Returns(passedEmail);

            var service = MockDependenciesFor<EmailService>(emailRenderingService, smtpEmailClient);

            var enrollee = TestDb.HasAnEnrollee();
            enrollee.Agreements = new[]
            {
                new Agreement
                {
                    AcceptedDate = DateTimeOffset.Now,
                    ExpiryDate = DateTimeOffset.Now.AddDays(daysUntilExpiry)
                }
            };
            TestDb.SaveChanges();

            // Act
            await service.SendEnrolleeRenewalEmails();

            // Assert
            if (expected == ExpectedEmail.Passed)
            {
                A.CallTo(() => smtpEmailClient.SendAsync(passedEmail)).MustHaveHappened();
            }
            else if (expected == ExpectedEmail.Required)
            {
                A.CallTo(() => smtpEmailClient.SendAsync(requiredEmail)).MustHaveHappened();
            }
            else
            {
                A.CallTo(() => smtpEmailClient.SendAsync(A<Email>._)).MustNotHaveHappened();
            }
        }

        public enum ExpectedEmail
        {
            None,
            Required,
            Passed
        }
        public static IEnumerable<object[]> RenewalScheduleTestCases()
        {
            foreach (int day in Enumerable.Range(-2, 18))
            {
                if (day == -1)
                {
                    yield return new object[] { day, ExpectedEmail.Passed };
                }
                else if (new[] { 0, 1, 2, 3, 7, 14 }.Contains(day))
                {
                    yield return new object[] { day, ExpectedEmail.Required };
                }
                else
                {
                    yield return new object[] { day, ExpectedEmail.None };
                }
            }
        }

        [Fact]
        public async void TestSendRemoteUserNotificationsAsync_NoRemoteUsersDoesNotThrow()
        {
            var site = new CommunitySiteFactory().Generate();
            var service = MockDependenciesFor<EmailService>();

            var exceptions = await Record.ExceptionAsync(() => service.SendRemoteUserNotificationsAsync(site.Id));

            Assert.Null(exceptions);
        }
    }
}

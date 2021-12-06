using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FakeItEasy;
using MassTransit;
using MassTransit.Testing;
using Xunit;

using Prime.Consumer;
using Prime.Contracts;
using Prime.Services;
using PrimeTests.ModelFactories;

namespace PrimeTests.UnitTests.Consumer
{
    public class SendSiteEmailConsumerTests
    {
        private InMemoryTestHarness _harness;
        private IEmailService _emailServiceMock;
        private SendSiteEmailConsumer _consumer;

        public SendSiteEmailConsumerTests()
        {
            _harness = new InMemoryTestHarness();
            _emailServiceMock = A.Fake<IEmailService>();
            _consumer = A.Fake<SendSiteEmailConsumer>(x => x.WithArgumentsForConstructor(() => new SendSiteEmailConsumer(_emailServiceMock)));
        }

        [Fact]
        public async Task ShouldSendCommandToTheConsumer()
        {
            var consumerHarness = _harness.Consumer(() => _consumer);
            await _harness.Start();
            await _harness.InputQueueSendEndpoint.Send<SendSiteEmail>(new SendSiteEmailModelFactory().Generate());

            // endpoint should consume the message
            Assert.True(await _harness.Consumed.Any<SendSiteEmail>());

            // actual consumer should consume the message
            Assert.True(await consumerHarness.Consumed.Any<SendSiteEmail>());

            await _harness.Stop();
        }

        [Theory]
        [MemberData(nameof(SiteEmailTypes))]
        public async Task ConsumerShouldMakeCorrectEmailServiceCall(SiteEmailType emailType)
        {
            var sendSiteEmail = new SendSiteEmailModelFactory().Generate();
            sendSiteEmail.EmailType = emailType;

            // Arrange
            var context = A.Fake<ConsumeContext<SendSiteEmail>>();
            A.CallTo(() => context.Message).Returns(sendSiteEmail);

            // Act
            await _consumer.Consume(context);

            // Assert
            switch (emailType)
            {
                case SiteEmailType.SiteRegistrationSubmission:
                    A.CallTo(() => _emailServiceMock.SendSiteRegistrationSubmissionAsync(sendSiteEmail)).MustHaveHappened();
                    break;
                case SiteEmailType.BusinessLicenceUploaded:
                    A.CallTo(() => _emailServiceMock.SendBusinessLicenceUploadedAsync(sendSiteEmail)).MustHaveHappened();
                    break;
                case SiteEmailType.RemoteUserNotifications:
                    A.CallTo(() => _emailServiceMock.SendRemoteUserNotificationsAsync(sendSiteEmail)).MustHaveHappened();
                    break;
                case SiteEmailType.RemoteUsersUpdated:
                    A.CallTo(() => _emailServiceMock.SendRemoteUsersUpdatedAsync(sendSiteEmail)).MustHaveHappened();
                    break;
                case SiteEmailType.SiteActiveBeforeRegistration:
                    A.CallTo(() => _emailServiceMock.SendSiteActiveBeforeRegistrationAsync(sendSiteEmail)).MustHaveHappened();
                    break;
                case SiteEmailType.SiteApprovedHIBC:
                    A.CallTo(() => _emailServiceMock.SendSiteApprovedHIBCAsync(sendSiteEmail)).MustHaveHappened();
                    break;
                case SiteEmailType.SiteApprovedPharmaNetAdministrator:
                    A.CallTo(() => _emailServiceMock.SendSiteApprovedPharmaNetAdministratorAsync(sendSiteEmail)).MustHaveHappened();
                    break;
                case SiteEmailType.SiteApprovedSigningAuthority:
                    A.CallTo(() => _emailServiceMock.SendSiteApprovedSigningAuthorityAsync(sendSiteEmail)).MustHaveHappened();
                    break;
                case SiteEmailType.SiteReviewedNotification:
                    A.CallTo(() => _emailServiceMock.SendSiteReviewedNotificationAsync(sendSiteEmail)).MustHaveHappened();
                    break;
                default:
                    A.CallTo(_emailServiceMock).MustNotHaveHappened();
                    break;
            }
        }

        public static IEnumerable<object[]> SiteEmailTypes()
        {
            foreach (var emailType in Enum.GetValues<SiteEmailType>())
            {
                yield return new object[] { emailType };
            }
        }
    }
}

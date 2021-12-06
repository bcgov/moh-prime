using System.Threading.Tasks;

using Bogus;
using FakeItEasy;
using MassTransit;
using MassTransit.Testing;
using Xunit;

using Prime.Consumer;
using Prime.Contracts;
using Prime.Services;

namespace PrimeTests.UnitTests.Consumer
{
    public class SendOrgClaimApprovalNotificationEmailConsumerTests
    {
        private InMemoryTestHarness _harness;
        private IEmailService _emailServiceMock;
        private SendOrgClaimApprovalNotificationEmailConsumer _consumer;

        public SendOrgClaimApprovalNotificationEmailConsumerTests()
        {
            _harness = new InMemoryTestHarness();
            _emailServiceMock = A.Fake<IEmailService>();
            _consumer = A.Fake<SendOrgClaimApprovalNotificationEmailConsumer>(x => x.WithArgumentsForConstructor(() => new SendOrgClaimApprovalNotificationEmailConsumer(_emailServiceMock)));
        }

        [Fact]
        public async Task ShouldSendCommandToTheConsumer()
        {
            var consumerHarness = _harness.Consumer(() => _consumer);
            await _harness.Start();
            var faker = new Faker();

            await _harness.InputQueueSendEndpoint.Send<SendOrgClaimApprovalNotificationEmail>(SendOrgClaimApprovalNotificationEmailModel.Generate());

            // endpoint should consume the message
            Assert.True(await _harness.Consumed.Any<SendOrgClaimApprovalNotificationEmail>());

            // actual consumer should consume the message
            Assert.True(await consumerHarness.Consumed.Any<SendOrgClaimApprovalNotificationEmail>());

            await _harness.Stop();
        }

        [Fact]
        public async Task ConsumerShouldMakeCorrectEmailServiceCall()
        {
            var sendEmail = SendOrgClaimApprovalNotificationEmailModel.Generate();

            // Arrange
            var context = A.Fake<ConsumeContext<SendOrgClaimApprovalNotificationEmail>>();
            A.CallTo(() => context.Message).Returns(sendEmail);

            // Act
            await _consumer.Consume(context);

            // Assert
            A.CallTo(() => _emailServiceMock.SendOrgClaimApprovalNotificationAsync(sendEmail)).MustHaveHappened();
        }

        private class SendOrgClaimApprovalNotificationEmailModel : SendOrgClaimApprovalNotificationEmail
        {
            public int OrganizationId { get; set; }

            public int NewSigningAuthorityId { get; set; }

            public string ProvidedSiteId { get; set; }

            public static SendOrgClaimApprovalNotificationEmailModel Generate()
            {
                return new Faker<SendOrgClaimApprovalNotificationEmailModel>()
                    .RuleFor(x => x.OrganizationId, f => f.Random.Number(1, int.MaxValue))
                    .RuleFor(x => x.NewSigningAuthorityId, f => f.Random.Number(1, int.MaxValue))
                    .RuleFor(x => x.ProvidedSiteId, f => f.Lorem.Letter(3))
                    .Generate();
            }
        }
    }
}

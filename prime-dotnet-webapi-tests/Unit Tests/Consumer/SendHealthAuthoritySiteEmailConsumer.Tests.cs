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
    public class SendHealthAuthoritySiteEmailConsumerTests
    {
        private InMemoryTestHarness _harness;
        private IEmailService _emailServiceMock;
        private SendHealthAuthoritySiteEmailConsumer _consumer;

        public SendHealthAuthoritySiteEmailConsumerTests()
        {
            _harness = new InMemoryTestHarness();
            _emailServiceMock = A.Fake<IEmailService>();
            _consumer = A.Fake<SendHealthAuthoritySiteEmailConsumer>(x => x.WithArgumentsForConstructor(() => new SendHealthAuthoritySiteEmailConsumer(_emailServiceMock)));
        }

        [Fact]
        public async Task ShouldSendCommandToTheConsumer()
        {
            var consumerHarness = _harness.Consumer(() => _consumer);
            await _harness.Start();
            var faker = new Faker();

            await _harness.InputQueueSendEndpoint.Send<SendHealthAuthoritySiteEmail>(SendHealthAuthoritySiteEmailModel.Generate());

            // endpoint should consume the message
            Assert.True(await _harness.Consumed.Any<SendHealthAuthoritySiteEmail>());

            // actual consumer should consume the message
            Assert.True(await consumerHarness.Consumed.Any<SendHealthAuthoritySiteEmail>());

            await _harness.Stop();
        }

        [Fact]
        public async Task ConsumerShouldMakeCorrectEmailServiceCall()
        {
            var sendEmail = SendHealthAuthoritySiteEmailModel.Generate();

            // Arrange
            var context = A.Fake<ConsumeContext<SendHealthAuthoritySiteEmail>>();
            A.CallTo(() => context.Message).Returns(sendEmail);

            // Act
            await _consumer.Consume(context);

            // Assert
            A.CallTo(() => _emailServiceMock.SendHealthAuthoritySiteRegistrationSubmissionAsync(sendEmail.SiteId)).MustHaveHappened();
        }

        private class SendHealthAuthoritySiteEmailModel : SendHealthAuthoritySiteEmail
        {
            public int SiteId { get; set; }

            public static SendHealthAuthoritySiteEmailModel Generate()
            {
                return new Faker<SendHealthAuthoritySiteEmailModel>()
                    .RuleFor(x => x.SiteId, f => f.Random.Number(1, int.MaxValue))
                    .Generate();;
            }
        }
    }
}

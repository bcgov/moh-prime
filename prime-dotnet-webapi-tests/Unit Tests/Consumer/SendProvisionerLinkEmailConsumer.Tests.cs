using System.Collections.Generic;
using System.Threading.Tasks;

using Bogus;
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
    public class SendProvisionerLinkEmailConsumerTests
    {
        private InMemoryTestHarness _harness;
        private IEmailService _emailServiceMock;
        private SendProvisionerLinkEmailConsumer _consumer;

        public SendProvisionerLinkEmailConsumerTests()
        {
            _harness = new InMemoryTestHarness();
            _emailServiceMock = A.Fake<IEmailService>();
            _consumer = A.Fake<SendProvisionerLinkEmailConsumer>(x => x.WithArgumentsForConstructor(() => new SendProvisionerLinkEmailConsumer(_emailServiceMock)));
        }

        [Fact]
        public async Task ShouldSendCommandToTheConsumer()
        {
            var consumerHarness = _harness.Consumer(() => _consumer);
            await _harness.Start();
            await _harness.InputQueueSendEndpoint.Send<SendProvisionerLinkEmail>(SendProvisionerLinkEmailModel.Generate());

            // endpoint should consume the message
            Assert.True(await _harness.Consumed.Any<SendProvisionerLinkEmail>());

            // actual consumer should consume the message
            Assert.True(await consumerHarness.Consumed.Any<SendProvisionerLinkEmail>());

            await _harness.Stop();
        }

        [Fact]
        public async Task ConsumerShouldMakeCorrectEmailServiceCall()
        {
            var sendEmail = SendProvisionerLinkEmailModel.Generate();

            // Arrange
            var context = A.Fake<ConsumeContext<SendProvisionerLinkEmail>>();
            A.CallTo(() => context.Message).Returns(sendEmail);

            // Act
            await _consumer.Consume(context);

            // Assert
            A.CallTo(() => _emailServiceMock.SendProvisionerLinkAsync(sendEmail)).MustHaveHappened();
        }

        private class SendProvisionerLinkEmailModel : SendProvisionerLinkEmail
        {
            public IEnumerable<string> RecipientEmails { get; set; }

            public int EnrolleeId { get; set; }

            public string TokenUrl { get; set; }

            public int CareSettingCode { get; set; }

            public static SendProvisionerLinkEmailModel Generate()
            {
                return new Faker<SendProvisionerLinkEmailModel>()
                    .RuleFor(x => x.EnrolleeId, f => f.Random.Number(1, int.MaxValue))
                    .RuleFor(x => x.TokenUrl, f => f.Internet.Url())
                    .RuleFor(x => x.CareSettingCode, f => f.PickRandom(CareSettingLookup.SiteCareSettings).Code)
                    .RuleFor(x => x.RecipientEmails, f => f.Make(f.Random.Number(0, 3), () => f.Person.Email))
                    .Generate();
            }
        }
    }
}

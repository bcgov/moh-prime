using System;
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

namespace PrimeTests.UnitTests.Consumer
{
    public class SendEnrolleeEmailConsumerTests
    {
        private InMemoryTestHarness _harness;
        private IEmailService _emailServiceMock;
        private SendEnrolleeEmailConsumer _consumer;

        public SendEnrolleeEmailConsumerTests()
        {
            _harness = new InMemoryTestHarness();
            _emailServiceMock = A.Fake<IEmailService>();
            _consumer = A.Fake<SendEnrolleeEmailConsumer>(x => x.WithArgumentsForConstructor(() => new SendEnrolleeEmailConsumer(_emailServiceMock)));
        }

        [Fact]
        public async Task ShouldSendCommandToTheConsumer()
        {
            var consumerHarness = _harness.Consumer(() => _consumer);
            await _harness.Start();
            var faker = new Faker();
            var sendEnrolleeEmail = new {
                EmailType = faker.PickRandom(Enum.GetValues<EnrolleeEmailType>()),
                EnrolleeId = faker.Random.Number(1, int.MaxValue)
            };

            await _harness.InputQueueSendEndpoint.Send<SendEnrolleeEmail>(sendEnrolleeEmail);

            // endpoint should consume the message
            Assert.True(await _harness.Consumed.Any<SendEnrolleeEmail>());

            // actual consumer should consume the message
            Assert.True(await consumerHarness.Consumed.Any<SendEnrolleeEmail>());

            await _harness.Stop();
        }

        [Theory]
        [MemberData(nameof(EnrolleeEmailTypes))]
        public async Task ConsumerShouldMakeCorrectEmailServiceCall(EnrolleeEmailType emailType)
        {
            var sendEnrolleeEmail = new Faker<SendEnrolleeEmailModel>()
                .RuleFor(x => x.EmailType, _ => emailType)
                .RuleFor(x => x.EnrolleeId, f => f.Random.Number(1, int.MaxValue))
                .Generate();;

            // Arrange
            var context = A.Fake<ConsumeContext<SendEnrolleeEmail>>();
            A.CallTo(() => context.Message).Returns(sendEnrolleeEmail);

            // Act
            await _consumer.Consume(context);

            // Assert
            switch (emailType)
            {
                case EnrolleeEmailType.EnrolleeRenewal:
                    A.CallTo(() => _emailServiceMock.SendEnrolleeRenewalEmails()).MustHaveHappened();
                    break;
                case EnrolleeEmailType.PaperEnrolmentSubmission:
                    A.CallTo(() => _emailServiceMock.SendPaperEnrolmentSubmissionEmailAsync(sendEnrolleeEmail.EnrolleeId)).MustHaveHappened();
                    break;
                case EnrolleeEmailType.Reminder:
                    A.CallTo(() => _emailServiceMock.SendReminderEmailAsync(sendEnrolleeEmail.EnrolleeId)).MustHaveHappened();
                    break;
                case EnrolleeEmailType.UnsignedToaReminder:
                    A.CallTo(() => _emailServiceMock.SendEnrolleeUnsignedToaReminderEmails()).MustHaveHappened();
                    break;
                default:
                    A.CallTo(_emailServiceMock).MustNotHaveHappened();
                    break;
            }
        }

        public static IEnumerable<object[]> EnrolleeEmailTypes()
        {
            foreach (var emailType in Enum.GetValues<EnrolleeEmailType>())
            {
                yield return new object[] { emailType };
            }
        }

        private class SendEnrolleeEmailModel : SendEnrolleeEmail
        {
            public EnrolleeEmailType EmailType { get; set; }
            public int EnrolleeId { get; set; }
        }
    }
}

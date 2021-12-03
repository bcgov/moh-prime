using System;
using System.Threading.Tasks;

using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Logging;

using Prime.Contracts;
using Prime.Services;

namespace Prime.Consumer
{
    public class SendEnrolleeEmailConsumer : SendEmailConsumerBase, IConsumer<SendEnrolleeEmail>
    {
        public SendEnrolleeEmailConsumer(
            IEmailService emailService,
            ILogger<SendEnrolleeEmailConsumer> logger) : base(emailService, logger)
        { }

        public async Task Consume(ConsumeContext<SendEnrolleeEmail> context)
        {
            _logger.LogInformation("Sending {0} email", Enum.GetName(typeof(EnrolleeEmailType), context.Message.EmailType));

            switch (context.Message.EmailType)
            {
                case EnrolleeEmailType.Reminder:
                    await _emailService.SendReminderEmailAsync(context.Message.EnrolleeId);
                    break;
                case EnrolleeEmailType.EnrolleeRenewal:
                    await _emailService.SendEnrolleeRenewalEmails();
                    break;
                case EnrolleeEmailType.PaperEnrolmentSubmission:
                    await _emailService.SendPaperEnrolmentSubmissionEmailAsync(context.Message.EnrolleeId);
                    break;
                default:
                    break;
            }
        }
    }

    public class SendEmrolleeEmailConsumerDefinition : ConsumerDefinition<SendEnrolleeEmailConsumer>
    {
        public SendEmrolleeEmailConsumerDefinition()
        {
            EndpointName = nameof(SendEnrolleeEmail);
            ConcurrentMessageLimit = PrimeConfiguration.Current.ServiceBus.ConcurrencyLimit;
        }
    }
}

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
        protected readonly ILogger _logger;

        public SendEnrolleeEmailConsumer(IEmailService emailService, ILogger<SendEnrolleeEmailConsumer> logger) : base(emailService)
        {
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<SendEnrolleeEmail> context)
        {
            _logger.LogDebug("SendEnrolleeEmailConsumer.Consume called ...");
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
                case EnrolleeEmailType.UnsignedToaReminder:
                    await _emailService.SendEnrolleeUnsignedToaReminderEmails();
                    break;
                default:
                    break;
            }
            _logger.LogDebug("SendEnrolleeEmailConsumer.Consume completed");
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

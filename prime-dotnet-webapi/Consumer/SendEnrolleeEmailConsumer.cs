using System.Threading.Tasks;

using MassTransit;
using MassTransit.Definition;

using Prime.Contracts;
using Prime.Services;

namespace Prime.Consumer
{
    public class SendEnrolleeEmailConsumer : SendEmailConsumerBase, IConsumer<SendEnrolleeEmail>
    {
        public SendEnrolleeEmailConsumer(IEmailService emailService) : base(emailService)
        { }

        public async Task Consume(ConsumeContext<SendEnrolleeEmail> context)
        {
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

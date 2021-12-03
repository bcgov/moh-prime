using System.Threading.Tasks;

using MassTransit;
using Microsoft.Extensions.Logging;

using Prime.Services;
using Prime.Contracts;
using MassTransit.Definition;

namespace Prime.Consumer
{
    public class SendProvisionerLinkEmailConsumer : SendEmailConsumerBase, IConsumer<SendProvisionerLinkEmail>
    {
        public SendProvisionerLinkEmailConsumer(
            IEmailService emailService,
            ILogger<SendProvisionerLinkEmailConsumer> logger) : base(emailService, logger)
        { }

        public async Task Consume(ConsumeContext<SendProvisionerLinkEmail> context)
        {
            _logger.LogInformation("Sending ProvisionerLink email");

            await _emailService.SendProvisionerLinkAsync(
                context.Message.RecipientEmails,
                context.Message.EnrolmentCertificateAccessToken,
                context.Message.CareSettingCode);
        }
    }

    public class SendProvisionerLinkEmailConsumerDefinition : ConsumerDefinition<SendProvisionerLinkEmailConsumer>
    {
        public SendProvisionerLinkEmailConsumerDefinition()
        {
            EndpointName = nameof(SendProvisionerLinkEmail);
            ConcurrentMessageLimit = PrimeConfiguration.Current.ServiceBus.ConcurrencyLimit;
        }
    }
}

using System.Threading.Tasks;

using MassTransit;
using MassTransit.Definition;

using Prime.Contracts;
using Prime.Services;

namespace Prime.Consumer
{
    public class SendProvisionerLinkEmailConsumer : SendEmailConsumerBase, IConsumer<SendProvisionerLinkEmail>
    {
        public SendProvisionerLinkEmailConsumer(IEmailService emailService) : base(emailService)
        { }

        public async Task Consume(ConsumeContext<SendProvisionerLinkEmail> context)
        {
            await _emailService.SendProvisionerLinkAsync(context.Message);
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

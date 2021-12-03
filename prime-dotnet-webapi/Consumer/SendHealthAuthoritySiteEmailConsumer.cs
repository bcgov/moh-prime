using System.Threading.Tasks;

using MassTransit;
using MassTransit.Definition;

using Prime.Services;
using Prime.Contracts;

namespace Prime.Consumer
{
    public class SendHealthAuthoritySiteEmailConsumer : SendEmailConsumerBase, IConsumer<SendHealthAuthoritySiteEmail>
    {
        public SendHealthAuthoritySiteEmailConsumer(IEmailService emailService) : base(emailService)
        { }

        public async Task Consume(ConsumeContext<SendHealthAuthoritySiteEmail> context)
        {
            await _emailService.SendHealthAuthoritySiteRegistrationSubmissionAsync(context.Message.SiteId);
        }
    }

    public class SendHealthAuthoritySiteEmailConsumerDefinition : ConsumerDefinition<SendHealthAuthoritySiteEmailConsumer>
    {
        public SendHealthAuthoritySiteEmailConsumerDefinition()
        {
            EndpointName = nameof(SendHealthAuthoritySiteEmail);
            ConcurrentMessageLimit = PrimeConfiguration.Current.ServiceBus.ConcurrencyLimit;
        }
    }
}

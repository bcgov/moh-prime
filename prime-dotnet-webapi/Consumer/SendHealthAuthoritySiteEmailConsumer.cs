using System.Threading.Tasks;

using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Logging;

using Prime.Services;
using Prime.Contracts;

namespace Prime.Consumer
{
    public class SendHealthAuthoritySiteEmailConsumer : SendEmailConsumerBase, IConsumer<SendHealthAuthoritySiteEmail>
    {
        public SendHealthAuthoritySiteEmailConsumer(
            IEmailService emailService,
            ILogger<SendHealthAuthoritySiteEmailConsumer> logger) : base(emailService, logger)
        { }

        public async Task Consume(ConsumeContext<SendHealthAuthoritySiteEmail> context)
        {
            _logger.LogInformation("Sending HealthAuthoritySiteRegistrationSubmission email");

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

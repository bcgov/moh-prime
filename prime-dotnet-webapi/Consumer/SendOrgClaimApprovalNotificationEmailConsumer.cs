using System.Threading.Tasks;

using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Logging;

using Prime.Contracts;
using Prime.Services;

namespace Prime.Consumer
{
    public class SendOrgClaimApprovalNotificationEmailConsumer : SendEmailConsumerBase, IConsumer<SendOrgClaimApprovalNotificationEmail>
    {
        public SendOrgClaimApprovalNotificationEmailConsumer(
            IEmailService emailService,
            ILogger<SendOrgClaimApprovalNotificationEmailConsumer> logger) : base(emailService, logger)
        { }

        public async Task Consume(ConsumeContext<SendOrgClaimApprovalNotificationEmail> context)
        {
            _logger.LogInformation("Sending OrgClaimApprovalNotification email");

            await _emailService.SendOrgClaimApprovalNotificationAsync(context.Message.OrganizationClaim);
        }
    }

    public class SendOrgClaimApprovalNotificationEmailConsumerDefinition : ConsumerDefinition<SendOrgClaimApprovalNotificationEmailConsumer>
    {
        public SendOrgClaimApprovalNotificationEmailConsumerDefinition()
        {
            EndpointName = nameof(SendOrgClaimApprovalNotificationEmail);
            ConcurrentMessageLimit = PrimeConfiguration.Current.ServiceBus.ConcurrencyLimit;
        }
    }
}

using System.Threading.Tasks;

using MassTransit;
using MassTransit.Definition;

using Prime.Contracts;
using Prime.Services;

namespace Prime.Consumer
{
    public class SendOrgClaimApprovalNotificationEmailConsumer : SendEmailConsumerBase, IConsumer<SendOrgClaimApprovalNotificationEmail>
    {
        public SendOrgClaimApprovalNotificationEmailConsumer(IEmailService emailService) : base(emailService)
        { }

        public async Task Consume(ConsumeContext<SendOrgClaimApprovalNotificationEmail> context)
        {
            await _emailService.SendOrgClaimApprovalNotificationAsync(context.Message);
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

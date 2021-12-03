using System;
using System.Threading.Tasks;

using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Logging;

using Prime.Contracts;
using Prime.Services;

namespace Prime.Consumer
{
    public class SendSiteEmailConsumer : SendEmailConsumerBase, IConsumer<SendSiteEmail>
    {
        public SendSiteEmailConsumer(
            IEmailService emailService,
            ILogger<SendSiteEmailConsumer> logger) : base(emailService, logger)
        { }

        public async Task Consume(ConsumeContext<SendSiteEmail> context)
        {
            _logger.LogInformation("Consuming message from {0} to {1}", context.SourceAddress.AbsoluteUri, context.DestinationAddress.AbsoluteUri);
            _logger.LogInformation("Sending {0} email", Enum.GetName(typeof(SiteEmailType), context.Message.EmailType));

            switch (context.Message.EmailType)
            {
                case SiteEmailType.SiteRegistrationSubmission:
                    await _emailService.SendSiteRegistrationSubmissionAsync(context.Message.Site);
                    break;
                case SiteEmailType.BusinessLicenceUploaded:
                    await _emailService.SendBusinessLicenceUploadedAsync(context.Message.Site);
                    break;
                case SiteEmailType.SiteApprovedHIBC:
                    await _emailService.SendSiteApprovedHIBCAsync(context.Message.Site);
                    break;
                case SiteEmailType.RemoteUsersUpdated:
                    await _emailService.SendRemoteUsersUpdatedAsync(context.Message.Site);
                    break;
                case SiteEmailType.SiteApprovedPharmaNetAdministrator:
                    await _emailService.SendSiteApprovedPharmaNetAdministratorAsync(context.Message.Site);
                    break;
                case SiteEmailType.SiteApprovedSigningAuthority:
                    await _emailService.SendSiteApprovedSigningAuthorityAsync(context.Message.Site);
                    break;
                case SiteEmailType.RemoteUserNotifications:
                    await _emailService.SendRemoteUserNotificationsAsync(context.Message.Site);
                    break;
                case SiteEmailType.SiteActiveBeforeRegistration:
                    await _emailService.SendSiteActiveBeforeRegistrationAsync(context.Message.Site);
                    break;
                case SiteEmailType.SiteReviewedNotification:
                    await _emailService.SendSiteReviewedNotificationAsync(context.Message.Site.Id, context.Message.Note);
                    break;
                default:
                    break;
            }
        }
    }

    public class SendSiteEmailConsumerDefinition : ConsumerDefinition<SendSiteEmailConsumer>
    {
        public SendSiteEmailConsumerDefinition()
        {
            EndpointName = nameof(SendSiteEmail);
            ConcurrentMessageLimit = PrimeConfiguration.Current.ServiceBus.ConcurrencyLimit;
        }
    }
}

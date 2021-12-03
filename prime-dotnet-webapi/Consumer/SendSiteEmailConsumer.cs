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
        public SendSiteEmailConsumer(IEmailService emailService) : base(emailService)
        { }

        public async Task Consume(ConsumeContext<SendSiteEmail> context)
        {
            switch (context.Message.EmailType)
            {
                case SiteEmailType.SiteRegistrationSubmission:
                    await _emailService.SendSiteRegistrationSubmissionAsync(context.Message);
                    break;
                case SiteEmailType.BusinessLicenceUploaded:
                    await _emailService.SendBusinessLicenceUploadedAsync(context.Message);
                    break;
                case SiteEmailType.SiteApprovedHIBC:
                    await _emailService.SendSiteApprovedHIBCAsync(context.Message);
                    break;
                case SiteEmailType.RemoteUsersUpdated:
                    await _emailService.SendRemoteUsersUpdatedAsync(context.Message);
                    break;
                case SiteEmailType.SiteApprovedPharmaNetAdministrator:
                    await _emailService.SendSiteApprovedPharmaNetAdministratorAsync(context.Message);
                    break;
                case SiteEmailType.SiteApprovedSigningAuthority:
                    await _emailService.SendSiteApprovedSigningAuthorityAsync(context.Message);
                    break;
                case SiteEmailType.RemoteUserNotifications:
                    await _emailService.SendRemoteUserNotificationsAsync(context.Message);
                    break;
                case SiteEmailType.SiteActiveBeforeRegistration:
                    await _emailService.SendSiteActiveBeforeRegistrationAsync(context.Message);
                    break;
                case SiteEmailType.SiteReviewedNotification:
                    await _emailService.SendSiteReviewedNotificationAsync(context.Message);
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

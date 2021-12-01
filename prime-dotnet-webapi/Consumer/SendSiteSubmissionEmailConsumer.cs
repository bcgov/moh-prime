using System.Threading.Tasks;

using MassTransit;

using Prime.Services;
using Prime.Contracts;

namespace Prime.Consumer
{
    public class SendSiteSubmissionEmailConsumer : IConsumer<SendSiteSubmissionEmail>
    {
        private readonly IEmailService _emailService;

        public SendSiteSubmissionEmailConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<SendSiteSubmissionEmail> context)
        {
            await _emailService.SendSiteRegistrationSubmissionAsync(
                context.Message.SiteId,
                context.Message.BusinessLicenceId,
                context.Message.CareSettingCode);
        }
    }
}

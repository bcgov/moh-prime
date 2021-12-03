using Prime.Services;

namespace Prime.Consumer
{
    public abstract class SendEmailConsumerBase
    {
        protected readonly IEmailService _emailService;

        public SendEmailConsumerBase(IEmailService emailService)
        {
            _emailService = emailService;
        }
    }
}

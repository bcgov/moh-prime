using Microsoft.Extensions.Logging;
using Prime.Services;

namespace Prime.Consumer
{
    public abstract class SendEmailConsumerBase
    {
        protected readonly IEmailService _emailService;
        protected readonly ILogger _logger;

        public SendEmailConsumerBase(
            IEmailService emailService,
            ILogger logger)
        {
            _emailService = emailService;
            _logger = logger;
        }
    }
}

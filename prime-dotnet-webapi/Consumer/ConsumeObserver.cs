using System;
using System.Threading.Tasks;

using MassTransit;
using Microsoft.Extensions.Logging;
using Prime.Contracts;

namespace Prime.Consumer
{
    public class ConsumeObserver : IConsumeObserver
    {
        private readonly ILogger _logger;

        public ConsumeObserver(ILogger logger)
        {
            _logger = logger;
        }

        public async Task PreConsume<T>(ConsumeContext<T> context) where T : class
        {
            string emailType = string.Empty;
            var messageType = typeof(T);
            if (messageType == typeof(SendSiteEmail))
            {
                emailType = Enum.GetName(typeof(SiteEmailType), (context.Message as SendSiteEmail).EmailType);
            }
            else if (messageType == typeof(SendEnrolleeEmail))
            {
                emailType = Enum.GetName(typeof(EnrolleeEmailType), (context.Message as SendEnrolleeEmail).EmailType);
            }

            _logger.LogInformation($"Consuming message {typeof(T).Name} {context.MessageId} {emailType}");
            await Task.CompletedTask;
        }

        public async Task PostConsume<T>(ConsumeContext<T> context) where T : class
        {
            _logger.LogInformation($"Successfully consumed message {typeof(T).Name} {context.MessageId}");
            await Task.CompletedTask;
        }

        public async Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
        {
            _logger.LogError($"Consumer fault consuming message {typeof(T).Name} {context.MessageId}: {exception.Message}");
            await Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Prime.Helpers
{
    /// <summary>
    /// Based on https://blog.elmah.io/asp-net-core-request-logging-middleware/
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
            _logger.LogInformation("In RequestLoggingMiddleware constructor");
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation(
                "Incoming request:  {method} {url}",
                context.Request?.Method,
                context.Request?.Path.Value);

            await _next(context);
        }
    }
}

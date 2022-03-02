using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Prime.Helpers
{
    public class SentryMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public SentryMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
        }

        public async Task Invoke(HttpContext httpContext, ISentryErrorReporter errorReporter)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                await errorReporter.CaptureAsync(ex);

                // We're not handling, just logging. Throw it for someone else to take care of it.
                throw;
            }
        }
    }
}

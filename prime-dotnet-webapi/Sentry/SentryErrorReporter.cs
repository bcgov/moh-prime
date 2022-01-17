using System;
using System.Threading.Tasks;

using Prime;
using SharpRaven;

namespace SentryCustomReporter
{
    public class SentryErrorReporter : ISentryErrorReporter
    {
        private readonly IRavenClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="SentryErrorReporter" /> class.
        /// </summary>
        public SentryErrorReporter()
        {
            var sentryDsn = PrimeConfiguration.Current.Sentry.Dsn;

            if (!string.IsNullOrEmpty(sentryDsn))
            {
                _client = new RavenClient(sentryDsn);
            }
        }

        /// <summary>
        /// Captures the specified exception asynchronously and hands it off to an error handling service.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">exception</exception>
        public async Task CaptureAsync(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            await _client.CaptureAsync(new SharpRaven.Data.SentryEvent(exception));
        }
    }
}
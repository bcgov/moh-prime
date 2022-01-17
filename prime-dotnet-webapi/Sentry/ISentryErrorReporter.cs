using System;
using System.Threading.Tasks;

namespace SentryCustomReporter
{
    public interface ISentryErrorReporter
    {
        Task CaptureAsync(Exception exception);
    }
}

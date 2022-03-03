using System;
using System.Threading.Tasks;

namespace Prime.Helpers
{
    public interface ISentryErrorReporter
    {
        Task CaptureAsync(Exception exception);
    }
}

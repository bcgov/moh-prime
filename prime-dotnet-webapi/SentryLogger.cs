using System;
using System.Globalization;
using System.Diagnostics;
using Sentry.Extensibility;
using Sentry;


public class SentryLogger : IDiagnosticLogger
{
    private readonly SentryLevel _minimalLevel;

    public SentryLogger(SentryLevel minimalLevel)
    {
        _minimalLevel = minimalLevel;
    }

    public bool IsEnabled(SentryLevel level)
    {
        return level >= _minimalLevel;
    }

    public void Log(SentryLevel logLevel, string message, Exception exception = null, params object[] args)
    {
        // Debug.WriteLine(DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.ffffZ", DateTimeFormatInfo.InvariantInfo), message, args);
        // Debug.WriteLine(message, args);
        // if (exception is Exception)
        // {
        //     Debug.WriteLine("Exception: ", exception);
        // }
    }
}

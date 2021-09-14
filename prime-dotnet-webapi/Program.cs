using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;


using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;
using Sentry.AspNetCore;
using Sentry.Serilog;

namespace Prime
{
    public class Program
    {
        public static int Main(string[] args)
        {
            CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args)
                    .Build()
                    .Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                // Ensure buffered logs are written to their target sink
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSentry(o => InitSentry(o));
                    webBuilder.UseStartup<Startup>();
                });

        private static void InitSentry(SentryAspNetCoreOptions sentryOptions)
        {
            string dsn = PrimeEnvironment.IsLocal
                ? PrimeEnvironment.PrimeSentryKeys.DevEnvDsn
                : PrimeEnvironment.PrimeSentryKeys.ProdEnvDsn;

            double sampleTraceRate = PrimeEnvironment.IsLocal
                ? PrimeEnvironment.PrimeSentryKeys.DevEnvTraceSampleRate
                : PrimeEnvironment.PrimeSentryKeys.ProdEnvTraceSampleRate;

            // Tells which project in Sentry to send events to:
            sentryOptions.Dsn = dsn;

            // When configuring for the first time, to see what the SDK is doing:
            sentryOptions.Debug = PrimeEnvironment.IsLocal;

            // Set traces_sample_rate to 1.0 to capture 100% of transactions for performance monitoring.
            // We recommend adjusting this value in production.
            sentryOptions.TracesSampleRate = sampleTraceRate;
        }

        private static void InitSentrySerilog(SentrySerilogOptions sentrySerilogOptions)
        {
            // Debug and higher are stored as breadcrumbs (default is Information)
            sentrySerilogOptions.MinimumBreadcrumbLevel = LogEventLevel.Debug;
            // Warning and higher is sent as event (default is Error)
            sentrySerilogOptions.MinimumEventLevel = LogEventLevel.Warning;
        }

        private static void CreateLogger()
        {
            var path = PrimeEnvironment.LogFile;

            try
            {
                if (PrimeEnvironment.IsLocal)
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Creating the logging directory failed: {0}", e.ToString());
            }

            var name = Assembly.GetExecutingAssembly().GetName();
            var outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Assembly", $"{name.Name}")
                .Enrich.WithProperty("Version", $"{name.Version}")
                .WriteTo.Console(
                    outputTemplate: outputTemplate,
                    theme: AnsiConsoleTheme.Code)
                .WriteTo.Async(a => a.File(
                    $@"{path}/prime.log",
                    outputTemplate: outputTemplate,
                    rollingInterval: RollingInterval.Day,
                    shared: true))
                .WriteTo.Async(a => a.File(
                    new JsonFormatter(),
                    $@"{path}/prime.json",
                    rollingInterval: RollingInterval.Day))
                .WriteTo.Sentry(serilogOptionso => InitSentrySerilog(serilogOptionso))
                .CreateLogger();
        }
    }
}

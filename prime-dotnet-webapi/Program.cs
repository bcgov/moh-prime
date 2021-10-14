using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;


using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;
using Sentry.AspNetCore;
using Sentry.Serilog;
using System;
using System.IO;
using System.Reflection;

using Prime.Configuration.Environment;

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
            // By default, .CreateDefaultBuilder() adds Configuration from the following, in order:
            // 1. appsettings.json
            // 2. appsettings.{EnvironmentName}.json,
            // 3. user secrets (only in local development)
            // 4. environment variables
            // 5. command line arguments
            // See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-5.0
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.Add(new PrimeEnvironmentVariablesConfigurationSource());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSentry(o => InitSentry(o));
                    webBuilder.UseStartup<Startup>();
                });

        private static void InitSentry(SentryAspNetCoreOptions sentryOptions)
        {
            if (PrimeEnvironment.IsLocal)
            {
                sentryOptions.Dsn = PrimeEnvironment.PrimeSentryKeys.DevEnvDsn;
                sentryOptions.TracesSampleRate = PrimeEnvironment.PrimeSentryKeys.DevEnvTraceSampleRate;
            }
            else if (PrimeEnvironment.IsProduction)
            {
                sentryOptions.Dsn = PrimeEnvironment.PrimeSentryKeys.ProdEnvDsn;
                sentryOptions.TracesSampleRate = PrimeEnvironment.PrimeSentryKeys.ProdEnvTraceSampleRate;
            }
            else
            {
                sentryOptions.Dsn = PrimeEnvironment.PrimeSentryKeys.TestEnvDsn;
                sentryOptions.TracesSampleRate = PrimeEnvironment.PrimeSentryKeys.TestEnvTraceSampleRate;
            }
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
            var path = PrimeConfiguration.LogFilePath;

            try
            {
                if (PrimeConfiguration.IsDevelopment())
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
                .WriteTo.Sentry(serilogOptions => InitSentrySerilog(serilogOptions))
                .CreateLogger();
        }
    }
}

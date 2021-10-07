using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;
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
            Log.Information($"LOG_LEVEL={Environment.GetEnvironmentVariable("LOG_LEVEL")}");

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
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.Add(new PrimeEnvironmentVariablesConfigurationSource());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();

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

            // Default is Information but can be overridden by LOG_LEVEL environment variable, 
            // (expecting a number) 
            int minimumLogLevel = (int)LogEventLevel.Information;
            if (Int32.TryParse(Environment.GetEnvironmentVariable("LOG_LEVEL"), out int dynamicLogLevel))
            {
                minimumLogLevel = dynamicLogLevel;
            }

            var logLevelSwitch = new LoggingLevelSwitch()
            {
                MinimumLevel = (LogEventLevel)minimumLogLevel
            };
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(logLevelSwitch)
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
                .CreateLogger();
        }
    }
}

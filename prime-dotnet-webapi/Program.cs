using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Formatting.Json;

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
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .UseSerilog();

        private static void CreateLogger()
        {
            string path = PrimeConstants.LOG_FILE_PATH;

            try
            {
                if (isDevelopment())
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Creating the logging directory failed: {0}", e.ToString());
            }

            // TODO could update appsettings.json to appsettings.Production.json and use:
            // var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json
            var environment = (isDevelopment()) ? ".Development" : "";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // TODO could update appsettings.json to appsettings.Production.json and use:
                // .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings{environment}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var name = Assembly.GetExecutingAssembly().GetName();
            var outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

            // TODO appsettings can be used to store formatters
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                // TODO more enrichers can be added or created
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Assembly", $"{name.Name}")
                .Enrich.WithProperty("Version", $"{name.Version}")
                // TODO segregation of logs through routing to different sinks
                .WriteTo.Console(outputTemplate: outputTemplate)
                .WriteTo.Debug(outputTemplate: outputTemplate)
                .WriteTo.Async(a => a.File(
                    $@"{path}/prime.log",
                    outputTemplate: outputTemplate,
                    rollingInterval: RollingInterval.Day))
                // TODO JSON output is easy to create using JsonFormatter or RenderedCompactJsonFormatter
                // currently right now we're not using Splunk so JSON isn't as useful, but easy addition
                .WriteTo.Async(a => a.File(
                    new JsonFormatter(),
                    $@"{path}/prime.json",
                    rollingInterval: RollingInterval.Day))
                // TODO add ElasticSearch sink for Kibana
                // @see https://github.com/serilog/serilog-sinks-elasticsearch
                // TODO add sink for Splunk
                // @see https://github.com/serilog/serilog-sinks-elasticsearch
                .CreateLogger();
        }

        private static bool isDevelopment() =>
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;
    }
}

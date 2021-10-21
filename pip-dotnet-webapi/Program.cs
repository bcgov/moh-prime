using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;
using System.Reflection;

using Pip.Configuration.Environment;

namespace Pip
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
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.Add(new PipEnvironmentVariablesConfigurationSource());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();

        private static void CreateLogger()
        {
            var path = PipConfiguration.LogFilePath;

            try
            {
                if (PipConfiguration.IsDevelopment())
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
                    $@"{path}/pip.log",
                    outputTemplate: outputTemplate,
                    rollingInterval: RollingInterval.Day,
                    shared: true))
                .WriteTo.Async(a => a.File(
                    new JsonFormatter(),
                    $@"{path}/pip.json",
                    rollingInterval: RollingInterval.Day))
                .CreateLogger();
        }
    }
}

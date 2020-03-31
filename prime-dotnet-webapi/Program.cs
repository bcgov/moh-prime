using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Prime
{
    public class Program
    {
        public static int Main(string[] args)
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

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(new JsonFormatter(), $"{path}/logs.json")
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args)
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(hostingContext
                            .Configuration
                            .GetSection("Logging"));
                        logging.AddConsole();
                        logging.AddDebug();
                    })
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
                // Ensure logs in temporary holding area are not lost in shutdown
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static bool isDevelopment() => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;
    }
}

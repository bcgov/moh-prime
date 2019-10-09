
using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prime;
using Prime.Services;
using PrimeTests.Mocks;

namespace PrimeTests.Utils
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
            .UseContentRoot("prime-dotnet-webapi-tests")
            .UseStartup<TestStartup>();

            // var builder = new WebHostBuilder()
            //     .UseContentRoot(Directory.GetCurrentDirectory())
            //     .UseStartup<TestStartup>();

            // return builder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSolutionRelativeContentRoot("./prime-dotnet-webapi-tests");
            builder.UseEnvironment("Development");
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                // Add a database context (ApiDbContext) using an in-memory 
                // database for testing.
                services.AddDbContext<ApiDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApiDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        TestUtils.InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}
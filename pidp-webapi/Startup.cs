using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Serilog;
using System.Reflection;
using System.Text.Json;

using Pidp.Data;
using Pidp.Features;

namespace Pidp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var config = InitializeConfiguration(services);

            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddJsonOptions(options => options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb));

            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo { Title = "PIdP Web API", Version = "v1" }));

            services.AddDbContext<PidpDbContext>(options =>
            {
                options.UseNpgsql(config.ConnectionStrings.PidpDatabase, npg => npg.UseNodaTime())
                    .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: false);
            });

            services.Scan(scan => scan
                .FromAssemblyOf<Startup>()
                .AddClasses(classes => classes.AssignableTo<IRequestHandler>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            // TODO Healthchecks
            // services
            //     .AddHealthChecks()
            //     .AddDbContextCheck<PidpDbContext>("DbContextHealthCheck")
            //     .AddNpgSql(connectionString);
        }

        private PidpConfiguration InitializeConfiguration(IServiceCollection services)
        {
            var config = new PidpConfiguration();
            Configuration.Bind(config);

            services.AddSingleton(config);

            Log.Logger.Information("### App Version:{0} ###", Assembly.GetExecutingAssembly().GetName().Version);
            Log.Logger.Information("### PIdP Configuration:{0} ###", JsonSerializer.Serialize(config));

            return config;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "PIdP Web API");
            });

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapHealthChecks("/health");
            });
        }
    }
}

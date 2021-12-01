using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text.Json;

using Pidp.Data;

namespace Pidp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var config = InitializeConfiguration(services);

            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo { Title = "PIdP Web API", Version = "v1" }));

            services.AddDbContext<PidpDbContext>(options =>
            {
                options.UseNpgsql(config.ConnectionStrings.PidpDatabase, npg => npg.UseNodaTime())
                    .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: false);
            });

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
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

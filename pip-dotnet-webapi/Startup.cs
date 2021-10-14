using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Serilog;
using AutoMapper;
using Newtonsoft.Json;
using FluentValidation.AspNetCore;

using Pip.Services;
using Pip.Infrastructure;

namespace Pip
{
    public class Startup
    {
        public const string CorsPolicy = "CorsPolicy";
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            InitializeConfiguration(services);

            services.AddScoped<IFirstService, FirstService>();

            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new EmptyStringToNullJsonConverter());
                });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pip Web API", Version = "v1" });
            });
            services.AddSwaggerGenNewtonsoftSupport();

            ConfigureDatabase(services);
        }

        private void InitializeConfiguration(IServiceCollection services)
        {
            var config = new PipConfiguration();
            Configuration.Bind(config);
            PipConfiguration.Current = config;

            services.AddSingleton(config);

            Log.Logger.Information("###App Version:{0}###", Assembly.GetExecutingAssembly().GetName().Version);
            Log.Logger.Information("###Pip Configuration:{0}###", JsonConvert.SerializeObject(PipConfiguration.Current));
        }
        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            var connectionString = PipConfiguration.Current!.ConnectionStrings.PipDatabase;

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: false);
            });

            services
                .AddHealthChecks()
                .AddDbContextCheck<ApiDbContext>("DbContextHealthCheck")
                .AddNpgSql(connectionString);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pip Pre-Test");
            });

            // Matches request to an endpoint
            app.UseRouting();
            app.UseCors(CorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}

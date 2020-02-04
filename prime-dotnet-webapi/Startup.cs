using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prime.Services;
using Prime.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Net.Mime;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Prime
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; }
        public IWebHostEnvironment Environment { get; }
        public readonly string AllowSpecificOrigins = "CorsPolicy";

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfig = configuration;
            Environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<IEnrolleeService, EnrolleeService>();
            services.AddScoped<IAutomaticAdjudicationService, AutomaticAdjudicationService>();
            services.AddScoped<IEnrolmentCertificateService, EnrolmentCertificateService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPharmanetApiService, PharmanetApiService>();
            services.AddScoped<IPrivilegeService, PrivilegeService>();
            services.AddScoped<IAccessTermService, AccessTermService>();
            services.AddScoped<IEnrolleeProfileVersionService, EnrolleeProfileVersionService>();

            services
                // Adds support for controllers and API-related features
                .AddControllers()
                // Add a convertor <globally> to change empty strings into null on serialization
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new EmptyStringToNullJsonConverter()));

            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prime Web API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddHttpContextAccessor();

            this.ConfigureDatabase(services);

            AuthenticationSetup.Initialize(services, Configuration, Environment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // this.ConfigureHealthCheck(app);

            // TODO Turn on when there is an actual cert
            // app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prime Web API V1");
            });

            // Matches request to an endpoint
            app.UseRouting();
            app.UseCors(AllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapHealthChecks("/health");
            });
        }

        // protected virtual void ConfigureHealthCheck(IApplicationBuilder app)
        // {
        //     // Health check output
        //     var healthCheckOptions = new HealthCheckOptions
        //     {
        //         ResponseWriter = async (c, r) =>
        //         {
        //             c.Response.ContentType = MediaTypeNames.Application.Json;

        //             var result = JsonConvert.SerializeObject(
        //                 new
        //                 {
        //                     checks = r.Entries.Select(e =>
        //                     new
        //                     {
        //                         description = e.Key,
        //                         status = e.Value.Status.ToString(),
        //                         responseTime = e.Value.Duration.TotalMilliseconds
        //                     }),
        //                     totalResponseTime = r.TotalDuration.TotalMilliseconds
        //                 });

        //             await c.Response.WriteAsync(result);
        //         }
        //     };

        //     // Enable healthchecks for an single endpoint
        //     app.UseHealthChecks("/healthcheck", healthCheckOptions);
        // }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            // Connect to database
            var connectionString = System.Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if (connectionString == null)
            {
                connectionString = Configuration.GetConnectionString("PrimeDatabase");
            }

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
    }
}

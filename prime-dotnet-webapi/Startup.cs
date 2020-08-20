using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

using AutoMapper;
using IdentityModel.Client;
using Newtonsoft.Json;
using Serilog;
using Wkhtmltopdf.NetCore;

using Prime.Auth;
using Prime.Services;
using Prime.Services.Clients;
using Prime.Models.Api;
using Prime.Infrastructure;

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
            services.AddScoped<ISubmissionRulesService, SubmissionRulesService>();
            services.AddScoped<IEnrolmentCertificateService, EnrolmentCertificateService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPrivilegeService, PrivilegeService>();
            services.AddScoped<IAccessTermService, AccessTermService>();
            services.AddScoped<IEnrolleeProfileVersionService, EnrolleeProfileVersionService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IBusinessEventService, BusinessEventService>();
            services.AddScoped<ISubmissionService, SubmissionService>();
            services.AddScoped<IRazorConverterService, RazorConverterService>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<IPartyService, PartyService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IVerifiableCredentialService, VerifiableCredentialService>();
            services.AddScoped<IDocumentAccessTokenService, DocumentAccessTokenService>();

            ConfigureClients(services);

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new EmptyStringToNullJsonConverter());
                });

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("submissionAction", typeof(EnumRouteConstraint<SubmissionAction>));
            });

            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithExposedHeaders("Location");
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
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddWkhtmltopdf("./Resources/wkhtmltopdf");

            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(Startup));
            services.AddRazorPages();

            this.ConfigureDatabase(services);

            AuthenticationSetup.Initialize(services, Configuration, Environment);
        }

        protected void ConfigureClients(IServiceCollection services)
        {
            if (PrimeConstants.ENVIRONMENT_NAME == "local")
            {
                services.AddSingleton<ICollegeLicenceClient, DummyCollegeLicenceClient>();
            }
            else
            {
                services.AddTransient<CollegeLicenceClientHandler>()
                .AddHttpClient<ICollegeLicenceClient, CollegeLicenceClient>(client =>
                {
                    client.SetBasicAuthentication(PrimeConstants.PHARMANET_API_USERNAME, PrimeConstants.PHARMANET_API_PASSWORD);
                })
                .ConfigurePrimaryHttpMessageHandler<CollegeLicenceClientHandler>();
            }

            services.AddTransient<DocumentManagerBearerTokenHandler>()
            .AddHttpClient<IDocumentManagerClient, DocumentManagerClient>(client =>
            {
                client.BaseAddress = new Uri(PrimeConstants.DOCUMENT_MANAGER_URL.EnsureTrailingSlash());
            })
            .AddHttpMessageHandler<DocumentManagerBearerTokenHandler>();

            services.AddHttpClient<IAccessTokenClient, AccessTokenClient>();
            services.AddSingleton(new DocumentManagerClientCredentials
            {
                Address = PrimeConstants.KEYCLOAK_TOKEN_URL,
                ClientId = PrimeConstants.DOCUMENT_MANAGER_CLIENT_ID,
                ClientSecret = PrimeConstants.DOCUMENT_MANAGER_CLIENT_SECRET,
            });

            services.AddHttpClient<IVerifiableCredentialClient, VerifiableCredentialClient>(client =>
            {
                client.BaseAddress = new Uri(PrimeConstants.VERIFIABLE_CREDENTIAL_API_URL.EnsureTrailingSlash());
                client.DefaultRequestHeaders.Add("x-api-key", PrimeConstants.VERIFIABLE_CREDENTIAL_API_KEY);
            });

            services.AddTransient<ChesBearerTokenHandler>()
            .AddHttpClient<IChesClient, ChesClient>(client =>
            {
                client.BaseAddress = new Uri(PrimeConstants.CHES_API_URL.EnsureTrailingSlash());
            })
            .AddHttpMessageHandler<ChesBearerTokenHandler>();

            services.AddSingleton(new ChesClientCredentials
            {
                Address = $"{PrimeConstants.CHES_TOKEN_URL}/token",
                ClientId = PrimeConstants.CHES_CLIENT_ID,
                ClientSecret = PrimeConstants.CHES_CLIENT_SECRET
            });

            services.AddTransient<ISmtpEmailClient, SmtpEmailClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            this.ConfigureHealthCheck(app);

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prime Web API V1");
            });

            this.ConfigureLogging(app);

            // Matches request to an endpoint
            app.UseRouting();
            app.UseCors(AllowSpecificOrigins);

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }

        protected virtual void ConfigureHealthCheck(IApplicationBuilder app)
        {
            // Health check output
            var healthCheckOptions = new HealthCheckOptions
            {
                ResponseWriter = async (c, r) =>
                {
                    c.Response.ContentType = MediaTypeNames.Application.Json;

                    var result = JsonConvert.SerializeObject(
                        new
                        {
                            checks = r.Entries.Select(e =>
                            new
                            {
                                description = e.Key,
                                status = e.Value.Status.ToString(),
                                responseTime = e.Value.Duration.TotalMilliseconds
                            }),
                            totalResponseTime = r.TotalDuration.TotalMilliseconds
                        });

                    await c.Response.WriteAsync(result);
                }
            };

            // Enable healthchecks for an single endpoint
            app.UseHealthChecks("/healthcheck", healthCheckOptions);
        }

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

        protected virtual void ConfigureLogging(IApplicationBuilder app)
        {
            // Only logs components that appear after it in the pipeline, which
            // can be used to exclude noisy handlers from logging
            app.UseSerilogRequestLogging(options =>
            {
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    var userId = httpContext.User.GetPrimeUserId();

                    if (!userId.Equals(Guid.Empty))
                    {
                        diagnosticContext.Set("User", userId);
                    }
                };
            });
        }
    }
}

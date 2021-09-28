using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
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

using Flurl;
using Serilog;
using SoapCore;
using AutoMapper;
using Newtonsoft.Json;
using Wkhtmltopdf.NetCore;
using IdentityModel.Client;
using FluentValidation.AspNetCore;

using Prime.Auth;
using Prime.Extensions;
using Prime.Services;
using Prime.Services.EmailInternal;
using Prime.HttpClients;
using Prime.HttpClients.Mail;
using Prime.Infrastructure;

namespace Prime
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InitializeConfiguration(services);

            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAgreementService, AgreementService>();
            services.AddScoped<IAuthorizedUserService, AuthorizedUserService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<IBusinessEventService, BusinessEventService>();
            services.AddScoped<IClientLogService, ClientLogService>();
            services.AddScoped<IDocumentAccessTokenService, DocumentAccessTokenService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IEmailDocumentsService, EmailDocumentsService>();
            services.AddScoped<IEmailRenderingService, EmailRenderingService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddScoped<IEnrolleeAgreementService, EnrolleeAgreementService>();
            services.AddScoped<IEnrolleePaperSubmissionService, EnrolleePaperSubmissionService>();
            services.AddScoped<IEnrolleeService, EnrolleeService>();
            services.AddScoped<IEnrolleeSubmissionService, EnrolleeSubmissionService>();
            services.AddScoped<IEnrolmentCertificateService, EnrolmentCertificateService>();
            services.AddScoped<IGisService, GisService>();
            services.AddScoped<IHealthAuthorityService, HealthAuthorityService>();
            services.AddScoped<IHealthAuthoritySiteService, HealthAuthoritySiteService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<IMetabaseService, MetabaseService>();
            services.AddScoped<IOrganizationAgreementService, OrganizationAgreementService>();
            services.AddScoped<IOrganizationClaimService, OrganizationClaimService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IPartyService, PartyService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IPhsaService, PhsaService>();
            services.AddScoped<IPlrProviderService, PlrProviderService>();
            services.AddScoped<IPrivilegeService, PrivilegeService>();
            services.AddScoped<IRazorConverterService, RazorConverterService>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<ISoapService, SoapService>();
            services.AddScoped<ISubmissionRulesService, SubmissionRulesService>();
            services.AddScoped<ISubmissionService, SubmissionService>();
            services.AddScoped<IVerifiableCredentialService, VerifiableCredentialService>();

            services.AddSoapServiceOperationTuner(new SoapServiceOperationTuner());

            ConfigureClients(services);

            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new EmptyStringToNullJsonConverter());
                });

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithExposedHeaders("Location", "Unlocked");
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
            services.AddSoapCore();

            ConfigureDatabase(services);

            AuthenticationSetup.Initialize(services);
        }

        private void InitializeConfiguration(IServiceCollection services)
        {
            var config = new PrimeConfiguration();
            Configuration.Bind(config);
            PrimeConfiguration.Current = config;

            services.AddSingleton(config);

            Log.Logger.Information("###App Version:{0}###", Assembly.GetExecutingAssembly().GetName().Version);
            Log.Logger.Information("###Prime Configuration:{0}###", JsonConvert.SerializeObject(PrimeConfiguration.Current));
        }

        private void ConfigureClients(IServiceCollection services)
        {
            services
                .AddTransient<ISmtpEmailClient, SmtpEmailClient>()
                .AddHttpClient<IAccessTokenClient, AccessTokenClient>();

            if (PrimeConfiguration.Current.IsLocal)
            {
                services.AddSingleton<ICollegeLicenceClient, DummyCollegeLicenceClient>();
            }
            else
            {
                services
                    .AddTransient<CollegeLicenceClientHandler>()
                    .AddHttpClient<ICollegeLicenceClient, CollegeLicenceClient>(client =>
                    {
                        client.SetBasicAuthentication(PrimeConfiguration.Current.PharmanetApi.Username, PrimeConfiguration.Current.PharmanetApi.Password);
                    })
                    .ConfigurePrimaryHttpMessageHandler<CollegeLicenceClientHandler>();
            }

            services.AddSingleton(new AddressAutocompleteClientCredentials
            {
                ApiKey = PrimeConfiguration.Current.AddressAutocompleteApi.Key
            })
            .AddHttpClientWithBaseAddress<IAddressAutocompleteClient, AddressAutocompleteClient>(PrimeConfiguration.Current.AddressAutocompleteApi.Url);

            services.AddHttpClientWithBaseAddress<IChesClient, ChesClient>(PrimeConfiguration.Current.ChesApi.Url)
            .WithBearerToken(new ChesClientCredentials
            {
                Address = Url.Combine(PrimeConfiguration.Current.ChesApi.TokenUrl, "token"),
                ClientId = PrimeConfiguration.Current.ChesApi.ClientId,
                ClientSecret = PrimeConfiguration.Current.ChesApi.ClientSecret
            });

            services.AddHttpClientWithBaseAddress<IDocumentManagerClient, DocumentManagerClient>(PrimeConfiguration.Current.DocumentManager.Url)
            .WithBearerToken(new DocumentManagerClientCredentials
            {
                Address = PrimeConfiguration.Current.PrimeKeycloak.TokenUrl,
                ClientId = PrimeConfiguration.Current.DocumentManager.ClientId,
                ClientSecret = PrimeConfiguration.Current.DocumentManager.ClientSecret,
            });

            services.AddHttpClientWithBaseAddress<IPrimeKeycloakAdministrationClient, KeycloakAdministrationClient>(PrimeConfiguration.Current.PrimeKeycloak.AdministrationUrl)
            .WithBearerToken(new PrimeKeycloakAdministrationClientCredentials
            {
                Address = PrimeConfiguration.Current.PrimeKeycloak.TokenUrl,
                ClientId = PrimeConfiguration.Current.PrimeKeycloak.AdministrationClientId,
                ClientSecret = PrimeConfiguration.Current.PrimeKeycloak.AdministrationClientSecret,
            });

            services.AddHttpClientWithBaseAddress<ILdapClient, LdapClient>(PrimeConfiguration.Current.LdapApi.Url);

            services.AddHttpClientWithBaseAddress<IMohKeycloakAdministrationClient, KeycloakAdministrationClient>(PrimeConfiguration.Current.MohKeycloak.AdministrationUrl)
            .WithBearerToken(new MohKeycloakAdministrationClientCredentials
            {
                Address = PrimeConfiguration.Current.MohKeycloak.TokenUrl,
                ClientId = PrimeConfiguration.Current.MohKeycloak.AdministrationClientId,
                ClientSecret = PrimeConfiguration.Current.MohKeycloak.AdministrationClientSecret,
            });

            services.AddHttpClient<IVerifiableCredentialClient, VerifiableCredentialClient>(client =>
            {
                client.BaseAddress = new Uri(PrimeConfiguration.Current.VerifiableCredentialApi.Url.EnsureTrailingSlash());
                client.DefaultRequestHeaders.Add("x-api-key", PrimeConfiguration.Current.VerifiableCredentialApi.Key);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureHealthCheck(app);

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prime Web API V1");
            });

            ConfigureLogging(app);

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            // Matches request to an endpoint
            app.UseRouting();
            app.UseCors(CorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            // Configure security settings on a basic HTTP binding
            Binding binding = new BasicHttpBinding
            {
                Security = new BasicHttpSecurity
                {
                    Mode = BasicHttpSecurityMode.TransportCredentialOnly,
                    Transport = new HttpTransportSecurity
                    {
                        ClientCredentialType = HttpClientCredentialType.Basic
                    }
                }
            };

            app.UseEndpoints(endpoints =>
            {
                endpoints.UseSoapEndpoint<ISoapService>("/api/PLRHL7", binding, SoapSerializer.XmlSerializer);
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            if (PrimeConfiguration.Current.IsLocal)
            {
                lifetime.ApplicationStarted.Register(OnApplicationStartedAsync(app.ApplicationServices.GetRequiredService<IVerifiableCredentialClient>()).Wait);
            }
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
            var connectionString = PrimeConfiguration.Current.ConnectionStrings.PrimeDatabase;

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

        private async Task<Action> OnApplicationStartedAsync(IVerifiableCredentialClient _verifiableCredentialClient)
        {
            try
            {
                var issuerDid = await _verifiableCredentialClient.GetIssuerDidAsync();

                if (issuerDid != null)
                {
                    var schemaId = await _verifiableCredentialClient.GetSchemaId(issuerDid);
                    if (schemaId == null)
                    {
                        schemaId = await _verifiableCredentialClient.CreateSchemaAsync();
                    }

                    var credentialDefinitionId = await _verifiableCredentialClient.GetCredentialDefinitionIdAsync(schemaId);
                    if (credentialDefinitionId == null)
                    {
                        await _verifiableCredentialClient.CreateCredentialDefinitionAsync(schemaId);
                    }
                }
            }
            catch
            {
                // Agent not setup
            }
            return null;
        }
    }
}

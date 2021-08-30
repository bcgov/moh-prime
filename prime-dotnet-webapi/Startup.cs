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
using Prime.Services;
using Prime.Services.EmailInternal;
using Prime.HttpClients;
using Prime.HttpClients.Mail;
using Prime.Infrastructure;
using Prime.ViewModels.HealthAuthorities;
using Prime.ViewModels.HealthAuthoritySites;

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
            // The services are ordered alphabetically assuming the word "Service" is not included
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAgreementService, AgreementService>();
            services.AddScoped<IAuthorizedUserService, AuthorizedUserService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<IBusinessEventService, BusinessEventService>();
            services.AddScoped<IClientLogService, ClientLogService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDocumentAccessTokenService, DocumentAccessTokenService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailDocumentsService, EmailDocumentsService>();
            services.AddScoped<IEmailRenderingService, EmailRenderingService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddScoped<IEnrolleeService, EnrolleeService>();
            services.AddScoped<IEnrolleeAgreementService, EnrolleeAgreementService>();
            services.AddScoped<IEnrolleePaperSubmissionService, EnrolleePaperSubmissionService>();
            services.AddScoped<IEnrolleeSubmissionService, EnrolleeSubmissionService>();
            services.AddScoped<IEnrolmentCertificateService, EnrolmentCertificateService>();
            services.AddScoped<IGisService, GisService>();
            services.AddScoped<IHealthAuthorityService, HealthAuthorityService>();
            services.AddScoped<IHealthAuthoritySiteService, HealthAuthoritySiteService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<IMetabaseService, MetabaseService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IOrganizationAgreementService, OrganizationAgreementService>();
            services.AddScoped<IOrganizationClaimService, OrganizationClaimService>();
            services.AddScoped<IPartyService, PartyService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IPlrProviderService, PlrProviderService>();
            services.AddScoped<IPrivilegeService, PrivilegeService>();
            services.AddScoped<IRazorConverterService, RazorConverterService>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<ISoapService, SoapService>();
            services.AddScoped<ISubmissionService, SubmissionService>();
            services.AddScoped<ISubmissionRulesService, SubmissionRulesService>();
            services.AddScoped<IVerifiableCredentialService, VerifiableCredentialService>();

            services.AddSoapServiceOperationTuner(new SoapServiceOperationTuner());

            ConfigureClients(services);

            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<PrivacyOfficeValidator>())
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new EmptyStringToNullJsonConverter());
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
            services.AddSoapCore();

            ConfigureDatabase(services);

            AuthenticationSetup.Initialize(services);
        }

        protected void ConfigureClients(IServiceCollection services)
        {
            // Token Handlers and Credentials
            services.AddSingleton(new AddressAutocompleteClientCredentials
            {
                ApiKey = PrimeEnvironment.AddressAutocompleteApi.Key
            })
            .AddTransient<BearerTokenHandler<ChesClientCredentials>>()
            .AddSingleton(new ChesClientCredentials
            {
                Address = Url.Combine(PrimeEnvironment.ChesApi.TokenUrl, "token"),
                ClientId = PrimeEnvironment.ChesApi.ClientId,
                ClientSecret = PrimeEnvironment.ChesApi.ClientSecret
            })
            .AddTransient<BearerTokenHandler<DocumentManagerClientCredentials>>()
            .AddSingleton(new DocumentManagerClientCredentials
            {
                Address = PrimeEnvironment.PrimeKeycloak.TokenUrl,
                ClientId = PrimeEnvironment.DocumentManager.ClientId,
                ClientSecret = PrimeEnvironment.DocumentManager.ClientSecret,
            })
            .AddTransient<BearerTokenHandler<KeycloakAdministrationClientCredentials>>()
            .AddSingleton(new KeycloakAdministrationClientCredentials
            {
                Address = PrimeEnvironment.PrimeKeycloak.TokenUrl,
                ClientId = PrimeEnvironment.PrimeKeycloak.AdministrationClientId,
                ClientSecret = PrimeEnvironment.PrimeKeycloak.AdministrationClientSecret,
            });

            // Clients
            services.AddTransient<ISmtpEmailClient, SmtpEmailClient>()
            .AddHttpClient<IAccessTokenClient, AccessTokenClient>();

            if (PrimeEnvironment.IsLocal)
            {
                services.AddSingleton<ICollegeLicenceClient, DummyCollegeLicenceClient>();
            }
            else
            {
                services.AddTransient<CollegeLicenceClientHandler>()
                .AddHttpClient<ICollegeLicenceClient, CollegeLicenceClient>(client =>
                {
                    client.SetBasicAuthentication(PrimeEnvironment.PharmanetApi.Username, PrimeEnvironment.PharmanetApi.Password);
                })
                .ConfigurePrimaryHttpMessageHandler<CollegeLicenceClientHandler>();
            }

            services.AddHttpClient<IDocumentManagerClient, DocumentManagerClient>(client =>
            {
                client.BaseAddress = new Uri(PrimeEnvironment.DocumentManager.Url.EnsureTrailingSlash());
            })
            .AddHttpMessageHandler<BearerTokenHandler<DocumentManagerClientCredentials>>();

            services.AddHttpClient<IKeycloakAdministrationClient, KeycloakAdministrationClient>(client =>
            {
                client.BaseAddress = new Uri(PrimeEnvironment.PrimeKeycloak.AdministrationUrl.EnsureTrailingSlash());
            })
            .AddHttpMessageHandler<BearerTokenHandler<KeycloakAdministrationClientCredentials>>();

            services.AddHttpClient<IVerifiableCredentialClient, VerifiableCredentialClient>(client =>
            {
                client.BaseAddress = new Uri(PrimeEnvironment.VerifiableCredentialApi.Url.EnsureTrailingSlash());
                client.DefaultRequestHeaders.Add("x-api-key", PrimeEnvironment.VerifiableCredentialApi.Key);
            });

            services.AddHttpClient<IChesClient, ChesClient>(client =>
            {
                client.BaseAddress = new Uri(PrimeEnvironment.ChesApi.Url.EnsureTrailingSlash());
            })
            .AddHttpMessageHandler<BearerTokenHandler<ChesClientCredentials>>();

            services.AddHttpClient<IAddressAutocompleteClient, AddressAutocompleteClient>(client =>
            {
                client.BaseAddress = new Uri(PrimeEnvironment.AddressAutocompleteApi.Url.EnsureTrailingSlash());
            });

            services.AddHttpClient<ILdapClient, LdapClient>(client =>
            {
                client.BaseAddress = new Uri(PrimeEnvironment.LdapApi.Url.EnsureTrailingSlash());
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
            app.UseCors(AllowSpecificOrigins);

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

            if (PrimeEnvironment.IsLocal)
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

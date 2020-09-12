using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Security.Claims;


using Prime.Auth.Requirements;
namespace Prime.Auth
{
    public static class AuthenticationSetup
    {
        public static void Initialize(
            IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            services.ThrowIfNull(nameof(services));
            configuration.ThrowIfNull(nameof(configuration));
            environment.ThrowIfNull(nameof(environment));

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                if (environment.IsDevelopment())
                {
                    IdentityModelEventSource.ShowPII = true;
                    options.RequireHttpsMetadata = false;
                }

                options.Audience = AuthConstants.ApiAudience;
                options.MetadataAddress = Environment.GetEnvironmentVariable("JWT_WELL_KNOWN_CONFIG") ?? configuration["Jwt:WellKnown"];
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();

                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        if (environment.IsDevelopment())
                        {
                            return c.Response.WriteAsync(c.Exception.ToString());
                        }
                        return c.Response.WriteAsync("An error occured processing your authentication.");
                    },
                    OnTokenValidated = async context => await OnTokenValidatedAsync(context)
                };
            });

            services.AddSingleton<IAuthorizationHandler, AdminUserTypeRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, EnrolleeUserTypeRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, SiteUserTypeRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, CanEditRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, AuthorizedPartyRequirementHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.EnrolleeOnly, policy => policy.AddRequirements(new UserTypeRequirement(UserType.Enrollee)));
                options.AddPolicy(Policies.AdminOnly, policy => policy.AddRequirements(new UserTypeRequirement(UserType.Admin)));
                options.AddPolicy(Policies.SiteRegistrantOnly, policy => policy.AddRequirements(new UserTypeRequirement(UserType.Site)));
                options.AddPolicy(Policies.EnrolleeOrAdmin, policy => policy.AddRequirements(new UserTypeRequirement(UserType.Enrollee, UserType.Admin)));
                options.AddPolicy(Policies.SiteRegistrantOrAdmin, policy => policy.AddRequirements(new UserTypeRequirement(UserType.Site, UserType.Admin)));
                options.AddPolicy(Policies.AnyUser, policy => policy.AddRequirements(new UserTypeRequirement(UserType.Enrollee, UserType.Site, UserType.Admin)));

                options.AddPolicy(Policies.CanEdit, policy => policy.AddRequirements(new CanEditRequirement()));

                // External Clientss
                options.AddPolicy(Policies.CareConnectAccess, policy => policy.AddRequirements(new AuthorizedPartyRequirement(AuthorizedParties.CareConnect)));
                options.AddPolicy(Policies.PosGpidAccess, policy => policy.AddRequirements(new AuthorizedPartyRequirement(AuthorizedParties.PosGpid)));
            });
        }

        private static Task OnTokenValidatedAsync(TokenValidatedContext context)
        {
            if (context.SecurityToken is JwtSecurityToken accessToken
                && context.Principal.Identity is ClaimsIdentity identity
                && identity.IsAuthenticated)
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, accessToken.Subject));

                AddRolesForRealmAccessClaims(identity);
            }

            return Task.CompletedTask;
        }

        private static void AddRolesForRealmAccessClaims(ClaimsIdentity identity)
        {
            // flatten realm_access because Microsoft identity model doesn't support nested claims
            var realmAccessClaim = identity.Claims.SingleOrDefault(claim => claim.Type == Claims.RealmAccess);

            if (realmAccessClaim != null)
            {
                var realmAccess = JsonConvert.DeserializeObject<RealmAccess>(realmAccessClaim.Value);

                foreach (var role in realmAccess.roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }
        }

        private class RealmAccess
        {
            public string[] roles { get; set; }
        }
    }
}

﻿using System;
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
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Prime.Infrastructure;

namespace Prime.Auth
{
    public static class AuthenticationSetup
    {
        public static void Initialize(
            IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment
            )
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

                options.Audience = AuthConstants.Audience;
                options.MetadataAddress = PrimeEnvironment.Keycloak.WellKnownConfig;
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

            services.AddSingleton<IAuthorizationHandler, PrimeUserAuthHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthConstants.USER_POLICY, policy => policy.Requirements.Add(new PrimeUserRequirement()));
                options.AddPolicy(AuthConstants.ADMIN_POLICY, policy => policy.RequireRole(AuthConstants.PRIME_ADMIN_ROLE));
                options.AddPolicy(AuthConstants.SUPER_ADMIN_POLICY, policy => policy.RequireRole(AuthConstants.PRIME_SUPER_ADMIN_ROLE));
                options.AddPolicy(AuthConstants.READONLY_ADMIN_POLICY, policy => policy.RequireRole(AuthConstants.PRIME_READONLY_ADMIN));
                options.AddPolicy(AuthConstants.EXTERNAL_HPDID_ACCESS_POLICY, policy => policy.RequireRole(AuthConstants.EXTERNAL_HPDID_ACCESS_ROLE));
                options.AddPolicy(AuthConstants.EXTERNAL_GPID_VALIDATION_POLICY, policy => policy.RequireRole(AuthConstants.EXTERNAL_GPID_VALIDATION_ROLE));
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
            if (identity.HasClaim((claim) => claim.Type == AuthConstants.KEYCLOAK_REALM_ACCESS_KEY))
            {
                var realmAccessClaim = identity.Claims.Single((claim) => claim.Type == AuthConstants.KEYCLOAK_REALM_ACCESS_KEY);
                var realmAccessAsDict = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(realmAccessClaim.Value);

                if (realmAccessAsDict.ContainsKey(AuthConstants.KEYCLOAK_ROLES_KEY))
                {
                    foreach (var role in realmAccessAsDict[AuthConstants.KEYCLOAK_ROLES_KEY])
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                }
            }
        }
    }
}

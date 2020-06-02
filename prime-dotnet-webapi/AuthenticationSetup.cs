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
            if (services is null)
            {
                throw new System.ArgumentNullException(nameof(services));
            }

            if (configuration is null)
            {
                throw new System.ArgumentNullException(nameof(configuration));
            }

            if (environment is null)
            {
                throw new System.ArgumentNullException(nameof(environment));
            }

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

                options.Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? configuration["Jwt:Audience"];
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

            services.AddSingleton<IAuthorizationHandler, PrimeUserAuthHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthConstants.USER_POLICY, policy => policy.Requirements.Add(new PrimeUserRequirement()));
                options.AddPolicy(AuthConstants.ADMIN_POLICY, policy => policy.RequireRole(AuthConstants.PRIME_ADMIN_ROLE));
                options.AddPolicy(AuthConstants.SUPER_ADMIN_POLICY, policy => policy.RequireRole(AuthConstants.PRIME_SUPER_ADMIN_ROLE));
                options.AddPolicy(AuthConstants.READONLY_ADMIN_POLICY, policy => policy.RequireRole(AuthConstants.PRIME_READONLY_ADMIN));
                options.AddPolicy(AuthConstants.EXTERNAL_HPDID_ACCESS_POLICY, policy => policy.RequireRole(AuthConstants.EXTERNAL_HPDID_ACCESS_ROLE));
            });
        }

        private static Task OnTokenValidatedAsync(TokenValidatedContext context)
        {
            if (context.SecurityToken is JwtSecurityToken accessToken
                    && context.Principal.Identity is ClaimsIdentity identity
                    && identity.IsAuthenticated)
            {
                // add the access token to the identity claims in case it is needed later
                identity.AddClaim(new Claim(AuthConstants.PRIME_ACCESS_TOKEN_KEY, accessToken.RawData));
                identity.AddClaim(new Claim(ClaimTypes.Name, accessToken.Subject));

                // flatten realm_access because Microsoft identity model doesn't support nested claims
                AddRolesForRealmAccessClaims(identity);

                // flatten resource_access because Microsoft identity model doesn't support nested claims
                AddRolesForResourceAccessClaims(identity);
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

        private static void AddRolesForResourceAccessClaims(ClaimsIdentity identity)
        {
            // flatten resource_access because Microsoft identity model doesn't support nested claims
            if (identity.HasClaim((claim) => claim.Type == AuthConstants.KEYCLOAK_RESOURCE_ACCESS_KEY))
            {
                var resourceAccessClaim = identity.Claims.Single((claim) => claim.Type == AuthConstants.KEYCLOAK_RESOURCE_ACCESS_KEY);
                var resourceAccessAsDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string[]>>>(resourceAccessClaim.Value);

                // get the roles from each potential client key that we care about
                foreach (var clientId in AuthConstants.PRIME_CLIENT_IDS)
                {
                    Dictionary<string, string[]> clientKeyValue;
                    if (resourceAccessAsDict.TryGetValue(clientId, out clientKeyValue)
                            && clientKeyValue.ContainsKey(AuthConstants.KEYCLOAK_ROLES_KEY))
                    {
                        foreach (var role in clientKeyValue[AuthConstants.KEYCLOAK_ROLES_KEY])
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, role));
                        }
                    }
                }
            }
        }
    }
}

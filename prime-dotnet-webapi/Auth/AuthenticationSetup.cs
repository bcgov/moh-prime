﻿using System.IdentityModel.Tokens.Jwt;
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
        }

        private static Task OnTokenValidatedAsync(TokenValidatedContext context)
        {
            if (context.SecurityToken is JwtSecurityToken accessToken
                    && context.Principal.Identity is ClaimsIdentity identity
                    && identity.IsAuthenticated)
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, accessToken.Subject));

                FlattenRealmAccessRoles(identity);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Flattens the Realm Access claim, as Microsoft Identity Model doesn't support nested claims
        /// </summary>
        private static void FlattenRealmAccessRoles(ClaimsIdentity identity)
        {
            var realmAccessClaim = identity.Claims
                .SingleOrDefault(claim => claim.Type == Claims.RealmAccess)
                ?.Value;

            if (realmAccessClaim != null)
            {
                var realmAccess = JsonConvert.DeserializeObject<RealmAccess>(realmAccessClaim);

                identity.AddClaims(realmAccess.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }
        }

        private class RealmAccess
        {
            public string[] Roles { get; set; }
        }
    }
}

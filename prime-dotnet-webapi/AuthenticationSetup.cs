using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace Prime
{
    public static class AuthenticationSetup
    {
        public static void Initialize(IServiceCollection services,
            IConfiguration configuration,
            IHostingEnvironment environment)
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
                    // options.Authority = configuration["Jwt:Authority"];
                    options.Audience = configuration["Jwt:Audience"];
                    options.MetadataAddress = configuration["Jwt:WellKnown"];
                    options.Events = new JwtBearerEvents()
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
                        OnTokenValidated = async context => await OnTokenValidated(context)
                    };
                });

            services.AddAuthorization(
                options =>
                {
                    options.AddPolicy(PrimeConstants.PRIME_ADMIN_POLICY, policy => policy.RequireClaim(PrimeConstants.PRIME_USER_ROLES_KEY, PrimeConstants.PRIME_ADMIN_ROLE));
                });
        }

        private static Task OnTokenValidated(TokenValidatedContext context)
        {
            if (context.SecurityToken is JwtSecurityToken accessToken
                    && context.Principal.Identity is ClaimsIdentity identity)
            {
                //add the access token to the identity claims in case it is needed later
                identity.AddClaim(new Claim("access_token", accessToken.RawData));
                identity.AddClaim(new Claim(PrimeConstants.PRIME_USER_ID_KEY, accessToken.Subject));

                //We could get the user roles from the resource_access claim, but it is a nested array of roles for each client
                //var resource_access = identity.Claims.FirstOrDefault(x => x.Type == "resource_access");

                //NOTE: these claims ('user_realm_roles' and 'user_roles') are being added to the token by a custom mapper in keycloak
                var realm_roles = identity.Claims.Where(x => x.Type == "user_realm_roles").Select(v => v.Value).ToList();
                foreach (var role in realm_roles)
                {
                    identity.AddClaim(new Claim(PrimeConstants.PRIME_USER_ROLES_KEY, role));
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
                var client_roles = identity.Claims.Where(x => x.Type == "user_roles").Select(v => v.Value).ToList();
                foreach (var role in client_roles)
                {
                    identity.AddClaim(new Claim(PrimeConstants.PRIME_USER_ROLES_KEY, role));
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }

            return Task.CompletedTask;
        }
    }
}

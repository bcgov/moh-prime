using System.Collections.Generic;
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
using Newtonsoft.Json;
using Prime.Infrastructure;

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
                    options.AddPolicy(PrimeConstants.PRIME_USER_POLICY, policy => policy.Requirements.Add(new PrimeUserRequirement()));
                    options.AddPolicy(PrimeConstants.PRIME_ADMIN_POLICY, policy => policy.RequireRole(PrimeConstants.PRIME_ADMIN_ROLE));
                });
        }

        private static Task OnTokenValidated(TokenValidatedContext context)
        {
            // add some constants for the KEYCLOAK access token keys
            const string REALM_ACCESS_KEY = "realm_access";
            const string RESOURCE_ACCESS_KEY = "resource_access";
            const string ROLES_KEY = "roles";

            if (context.SecurityToken is JwtSecurityToken accessToken
                    && context.Principal.Identity is ClaimsIdentity identity
                    && identity.IsAuthenticated)
            {
                // add the access token to the identity claims in case it is needed later
                identity.AddClaim(new Claim(PrimeConstants.PRIME_ACCESS_TOKEN_KEY, accessToken.RawData));
                identity.AddClaim(new Claim(ClaimTypes.Name, accessToken.Subject));

                // flatten realm_access because Microsoft identity model doesn't support nested claims
                if (identity.HasClaim((claim) => claim.Type == REALM_ACCESS_KEY))
                {
                    var realmAccessClaim = identity.Claims.Single((claim) => claim.Type == REALM_ACCESS_KEY);
                    var realmAccessAsDict = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(realmAccessClaim.Value);

                    if (realmAccessAsDict.ContainsKey(ROLES_KEY))
                    {
                        foreach (var role in realmAccessAsDict[ROLES_KEY])
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, role));
                        }
                    }
                }

                // flatten resource_access because Microsoft identity model doesn't support nested claims
                if (identity.HasClaim((claim) => claim.Type == RESOURCE_ACCESS_KEY))
                {
                    var resourceAccessClaim = identity.Claims.Single((claim) => claim.Type == RESOURCE_ACCESS_KEY);
                    var resourceAccessAsDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string[]>>>(resourceAccessClaim.Value);

                    // get the roles from each potential client key that we care about
                    foreach (var clientId in PrimeConstants.PRIME_CLIENT_IDS)
                    {
                        Dictionary<string, string[]> clientKeyValue;
                        if (resourceAccessAsDict.TryGetValue(clientId, out clientKeyValue)
                                && clientKeyValue.ContainsKey(ROLES_KEY))
                        {
                            foreach (var role in clientKeyValue[ROLES_KEY])
                            {
                                identity.AddClaim(new Claim(ClaimTypes.Role, role));
                            }
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}

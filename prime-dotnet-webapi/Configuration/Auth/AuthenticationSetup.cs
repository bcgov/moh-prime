using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Prime.Configuration.Auth
{
    public static class AuthenticationSetup
    {
        public static void Initialize(IServiceCollection services)
        {
            services.ThrowIfNull(nameof(services));

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(Schemes.PrimeJwt)
            .AddJwtBearer(Schemes.PrimeJwt, options =>
            {
                options.Authority = PrimeConfiguration.Current.PrimeKeycloak.RealmUrl;
                options.Audience = AuthConstants.Audience;
                options.MetadataAddress = PrimeConfiguration.Current.PrimeKeycloak.WellKnownConfig;
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context => await OnTokenValidatedAsync(context)
                };
            })
            .AddJwtBearer(Schemes.MohJwt, options =>
            {
                options.Authority = PrimeConfiguration.Current.MohKeycloak.RealmUrl;
                options.Audience = AuthConstants.Audience;
                options.MetadataAddress = PrimeConfiguration.Current.MohKeycloak.WellKnownConfig;
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context => await OnTokenValidatedAsync(context)
                };
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(Schemes.PrimeJwt)
                    .RequireAuthenticatedUser()
                    .Build();
            });
        }

        private static Task OnTokenValidatedAsync(TokenValidatedContext context)
        {
            if (context.SecurityToken is JwtSecurityToken accessToken
                    && context.Principal.Identity is ClaimsIdentity identity
                    && identity.IsAuthenticated)
            {
                // TODO: constant
                identity.AddClaim(new Claim(ClaimTypes.Name, (string)accessToken.Payload.GetValueOrDefault("preferred_username")));

                FlattenRealmAccessRoles(identity);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Flattens the Resource Access claim, as Microsoft Identity Model doesn't support nested claims
        /// </summary>
        private static void FlattenRealmAccessRoles(ClaimsIdentity identity)
        {
            var resourceAccessClaim = identity.Claims
                .SingleOrDefault(claim => claim.Type == Claims.ResourceAccess)
                ?.Value;
            string authorizedParty = identity.Claims
                .SingleOrDefault(claim => claim.Type == Claims.AuthorizedParty)
                ?.Value;

            if (resourceAccessClaim != null)
            {
                var clientsToRoles = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string[]>>>(resourceAccessClaim);
                Dictionary<string, string[]> rolesToRolesList = clientsToRoles.GetValueOrDefault(authorizedParty);
                string[] roles = rolesToRolesList.GetValueOrDefault("roles");
                identity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Prime.Auth
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

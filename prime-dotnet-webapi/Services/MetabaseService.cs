using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Flurl;

namespace Prime.Services
{
    public class MetabaseService : IMetabaseService
    {
        public MetabaseService()
        { }

        public string BuildMetabaseEmbeddedUrl()
        {
            var token = BuildMetabaseSecurityToken();

            return Url.Combine(PrimeEnvironment.MetabaseApi.Url, "embed/dashboard", token)
                .SetFragment("bordered=false&titled=false");
        }

        private string BuildMetabaseSecurityToken()
        {
            var payload = new JwtPayload
            {
               { "resource", new { dashboard = PrimeEnvironment.MetabaseApi.DashboardId } },
               { "exp", DateTimeOffset.Now.AddMinutes(10).ToUnixTimeSeconds() },
               { "params", new object() }
            };

            var secToken = new JwtSecurityToken(BuildJwtHeader(), payload);
            return new JwtSecurityTokenHandler().WriteToken(secToken);
        }

        private JwtHeader BuildJwtHeader()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(PrimeEnvironment.MetabaseApi.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            return new JwtHeader(credentials);
        }
    }
}

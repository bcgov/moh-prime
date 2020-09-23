using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.IO;

namespace Prime.Services
{
    public class MetabaseService : IMetabaseService
    {
        public MetabaseService()
        { }

        public string BuildMetabaseEmbeddedString()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(PrimeEnvironment.MetabaseApi.Key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(credentials);

            // 10 minutes in the future represented by milliseconds from epoch
            var expired = DateTimeOffset.Now.AddMinutes(10).ToUnixTimeSeconds();

            var payload = new JwtPayload
           {
               { "resource", new {dashboard = PrimeEnvironment.MetabaseApi.DashboardId} },
               { "exp",  expired},
               { "params", new object()}
           };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            var tokenString = handler.WriteToken(secToken);

            return Path.Join(PrimeEnvironment.MetabaseApi.Url, "embed/dashboard", tokenString, "#bordered=false&titled=false");
        }
    }
}

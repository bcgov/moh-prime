using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Collections.Generic;

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
            var expired = DateTime.Now.AddMinutes(10).Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;

            var payload = new JwtPayload
           {
               { "resource", new DashboardResource(4)},
               { "exp",  Math.Round(expired)},
               { "params", new object()}
           };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            var tokenString = handler.WriteToken(secToken);

            return Path.Join(PrimeEnvironment.MetabaseApi.Url, "embed/dashboard", tokenString, "#bordered=true&titled=true");
        }
    }
    public class DashboardResource
    {
        public int dashboard { get; set; }
        public DashboardResource(int _dashboard)
        {
            dashboard = _dashboard;
        }
    }
}

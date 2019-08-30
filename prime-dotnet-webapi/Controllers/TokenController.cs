using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using prime.Models;

namespace prime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public TokenController(ApiDbContext context)
        {
            _context = context;
        }

        // POST api/token
        [HttpPost]
        public ActionResult<object> Post(string token)
        {
            // TODO: verify google token

            var jwtToken = new JwtToken();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SIGNING_KEY"));

            var subject = new ClaimsIdentity(new Claim[]
            {
                    // TODO: add claims to our token.
            });
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var newToken = tokenHandler.CreateToken(tokenDescriptor);
            jwtToken.Token = tokenHandler.WriteToken(newToken);

            // TODO: return the token.
            return "this is a token";
        }
    }

    public class JwtToken
    {
        public string Token { get; set; }
    }
}

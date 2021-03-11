using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Services;
using Prime.Models.Api;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GisController : ControllerBase
    {
        private readonly IGisService _gisService;
        public GisController(IGisService gisService)
        {
            _gisService = gisService;
        }

        // POST: api/gis/ldap/login
        /// <summary>
        /// Login to ldap using username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        [HttpPost("ldap/login", Name = nameof(LdapLogin))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> LdapLogin(string username, string password)
        {
            var result = await _gisService.LdapLogin(username, password);

            return result
            ? Unauthorized()
            : Ok();
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Services;
using Prime.Models.Api;
using Prime.ViewModels.Parties;
using Prime.Models;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GisController : ControllerBase
    {
        private readonly IGisService _gisService;
        private readonly IPartyService _partyService;
        public GisController(
            IGisService gisService,
            IPartyService partyService)
        {
            _gisService = gisService;
            _partyService = partyService;
        }

        // POST: api/parties/gis
        /// <summary>
        /// Creates a new Gis Party
        /// </summary>
        [HttpPost(Name = nameof(CreateGisParty))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateGisParty(GisChangeModel changeModel)
        {
            if (changeModel == null)
            {
                ModelState.AddModelError("Party", "Could not create the Party, the passed in model cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            if (await _partyService.CreateOrUpdatePartyAsync(changeModel, User) == -1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error when saving the Party." });
            }

            return Ok();
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

            if (result == true)
            {
                return Ok();
            }

            return Unauthorized();
        }
    }
}

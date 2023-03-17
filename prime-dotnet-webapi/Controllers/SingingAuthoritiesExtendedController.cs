using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/parties/signing-authorities")]
    [ApiController]
    public class SigningAuthorityExtendedController : PrimeControllerBase
    {
        private readonly IPartyService _partyService;

        public SigningAuthorityExtendedController(
            IPartyService partyService)
        {
            _partyService = partyService;
        }

        // POST: api/SigningAuthority/UpdateHpdid
        /// <summary>
        /// Update party HPDID from keycloak - used to pre-conditioning database for MOH keycloak migration
        /// </summary>
        /// <param name="limit"></param>
        [HttpPost("UpdateHpdid", Name = nameof(UpdatePartyHpdid))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdatePartyHpdid([FromQuery] int limit = 1000)
        {
            var total = await _partyService.UpdatePartyHpdid(limit);

            return Ok($"{total} party records have been updated.");
        }
    }
}

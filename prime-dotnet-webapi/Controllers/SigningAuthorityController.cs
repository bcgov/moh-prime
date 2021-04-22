using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels.Parties;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
    public class SigningAuthorityController : ControllerBase
    {
        private readonly IPartyService _partyService;

        public SigningAuthorityController(
            IPartyService partyService)
        {
            _partyService = partyService;
        }

        // POST: api/SigningAuthorities
        /// <summary>
        /// Creates a new SigningAuthority.
        /// </summary>
        [HttpPost(Name = nameof(CreateSigningAuthority))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<SigningAuthorityChangeModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Organization>> CreateSigningAuthority(SigningAuthorityChangeModel signingAuthority)
        {
            if (signingAuthority == null)
            {
                ModelState.AddModelError("SigningAuthority", "Could not create a SigningAuthority on null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var createdSigningAuthorityId = await _partyService.CreateOrUpdatePartyAsync(signingAuthority, User);
            var createdSigningAuthority = await _partyService.GetPartyAsync(createdSigningAuthorityId);

            return CreatedAtAction(
                nameof(GetSigningAuthorityById),
                new { signingAuthorityId = createdSigningAuthorityId },
                ApiResponse.Result(createdSigningAuthority)
            );
        }

        // GET: api/SigningAuthorities/5
        /// <summary>
        /// Gets a specific SigningAuthority.
        /// </summary>
        /// <param name="signingAuthorityId"></param>
        [HttpGet("{signingAuthorityId}", Name = nameof(GetSigningAuthorityById))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SigningAuthorityChangeModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Organization>> GetSigningAuthorityById(int signingAuthorityId)
        {
            var signingAuthority = await _partyService.GetPartyAsync(signingAuthorityId);
            return Ok(ApiResponse.Result(signingAuthority));
        }

        // PUT: api/SigningAuthorities/5
        /// <summary>
        /// Updates a specific SigningAuthority.
        /// </summary>
        /// <param name="signingAuthorityId"></param>
        /// <param name="updatedSigningAuthority"></param>
        [HttpPut("{signingAuthorityId}", Name = nameof(UpdateSigningAuthority))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSigningAuthority(int signingAuthorityId, SigningAuthorityChangeModel updatedSigningAuthority)
        {
            if (!await _partyService.PartyExistsAsync(signingAuthorityId))
            {
                return NotFound(ApiResponse.Message($"SigningAuthority not found with id {signingAuthorityId}"));
            }

            await _partyService.CreateOrUpdatePartyAsync(updatedSigningAuthority, User);

            return NoContent();
        }
    }
}

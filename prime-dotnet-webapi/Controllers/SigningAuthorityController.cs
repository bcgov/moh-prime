using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
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

        // GET: api/SigningAuthority/5
        /// <summary>
        /// Gets a specific SigningAuthority.
        /// </summary>
        /// <param name="partyId"></param>
        [HttpGet("{partyId}", Name = nameof(GetSigningAuthorityById))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SigningAuthorityChangeModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<SigningAuthorityChangeModel>> GetSigningAuthorityById(int partyId)
        {
            var signingAuthority = await _partyService.GetPartyAsync(partyId);
            return Ok(ApiResponse.Result(signingAuthority));
        }

        // POST: api/SigningAuthority
        /// <summary>
        /// Creates a new SigningAuthority.
        /// </summary>
        [HttpPost(Name = nameof(CreateSigningAuthority))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<SigningAuthorityChangeModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult<SigningAuthorityChangeModel>> CreateSigningAuthority(SigningAuthorityChangeModel signingAuthority)
        {
            if (signingAuthority == null)
            {
                ModelState.AddModelError("SigningAuthority", "SigningAuthority can not be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var createdSigningAuthorityId = await _partyService.CreateOrUpdatePartyAsync(signingAuthority, User);
            var createdSigningAuthority = await _partyService.GetPartyAsync(createdSigningAuthorityId);

            return CreatedAtAction(
                nameof(GetSigningAuthorityById),
                new { partyId = createdSigningAuthorityId },
                ApiResponse.Result(createdSigningAuthority)
            );
        }

        // PUT: api/SigningAuthority/5
        /// <summary>
        /// Updates a specific SigningAuthority.
        /// </summary>
        /// <param name="partyId"></param>
        /// <param name="updatedSigningAuthority"></param>
        [HttpPut("{partyId}", Name = nameof(UpdateSigningAuthority))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSigningAuthority(int partyId, SigningAuthorityChangeModel updatedSigningAuthority)
        {
            if (!await _partyService.PartyExistsAsync(partyId))
            {
                return NotFound(ApiResponse.Message($"SigningAuthority not found with id {partyId}"));
            }

            await _partyService.CreateOrUpdatePartyAsync(updatedSigningAuthority, User);

            return NoContent();
        }
    }
}

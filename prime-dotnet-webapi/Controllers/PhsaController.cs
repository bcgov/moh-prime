using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models.Api;
using Prime.Services;
using Prime.HttpClients;
using Prime.ViewModels.Parties;
using Prime.Models;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/parties/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee)]
    public class PhsaController : ControllerBase
    {
        private readonly IPartyService _partyService;
        private readonly IKeycloakAdministrationClient _keycloakClient;

        public PhsaController(
            IPartyService partyService,
            IKeycloakAdministrationClient keycloakClient)
        {
            _partyService = partyService;
            _keycloakClient = keycloakClient;
        }

        // POST: api/parties/phsa
        /// <summary>
        /// Creates a new PHSA eForms Party of one or more types (currently Labtech and/or Immunizer).
        /// If successful, also updates Keycloak with additional user info and the relevant role(s).
        /// </summary>
        [HttpPost(Name = nameof(CreatePhsaParty))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreatePhsaParty(PhsaChangeModel changeModel)
        {
            if (changeModel == null)
            {
                ModelState.AddModelError("Party", "Could not create the Party, the passed in model cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var validPartyTypes = await _partyService.GetPreApprovedRegistrationsAsync(firstName: User.GetFirstName(), lastName: User.GetLastName(), email: changeModel.Email);

            if (!changeModel.Validate(validPartyTypes))
            {
                ModelState.AddModelError("Party", "Validation failed: Email and Phone Number are required, and at least one Pre-Approved PHSA Party Type must be specified.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            if (await _partyService.CreateOrUpdatePartyAsync(changeModel, User) == -1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error when saving the Party." });
            }

            if (!await _keycloakClient.UpdatePhsaUserInfo(User.GetPrimeUserId(), changeModel))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error when updating Keycloak." });
            }

            return Ok();
        }

        // GET: api/parties/phsa/pre-approved?email=example@example.com
        /// <summary>
        /// Returns a list of PHSA eForms registrations this User is pre-approved for.
        /// </summary>
        [HttpGet("pre-approved", Name = nameof(GetPreApprovedRegistrations))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<PartyType>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPreApprovedRegistrations([FromQuery] string email)
        {
            var partyTypes = await _partyService.GetPreApprovedRegistrationsAsync(firstName: User.GetFirstName(), lastName: User.GetLastName(), email: email);

            return Ok(ApiResponse.Result(partyTypes));
        }
    }
}

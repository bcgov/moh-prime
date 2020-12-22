using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models.Api;
using Prime.Services;
using Prime.HttpClients;
using Prime.ViewModels.Parties;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/parties/[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.User)]
    public class LabtechsController : ControllerBase
    {
        private readonly IPartyService _partyService;
        private readonly IKeycloakAdministrationClient _keycloakClient;

        public LabtechsController(
            IPartyService partyService,
            IKeycloakAdministrationClient keycloakClient)
        {
            _partyService = partyService;
            _keycloakClient = keycloakClient;
        }

        // POST: api/parties/labtechs
        /// <summary>
        /// Creates or updates a Labtech record.
        /// If successful, also updates Keycloak with additional user info and the Labtech role.
        /// </summary>
        [HttpPost(Name = nameof(CreateLabtech))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateLabtech(LabtechChangeModel labtech)
        {
            if (labtech == null)
            {
                ModelState.AddModelError("Labtech", "Could not create the Labtech, the passed in model cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }
            if (!labtech.IsValid())
            {
                ModelState.AddModelError("Labtech", "Email and Phone Number are required.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            await _partyService.CreateOrUpdatePartyAsync(labtech, User);

            await _keycloakClient.AssignRealmRole(User.GetPrimeUserId(), Roles.PhsaLabtech);
            await _keycloakClient.UpdateUserInfo(User.GetPrimeUserId(), email: labtech.Email, phoneNumber: labtech.Phone, phoneExtension: labtech.PhoneExtension);

            return Ok();
        }
    }
}

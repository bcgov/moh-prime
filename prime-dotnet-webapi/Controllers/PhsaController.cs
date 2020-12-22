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
    [Authorize(Policy = Policies.User)]
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
        /// Creates a new Labtech.
        /// If successful, also updates Keycloak with additional user info and the Labtech role.
        /// </summary>
        [HttpPost(Name = nameof(CreateLabtech))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateLabtech(LabtechChangeModel labtech)
        {
            await _partyService.CreateOrUpdatePartyAsync(labtech, User);

            await _keycloakClient.AssignRealmRole(User.GetPrimeUserId(), Roles.PhsaLabtech);
            await _keycloakClient.UpdateUserInfo(User.GetPrimeUserId(), email: labtech.Email, phoneNumber: labtech.Phone, phoneExtension: labtech.PhoneExtension);

            return Ok();
        }

        // GET: api/parties/phsa/pre-approved
        /// <summary>
        /// Returns a list of PHSA eForms registrations this person is pre-approved for.
        /// </summary>
        [HttpGet("pre-approved", Name = nameof(GetPreApprovedRegistrations))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<PartyType>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPreApprovedRegistrations([FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string email)
        {
            var partyTypes = await _partyService.GetPreApprovedRegistrationsAsync(firstName: firstName, lastName: lastName, email: email);

            return Ok(ApiResponse.Result(partyTypes));
        }
    }
}

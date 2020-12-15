using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels.Labtech;
using Prime.HttpClients;

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
        /// Creates a new Labtech.
        /// If successful, also updates Keycloak with additional user info and the Labtech role.
        /// </summary>
        [HttpPost(Name = nameof(CreateLabtech))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateLabtech(LabtechCreateModel labtech)
        {
            Party model = labtech.Create(User);
            var partyId = await _partyService.GetPartyIdForUserIdAsync(model.UserId);

            if (partyId == -1)
            {
                await _partyService.CreatePartyAsync(model, PartyType.Labtech);
            }
            else
            {
                await _partyService.UpdatePartyAsync(partyId, model, PartyType.Labtech);
            }

            await _keycloakClient.AssignRealmRole(model.UserId, Roles.PhsaLabtech);
            await _keycloakClient.UpdateUserInfo(model.UserId, email: model.Email, phoneNumber: model.Phone, phoneExtension: model.PhoneExtension);

            return Ok();
        }
    }
}

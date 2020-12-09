using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateLabtech(LabtechCreateModel labtech)
        {
            var model = Labtech.From(labtech, User);
            await _partyService.CreatePartyAsync(model);

            await _keycloakClient.AssignRealmRole(model.UserId, Roles.PhsaLabtech);
            await _keycloakClient.UpdateUserInfo(model.UserId, email: labtech.Email, phoneNumber: labtech.Phone, phoneExtension: labtech.PhoneExtension);

            return Ok();
        }
    }
}

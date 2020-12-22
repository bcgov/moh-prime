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
    public class PhsaController : ControllerBase
    {
        private readonly IPartyService _partyService;
        private readonly IKeycloakAdministrationClient _keycloakClient;
        private readonly IPreApprovedRegistrationService _preApprovedRegistrationService;

        public PhsaController(
            IPartyService partyService,
            IPreApprovedRegistrationService preApprovedRegistrationService,
            IKeycloakAdministrationClient keycloakClient)
        {
            _partyService = partyService;
            _preApprovedRegistrationService = preApprovedRegistrationService;
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
        public async Task<ActionResult> CreateLabtech(LabtechChangeModel labtech)
        {
            await _partyService.CreateOrUpdatePartyAsync(labtech, User);

            await _keycloakClient.AssignRealmRole(User.GetPrimeUserId(), Roles.PhsaLabtech);
            await _keycloakClient.UpdateUserInfo(User.GetPrimeUserId(), email: labtech.Email, phoneNumber: labtech.Phone, phoneExtension: labtech.PhoneExtension);

            return Ok();
        }

        // GET: api/...
        /// <summary>
        ///
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPreApprovedRegistrations()
        {
            throw new System.NotImplementedException();
        }
    }
}

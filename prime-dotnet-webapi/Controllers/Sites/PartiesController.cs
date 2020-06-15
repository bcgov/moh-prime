using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = AuthConstants.USER_POLICY, Roles = AuthConstants.FEATURE_SITE_REGISTRATION)]
    public class PartiesController : ControllerBase
    {
        private readonly IPartyService _partyService;

        public PartiesController(
            IPartyService partyService)
        {
            _partyService = partyService;
        }

        // PATCH: api/Party/5
        /// <summary>
        /// Updates a specific Party.
        /// </summary>
        /// <param name="partyId"></param>
        /// <param name="patchDoc"></param>
        [HttpPatch("{partyId}", Name = nameof(JsonPatchPartyWithModelState))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> JsonPatchPartyWithModelState(int partyId, [FromBody] JsonPatchDocument<Party> patchDoc)
        {
            if (patchDoc != null)
            {
                var party = await _partyService.GetPartyAsync(partyId);

                // Check if mailing address is being added
                var operations = patchDoc.Operations.FindAll(x => x.path.Contains("mailingAddress"));
                if (operations.Exists(x => x.op == "add") && party.MailingAddress == null)
                {
                    party.MailingAddress = new MailingAddress { };
                }

                patchDoc.ApplyTo(party, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _partyService.SavePatchPartyAsync(party);

                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }


}

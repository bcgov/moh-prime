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
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(
            ILocationService locationService)
        {
            _locationService = locationService;
        }

        // PATCH: api/Location/5
        /// <summary>
        /// Updates a specific Location.
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="patchDoc"></param>
        [HttpPatch("{locationId}", Name = nameof(JsonPatchLocationWithModelState))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> JsonPatchLocationWithModelState(int locationId, [FromBody] JsonPatchDocument<Location> patchDoc)
        {
            if (patchDoc != null)
            {
                var location = await _locationService.GetLocationAsync(locationId);

                // Check if physical address is being added
                var operations = patchDoc.Operations.FindAll(x => x.path.Contains("physicalAddress"));
                if (operations.Exists(x => x.op == "add") && location.PhysicalAddress == null)
                {
                    location.PhysicalAddress = new PhysicalAddress { };
                }

                patchDoc.ApplyTo(location, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _locationService.SavePatchLocationAsync(location);

                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }


}

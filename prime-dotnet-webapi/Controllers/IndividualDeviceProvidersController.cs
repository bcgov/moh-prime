using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prime.Configuration.Auth;
using Prime.Services;
using Prime.ViewModels;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/sites")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
    public class IndividualDeviceProvidersController : PrimeControllerBase
    {
        private readonly IIndividualDeviceProviderService _individualDeviceProviderService;
        private readonly ICommunitySiteService _communitySiteService;

        public IndividualDeviceProvidersController(
            IIndividualDeviceProviderService individualDeviceProviderService,
            ICommunitySiteService communitySiteService
        )
        {
            _individualDeviceProviderService = individualDeviceProviderService;
            _communitySiteService = communitySiteService;
        }

        // POST: api/sites/5/individual-device-providers
        /// <summary>
        ///     Creates an individual device provider
        /// </summary>
        [HttpPost("{communitySiteId}/individual-device-providers", Name = nameof(CreateProvider))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IndividualDeviceProviderViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateProvider(int communitySiteId, IndividualDeviceProviderCreateOrUpdateModel createModel)
        {
            var record = await _communitySiteService.GetPermissionsRecordAsync(communitySiteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {communitySiteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var provider = await _individualDeviceProviderService.CreateProviderAsync(communitySiteId, createModel);

            return Ok(provider);
        }

        // PUT: api/sites/5/individual-device-providers/1
        /// <summary>
        ///     Updates an individual device provider
        /// </summary>
        [HttpPut("{communitySiteId}/individual-device-providers/{providerId}", Name = nameof(UpdateProvider))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateProvider(int communitySiteId, int providerId, IndividualDeviceProviderCreateOrUpdateModel updateModel)
        {
            var record = await _communitySiteService.GetPermissionsRecordAsync(communitySiteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {communitySiteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }
            if (await _individualDeviceProviderService.ProviderExistsOnSiteAsync(communitySiteId, providerId))
            {
                return NotFound($"Individual Device Provider not found with id {communitySiteId}");
            }

            await _individualDeviceProviderService.UpdateProviderAsync(communitySiteId, updateModel);

            return NoContent();
        }

        // DELETE: api/sites/5/individual-device-providers/1
        /// <summary>
        ///     Deletes an individual device provider
        /// </summary>
        [HttpDelete("{communitySiteId}/individual-device-providers/{providerId}", Name = nameof(RemoveProvider))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> RemoveProvider(int communitySiteId, int providerId)
        {
            var record = await _communitySiteService.GetPermissionsRecordAsync(communitySiteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {communitySiteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }
            if (await _individualDeviceProviderService.ProviderExistsOnSiteAsync(communitySiteId, providerId))
            {
                return NotFound($"Individual Device Provider not found with id {communitySiteId}");
            }

            await _individualDeviceProviderService.RemoveProviderAsync(providerId);

            return NoContent();
        }

        // GET: api/sites/5/individual-device-providers
        /// <summary>
        ///     Gets individual device providers for a community site
        /// </summary>
        [HttpGet("{communitySiteId}/individual-device-providers", Name = nameof(GetProviders))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<IndividualDeviceProviderViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetProviders(int communitySiteId)
        {
            var record = await _communitySiteService.GetPermissionsRecordAsync(communitySiteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {communitySiteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var providers = await _individualDeviceProviderService.GetProvidersAsync(communitySiteId);

            return Ok(providers);
        }

    }
}

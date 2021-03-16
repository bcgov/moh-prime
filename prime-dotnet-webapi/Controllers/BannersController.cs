using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IBannerService _bannerService;
        private readonly IMetabaseService _metabaseService;

        public BannersController(IBannerService bannerService, IMetabaseService metabaseService)
        {
            _bannerService = bannerService;
            _metabaseService = metabaseService;
        }

        // POST: api/Banners
        /// <summary>
        /// Creates a new Banner.
        /// </summary>
        /// <param name="banner"></param>
        [HttpPost(Name = nameof(CreateBanner))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerAdminViewModel>), StatusCodes.Status201Created)]
        [Authorize(Roles = Roles.PrimeAdministrant)]
        public async Task<ActionResult<BannerAdminViewModel>> CreateBanner(Banner banner)
        {
            if (banner == null)
            {
                ModelState.AddModelError("Banner", "Could not create a Banner, the passed in Banner cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            await _bannerService.CreateBannerAsync(banner);

            return CreatedAtAction(
                nameof(GetBannerById),
                new { bannerId = banner.Id },
                ApiResponse.Result(banner)
            );
        }

        // PUT: api/Banners/1
        /// <summary>
        /// updates a new Banner.
        /// </summary>
        /// <param name="bannerId"></param>
        /// <param name="updateModel"></param>
        [HttpPut("{bannerId}", Name = nameof(UpdateBanner))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerAdminViewModel>), StatusCodes.Status200OK)]
        [Authorize(Roles = Roles.PrimeAdministrant)]
        public async Task<ActionResult<BannerAdminViewModel>> UpdateBanner(int bannerId, BannerUpdateViewModel updateModel)
        {
            var banner = await _bannerService.GetBannerAsync(bannerId);
            if (banner == null)
            {
                return NotFound(ApiResponse.Message($"No Banner found with id {bannerId}"));
            }

            var updatedBanner = await _bannerService.UpdateBannerAsync(bannerId, updateModel);
            return Ok(ApiResponse.Result(updatedBanner));
        }

        // DELETE: api/Banners/5
        /// <summary>
        /// Deletes a specific Banner.
        /// </summary>
        /// <param name="bannerId"></param>
        [HttpDelete("{bannerId}", Name = nameof(DeleteBanner))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerAdminViewModel>), StatusCodes.Status200OK)]
        [Authorize(Roles = Roles.PrimeAdministrant)]
        public async Task<ActionResult<BannerAdminViewModel>> DeleteBanner(int bannerId)
        {
            var banner = await _bannerService.GetBannerAsync(bannerId);
            if (banner == null)
            {
                return NotFound(ApiResponse.Message($"Banner not found with id {bannerId}"));
            }

            await _bannerService.RemoveBannerAsync(bannerId);

            return Ok(ApiResponse.Result(banner));
        }

        // GET: api/Banners
        /// <summary>
        /// Gets all the banners.
        /// </summary>
        [HttpGet(Name = nameof(GetBanners))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerAdminViewModel>), StatusCodes.Status200OK)]
        [Authorize(Roles = Roles.PrimeAdministrant)]
        public async Task<ActionResult<IEnumerable<BannerAdminViewModel>>> GetBanners()
        {
            var banners = await _bannerService.GetBannersAsync();
            return Ok(ApiResponse.Result(banners));
        }


        // GET: api/Banners/5
        /// <summary>
        /// Gets a specific Banner.
        /// </summary>
        /// <param name="bannerId"></param>
        [HttpGet("{bannerId}", Name = nameof(GetBannerById))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerAdminViewModel>), StatusCodes.Status200OK)]
        [Authorize(Roles = Roles.PrimeAdministrant)]
        public async Task<ActionResult<BannerAdminViewModel>> GetBannerById(int bannerId)
        {
            var banner = await _bannerService.GetBannerAsync(bannerId);
            if (banner == null)
            {
                return NotFound(ApiResponse.Message($"No Banner found with id {bannerId}"));
            }

            return Ok(ApiResponse.Result(banner));
        }

        // GET: api/Banners/active?locationCode=4
        /// <summary>
        /// Gets an active Banner by location code. Returns null result if no active banner
        /// </summary>
        /// <param name="locationCode"></param>
        [HttpGet("active", Name = nameof(GetBannerById))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BannerViewModel>> GetActiveBannerByLocationCode([FromQuery] BannerLocationCode locationCode)
        {
            var banner = await _bannerService.GetActiveBannerByLocationAsync(locationCode);
            return Ok(ApiResponse.Result(banner));
        }
    }
}

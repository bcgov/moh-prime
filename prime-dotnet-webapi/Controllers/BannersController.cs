using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class BannersController : PrimeControllerBase
    {
        private readonly IBannerService _bannerService;

        public BannersController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        // POST: api/banners?locationCode=1
        /// <summary>
        /// Create a banner
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="locationCode"></param>
        [HttpPost("", Name = nameof(CreateBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateBanner(BannerViewModel viewModel, [FromQuery] BannerLocationCode locationCode)
        {
            var banner = await _bannerService.CreateBannerAsync(locationCode, viewModel);
            return Ok(banner);
        }

        // POST: api/banners/enrolment-landing
        /// <summary>
        /// Create an Enrollee landing banner
        /// </summary>
        /// <param name="viewModel"></param>
        [HttpPost("enrolment-landing", Name = nameof(CreateEnrolleeLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateEnrolleeLandingBanner(BannerViewModel viewModel)
        {
            var banner = await _bannerService.CreateBannerAsync(BannerLocationCode.EnrolmentLandingPage, viewModel);
            return Ok(banner);
        }

        // POST: api/banners/site-landing
        /// <summary>
        /// Set the Site landing banner
        /// </summary>
        /// <param name="viewModel"></param>
        [HttpPost("site-landing", Name = nameof(CreateSiteLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateSiteLandingBanner(BannerViewModel viewModel)
        {
            var banner = await _bannerService.CreateBannerAsync(BannerLocationCode.SiteRegistrationLandingPage, viewModel);
            return Ok(banner);
        }

        // PUT: api/banners/1
        /// <summary>
        /// Update banner
        /// </summary>
        /// <param name="bannerId"></param>
        /// <param name="viewModel"></param>
        [HttpPut("{bannerId}", Name = nameof(UpdateBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateBanner(int bannerId, BannerViewModel viewModel)
        {
            var updatedBanner = await _bannerService.UpdateBannerAsync(bannerId, viewModel);
            return Ok(updatedBanner);
        }

        // DELETE: api/banners/1
        /// <summary>
        /// Delete a banner by id
        /// </summary>
        /// <param name="bannerId"></param>
        [HttpDelete("{bannerId}", Name = nameof(DeleteBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteBanner(int bannerId)
        {
            await _bannerService.DeleteBannerAsync(bannerId);
            return NoContent();
        }

        // GET: api/banners/1
        /// <summary>
        /// Get Banner by Id
        /// </summary>
        /// <param name="bannerId"></param>
        [HttpGet("{bannerId}", Name = nameof(GetBannerById))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBannerById(int bannerId)
        {
            var banner = await _bannerService.GetBannerAsync(bannerId);
            return Ok(banner);
        }

        // GET: api/banners?locationCode=1
        /// <summary>
        /// Get banners
        /// </summary>
        /// <param name="locationCode"></param>
        [HttpGet("", Name = nameof(GetBanners))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<BannerViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBanners([FromQuery] BannerLocationCode locationCode)
        {
            var banners = await _bannerService.GetBannersAsync(locationCode);
            return Ok(banners);
        }

        // GET: api/banners/enrolment-landing
        /// <summary>
        /// Get enrollee landing banners
        /// </summary>
        [HttpGet("enrolment-landing", Name = nameof(GetEnrolmentLandingBanners))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<BannerViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrolmentLandingBanners()
        {
            var banners = await _bannerService.GetBannersAsync(BannerLocationCode.EnrolmentLandingPage);
            return Ok(banners);
        }

        // GET: api/banners/site-landing
        /// <summary>
        /// Get Site Landing Banners
        /// </summary>
        [HttpGet("site-landing", Name = nameof(GetSiteLandingBanners))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<BannerViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSiteLandingBanners()
        {
            var banners = await _bannerService.GetBannersAsync(BannerLocationCode.SiteRegistrationLandingPage);
            return Ok(banners);
        }

        // GET: api/banners/active?locationCode=4
        /// <summary>
        /// Gets the active Banners by location code. Returns null result if no active banner
        /// </summary>
        /// <param name="locationCode"></param>
        [HttpGet("active", Name = nameof(GetActiveBannersByLocationCode))]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<BannerDisplayViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetActiveBannersByLocationCode([FromQuery] BannerLocationCode locationCode)
        {
            var banners = await _bannerService.GetActiveBannersAsync(locationCode, DateTime.UtcNow);
            return Ok(banners);
        }
    }
}

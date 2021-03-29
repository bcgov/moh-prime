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
    public class BannersController : ControllerBase
    {
        private readonly IBannerService _bannerService;

        public BannersController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        // PUT: api/Banners/enrolment-landing
        /// <summary>
        /// Create/update enrollee landing banner
        /// </summary>
        /// <param name="viewModel"></param>
        [HttpPut("enrolment-landing", Name = nameof(CreateOrUpdateEnrolleeLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateOrUpdateEnrolleeLandingBanner(BannerViewModel viewModel)
        {
            var updatedBanner = await _bannerService.CreateOrUpdateBannerAsync(BannerLocationCode.EnrolmentLandingPage, viewModel);
            return Ok(ApiResponse.Result(updatedBanner));
        }

        // PUT: api/Banners/site-landing
        /// <summary>
        /// Create/update site landing banner
        /// </summary>
        /// <param name="viewModel"></param>
        [HttpPut("site-landing", Name = nameof(CreateOrUpdateSiteLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateOrUpdateSiteLandingBanner(BannerViewModel viewModel)
        {
            var updatedBanner = await _bannerService.CreateOrUpdateBannerAsync(BannerLocationCode.SiteRegistrationLandingPage, viewModel);
            return Ok(ApiResponse.Result(updatedBanner));
        }

        // GET: api/Banners/enrolment-landing
        /// <summary>
        /// Get enrollee landing banner
        /// </summary>
        [HttpGet("enrolment-landing", Name = nameof(GetEnrolleeLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrolleeLandingBanner()
        {
            var banner = await _bannerService.GetBannerByLocationAsync(BannerLocationCode.EnrolmentLandingPage);
            return Ok(ApiResponse.Result(banner));
        }

        // GET: api/Banners/site-landing
        /// <summary>
        /// Get site landing banner
        /// </summary>
        [HttpGet("site-landing", Name = nameof(GetSiteLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSiteLandingBanner()
        {
            var banner = await _bannerService.GetBannerByLocationAsync(BannerLocationCode.SiteRegistrationLandingPage);
            return Ok(ApiResponse.Result(banner));
        }

        // DELETE: api/Banners/enrolment-landing
        /// <summary>
        /// Delete enrollee landing banner
        /// </summary>
        [HttpDelete("enrolment-landing", Name = nameof(DeleteEnrolleeLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteEnrolleeLandingBanner()
        {
            await _bannerService.RemoveBannerByLocationAsync(BannerLocationCode.EnrolmentLandingPage);
            return Ok();
        }

        // DELETE: api/Banners/site-landing
        /// <summary>
        /// Delete site landing banner
        /// </summary>
        [HttpDelete("site-landing", Name = nameof(DeleteSiteLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteSiteLandingBanner()
        {
            await _bannerService.RemoveBannerByLocationAsync(BannerLocationCode.SiteRegistrationLandingPage);
            return Ok();
        }

        // GET: api/Banners/active?locationCode=4
        /// <summary>
        /// Gets an active Banner by location code. Returns null result if no active banner
        /// </summary>
        /// <param name="locationCode"></param>
        [HttpGet("active", Name = nameof(GetActiveBannerByLocationCode))]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerDisplayViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetActiveBannerByLocationCode([FromQuery] BannerLocationCode locationCode)
        {
            var banner = await _bannerService.GetActiveBannerByLocationAsync(locationCode);
            return Ok(ApiResponse.Result(banner));
        }
    }
}

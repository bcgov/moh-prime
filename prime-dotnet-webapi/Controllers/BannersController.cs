using System;
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

        // PUT: api/banners/enrolment-landing
        /// <summary>
        /// Set the Enrollee landing banner
        /// </summary>
        /// <param name="viewModel"></param>
        [HttpPut("enrolment-landing", Name = nameof(SetEnrolleeLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> SetEnrolleeLandingBanner(BannerViewModel viewModel)
        {
            var updatedBanner = await _bannerService.SetBannerAsync(BannerLocationCode.EnrolmentLandingPage, viewModel);
            return Ok(ApiResponse.Result(updatedBanner));
        }

        // PUT: api/banners/site-landing
        /// <summary>
        /// Set the Site landing banner
        /// </summary>
        /// <param name="viewModel"></param>
        [HttpPut("site-landing", Name = nameof(SetSiteLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> SetSiteLandingBanner(BannerViewModel viewModel)
        {
            var updatedBanner = await _bannerService.SetBannerAsync(BannerLocationCode.SiteRegistrationLandingPage, viewModel);
            return Ok(ApiResponse.Result(updatedBanner));
        }

        // GET: api/banners/enrolment-landing
        /// <summary>
        /// Get the Enrollee landing banner
        /// </summary>
        [HttpGet("enrolment-landing", Name = nameof(GetEnrolleeLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrolleeLandingBanner()
        {
            var banner = await _bannerService.GetBannerAsync(BannerLocationCode.EnrolmentLandingPage);
            return Ok(ApiResponse.Result(banner));
        }

        // GET: api/banners/site-landing
        /// <summary>
        /// Get the Site landing banner
        /// </summary>
        [HttpGet("site-landing", Name = nameof(GetSiteLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSiteLandingBanner()
        {
            var banner = await _bannerService.GetBannerAsync(BannerLocationCode.SiteRegistrationLandingPage);
            return Ok(ApiResponse.Result(banner));
        }

        // DELETE: api/banners/enrolment-landing
        /// <summary>
        /// Delete the Enrollee landing banner
        /// </summary>
        [HttpDelete("enrolment-landing", Name = nameof(DeleteEnrolleeLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteEnrolleeLandingBanner()
        {
            await _bannerService.DeleteBannerAsync(BannerLocationCode.EnrolmentLandingPage);
            return NoContent();
        }

        // DELETE: api/banners/site-landing
        /// <summary>
        /// Delete the Site landing banner
        /// </summary>
        [HttpDelete("site-landing", Name = nameof(DeleteSiteLandingBanner))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteSiteLandingBanner()
        {
            await _bannerService.DeleteBannerAsync(BannerLocationCode.SiteRegistrationLandingPage);
            return NoContent();
        }

        // GET: api/banners/active?locationCode=4
        /// <summary>
        /// Gets the active Banner by location code. Returns null result if no active banner
        /// </summary>
        /// <param name="locationCode"></param>
        [HttpGet("active", Name = nameof(GetActiveBannerByLocationCode))]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<BannerDisplayViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetActiveBannerByLocationCode([FromQuery] BannerLocationCode locationCode)
        {
            var banner = await _bannerService.GetActiveBannerAsync(locationCode, DateTime.UtcNow);
            return Ok(ApiResponse.Result(banner));
        }
    }
}

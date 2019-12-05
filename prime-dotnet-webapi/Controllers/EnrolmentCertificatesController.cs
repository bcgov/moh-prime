using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Models;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/enrolment-certificates")]
    [ApiController]
    // User needs at least the ADMIN or ENROLLEE role to use this controller
    [Authorize(Policy = PrimeConstants.PRIME_USER_POLICY)]
    public class EnrolmentCertificatesController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IEnrolmentCertificateService _certificateService;

        public EnrolmentCertificatesController(IEnrolleeService enrolleeService, IEnrolmentCertificateService enrolmentCertificateService)
        {
            _enrolleeService = enrolleeService;
            _certificateService = enrolmentCertificateService;
        }

        // GET: api/enrolment-certificates/certificate/{guid}
        /// <summary>
        /// Gets the Enrolment Certificate based on the supplied Access Token GUID. This endpoint is not authenticated.
        /// </summary>
        [HttpGet("certificate/{accessTokenId}", Name = nameof(GetEnrolmentCertificate))]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<EnrolmentCertificate>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<EnrolmentCertificate>> GetEnrolmentCertificate(Guid accessTokenId)
        {
            var certificate = await _certificateService.GetEnrolmentCertificateAsync(accessTokenId);
            if (certificate == null)
            {
                return NotFound(new ApiResponse(404, $"No valid Enrolment Certificate Access Token found with id {accessTokenId}"));
            }

            // TODO: Access controls based on vewcount, passphrase, etc.

            return Ok(new ApiOkResponse<EnrolmentCertificate>(certificate));
        }


        // GET: api/enrolment-certificates/access
        /// <summary>
        /// Gets all of the access tokens for the user.
        /// </summary>
        [HttpGet("access", Name = nameof(GetAccessTokens))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<EnrolmentCertificateAccessToken>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolmentCertificateAccessToken>>> GetAccessTokens()
        {
            var tokens = await _certificateService.GetCertificateAccessTokensForUserIdAsync(User.GetPrimeUserId());

            return Ok(new ApiOkResponse<IEnumerable<EnrolmentCertificateAccessToken>>(tokens));
        }


        // POST: api/enrolment-certificates/access
        /// <summary>
        /// Creates an EnrolmentCertificateAccessToken for the user if the user has a finished enrolment.
        /// </summary>
        [HttpPost("access", Name = nameof(CreateEnrolmentCertificateAccessToken))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiOkResponse<EnrolmentCertificateAccessToken>), StatusCodes.Status201Created)]
        public async Task<ActionResult<EnrolmentCertificateAccessToken>> CreateEnrolmentCertificateAccessToken()
        {
            var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(PrimeUtils.PrimeUserId(User));
            if (enrollee == null)
            {
                this.ModelState.AddModelError("Enrollee.UserId", "No enrollee exists for this User Id.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }
            if (enrollee.CurrentStatus?.Status.Code != Status.ACCEPTED_TOS_CODE)
            {
                this.ModelState.AddModelError("Enrollee.UserId", "The enrollee for this User Id is not in a finished state.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var createdToken = await _certificateService.CreateCertificateAccessTokenAsync(enrollee);

            return CreatedAtAction(nameof(GetEnrolmentCertificate), new { accessTokenId = createdToken.Id }, new ApiCreatedResponse<EnrolmentCertificateAccessToken>(createdToken));
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Models;
using Prime.Models.Api;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/provisioner-access")]
    [ApiController]
    public class ProvisionerAccessController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IEnrolmentCertificateService _certificateService;
        private readonly IEmailService _emailService;

        public ProvisionerAccessController(IEnrolleeService enrolleeService, IEnrolmentCertificateService enrolmentCertificateService, IEmailService emailService)
        {
            _enrolleeService = enrolleeService;
            _certificateService = enrolmentCertificateService;
            _emailService = emailService;
        }

        // GET: api/provisioner-access/certificate/{guid}
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

            return Ok(new ApiOkResponse<EnrolmentCertificate>(certificate));
        }

        // GET: api/provisioner-access/token
        /// <summary>
        /// Gets all of the access tokens for the user.
        /// </summary>
        [HttpGet("token", Name = nameof(GetAccessTokens))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<EnrolmentCertificateAccessToken>>), StatusCodes.Status200OK)]
        [Authorize(Policy = PrimeConstants.USER_POLICY)]
        public async Task<ActionResult<IEnumerable<EnrolmentCertificateAccessToken>>> GetAccessTokens()
        {
            var tokens = await _certificateService.GetCertificateAccessTokensForUserIdAsync(User.GetPrimeUserId());

            return Ok(new ApiOkResponse<IEnumerable<EnrolmentCertificateAccessToken>>(tokens));
        }

        // POST: api/provisioner-access/send-link
        /// <summary>
        /// Creates an EnrolmentCertificateAccessToken for the user if the user has a finished enrolment,
        /// then sends the link to a recipient by email.
        /// </summary>
        [HttpPost("send-link/{provisionerName}", Name = nameof(SendProvisionerLink))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiOkResponse<EnrolmentCertificateAccessToken>), StatusCodes.Status201Created)]
        // [Authorize(Policy = PrimeConstants.USER_POLICY)]
        public async Task<ActionResult<EnrolmentCertificateAccessToken>> SendProvisionerLink(string provisionerName, FromBodyText ccEmail)
        {
            if (!string.IsNullOrEmpty(ccEmail) && !EmailService.IsValidEmail(ccEmail))
            {
                this.ModelState.AddModelError("Contact Email", "The contact email provided is not valid.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(User.GetPrimeUserId());
            if (enrollee == null)
            {
                this.ModelState.AddModelError("Enrollee.UserId", "No enrollee exists for this User Id.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }
            if (enrollee.ExpiryDate == null)
            {
                this.ModelState.AddModelError("Enrollee.UserId", "The enrollee for this User Id is not in a finished state.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var createdToken = await _certificateService.CreateCertificateAccessTokenAsync(enrollee);
            var recipientEmail = _certificateService.GetPharmaNetProvisionerEmail(provisionerName);

            if (!EmailService.IsValidEmail(recipientEmail))
            {
                this.ModelState.AddModelError("Recipient Email", "The recipient email provided is not valid.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            await _emailService.SendProvisionerLinkAsync(recipientEmail, createdToken, ccEmail);

            return CreatedAtAction(
                nameof(GetEnrolmentCertificate),
                new { accessTokenId = createdToken.Id },
                new ApiCreatedResponse<EnrolmentCertificateAccessToken>(createdToken)
            );
        }

        // GET: api/provisioner-access/gpid
        /// <summary>
        /// Gets the GPID for the user. Only a valid token is required, no role is required.
        /// </summary>
        [HttpGet("gpid", Name = nameof(GetGpid))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiOkResponse<string>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<ActionResult<string>> GetGpid()
        {
            var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(User.GetPrimeUserId());

            return Ok(new ApiOkResponse<string>(enrollee?.GPID));
        }
    }
}

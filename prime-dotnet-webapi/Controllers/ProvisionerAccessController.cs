using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
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
        private readonly IBusinessEventService _businessEventService;

        public ProvisionerAccessController(
            IEnrolleeService enrolleeService,
            IEnrolmentCertificateService enrolmentCertificateService,
            IEmailService emailService,
            IBusinessEventService businessEventService)
        {
            _enrolleeService = enrolleeService;
            _certificateService = enrolmentCertificateService;
            _emailService = emailService;
            _businessEventService = businessEventService;
        }

        // GET: api/provisioner-access/certificate/{guid}
        /// <summary>
        /// Gets the Enrolment Certificate based on the supplied Access Token GUID. This endpoint is not authenticated.
        /// </summary>
        [HttpGet("certificate/{accessTokenId}", Name = nameof(GetEnrolmentCertificate))]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolmentCertificate>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolmentCertificate>> GetEnrolmentCertificate(Guid accessTokenId)
        {
            var certificate = await _certificateService.GetEnrolmentCertificateAsync(accessTokenId);
            if (certificate == null)
            {
                return NotFound(ApiResponse.Message($"No valid Enrolment Certificate Access Token found with id {accessTokenId}"));
            }

            return Ok(ApiResponse.Result(certificate));
        }

        // GET: api/provisioner-access/token
        /// <summary>
        /// Gets all of the access tokens for the user.
        /// </summary>
        [HttpGet("token", Name = nameof(GetAccessTokens))]
        [Authorize(Policy = AuthConstants.USER_POLICY)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<EnrolmentCertificateAccessToken>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolmentCertificateAccessToken>>> GetAccessTokens()
        {
            var tokens = await _certificateService.GetCertificateAccessTokensForUserIdAsync(User.GetPrimeUserId());

            return Ok(ApiResponse.Result(tokens));
        }

        // POST: api/provisioner-access/send-link
        /// <summary>
        /// Creates an EnrolmentCertificateAccessToken for the user if the user has a finished enrolment,
        /// then sends the link to a recipient by email.
        /// </summary>
        [HttpPost("send-link/{provisionerName}", Name = nameof(SendProvisionerLink))]
        [Authorize(Policy = AuthConstants.USER_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolmentCertificateAccessToken>), StatusCodes.Status201Created)]
        public async Task<ActionResult<EnrolmentCertificateAccessToken>> SendProvisionerLink(string provisionerName, FromBodyText providedEmails)
        {
            var provisionerNames = _certificateService.GetPharmaNetProvisionerNames();
            if (!provisionerNames.Contains(provisionerName) && provisionerName != "Other")
            {
                this.ModelState.AddModelError("Provisioner", "The provisioner provided is not valid.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            string providedEmailsTemp = (string)providedEmails;
            string[] emails = (!string.IsNullOrEmpty(providedEmailsTemp))
                ? (providedEmailsTemp).Split(",")
                : new string[] { };

            // Emails are either "Other" provisioners, or office manager(s)
            if (emails.Count() > 0 && !EmailService.AreValidEmails(emails))
            {
                this.ModelState.AddModelError("Email(s)", "The email(s) provided are not valid.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }
            if (provisionerName == "Other" && emails.Count() > 1)
            {
                this.ModelState.AddModelError("Email", "Other provisioners can only provide a single email address.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(User.GetPrimeUserId());
            if (enrollee == null)
            {
                this.ModelState.AddModelError("Enrollee.UserId", "No enrollee exists for this User Id.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }
            if (enrollee.ExpiryDate == null)
            {
                this.ModelState.AddModelError("Enrollee.UserId", "The enrollee for this User Id is not in a finished state.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            var createdToken = await _certificateService.CreateCertificateAccessTokenAsync(enrollee);

            // Only a few provisioners want emails sent directly, otherwise sent only to managers
            if (provisionerName == "iClinic" || provisionerName == "MediNet" || provisionerName == "Other")
            {
                var provisionerEmail = (provisionerName != "Other")
                    ? _certificateService.GetPharmaNetProvisionerEmail(provisionerName)
                    : emails[0];

                emails = new[] { provisionerEmail };
            }
            else
            {
                provisionerName = null;
            }

            await _emailService.SendProvisionerLinkAsync(emails, createdToken, provisionerName);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, "Provisioner link sent to email(s): " + string.Join(",", emails));

            return CreatedAtAction(
                nameof(GetEnrolmentCertificate),
                new { accessTokenId = createdToken.Id },
                ApiResponse.Result(createdToken)
            );
        }

        // GET: api/provisioner-access/gpid
        /// <summary>
        /// Gets the GPID for the user. Only a valid token is required, no role is required.
        /// </summary>
        [HttpGet("gpid", Name = nameof(GetGpid))]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetGpid()
        {
            var enrollee = await _enrolleeService.GetEnrolleeForUserIdNotDeclinedAsync(User.GetPrimeUserId());

            return Ok(ApiResponse.Result(enrollee?.GPID));
        }

        // GET: api/provisioner-access/gpids?hpdid=11111&hpdid=22222
        /// <summary>
        /// Gets the GPID and renewal date for the user(s) with the provided HPDIDs (if they exist). Requires a valid direct access grant token.
        /// </summary>
        [HttpGet("gpids", Name = nameof(HpdidLookup))]
        [Authorize(Policy = AuthConstants.EXTERNAL_HPDID_ACCESS_POLICY)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HpdidLookup>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> HpdidLookup([FromQuery] string[] hpdid)
        {
            var result = await _enrolleeService.HpdidLookupAsync(hpdid);

            return Ok(ApiResponse.Result(result));
        }
    }
}

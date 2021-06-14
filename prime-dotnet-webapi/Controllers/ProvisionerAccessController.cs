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
using Prime.HttpClients.Mail;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/provisioner-access")]
    [ApiController]
    public class ProvisionerAccessController : PrimeControllerBase
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
                return NotFound($"No valid Enrolment Certificate Access Token found with id {accessTokenId}");
            }

            return Ok(certificate);
        }

        // GET: api/provisioner-access/token
        /// <summary>
        /// Gets all of the access tokens for the user.
        /// </summary>
        [HttpGet("token", Name = nameof(GetAccessTokens))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<EnrolmentCertificateAccessToken>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolmentCertificateAccessToken>>> GetAccessTokens()
        {
            var tokens = await _certificateService.GetCertificateAccessTokensForUserIdAsync(User.GetPrimeUserId());

            return Ok(tokens);
        }

        // POST: api/provisioner-access/send-link/1
        /// <summary>
        /// Creates an EnrolmentCertificateAccessToken for the user if the user has a finished enrolment,
        /// then sends the link to a recipient by email based on Care Setting Code.
        /// </summary>
        /// <param name="careSettingCode"></param>
        /// <param name="providedEmails"></param>
        [HttpPost("send-link/{careSettingCode}", Name = nameof(SendProvisionerLink))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolmentCertificateAccessToken>), StatusCodes.Status201Created)]
        public async Task<ActionResult<EnrolmentCertificateAccessToken>> SendProvisionerLink(int careSettingCode, FromBodyText providedEmails)
        {
            var emails = Email.ParseCommaSeparatedEmails(providedEmails);
            if (!emails.Any())
            {
                return BadRequest("The email(s) provided are not valid.");
            }

            var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(User.GetPrimeUserId());
            if (enrollee == null)
            {
                return BadRequest("No enrollee exists for this User Id.");
            }
            if (enrollee.ExpiryDate == null)
            {
                return BadRequest("The enrollee for this User Id is not in a finished state.");
            }
            if (!enrollee.CurrentStatus.IsType(StatusType.Editable))
            {
                return BadRequest("The enrollee for this User Id is not in an editable state.");
            }
            var createdToken = await _certificateService.CreateCertificateAccessTokenAsync(enrollee.Id);

            await _emailService.SendProvisionerLinkAsync(emails, createdToken, careSettingCode);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, $"Provisioner link sent to email(s): {string.Join(",", emails)}");

            return CreatedAtAction(
                nameof(GetEnrolmentCertificate),
                new { accessTokenId = createdToken.Id },
                createdToken
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
            var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(User.GetPrimeUserId(), true);

            return Ok(enrollee?.GPID);
        }

        // GET: api/provisioner-access/gpids?hpdids=11111&hpdids=22222
        /// <summary>
        /// Gets the GPID and renewal date for the user(s) with the provided HPDIDs (if they exist). Requires a valid direct access grant token.
        /// </summary>
        [HttpGet("gpids", Name = nameof(HpdidLookup))]
        [Authorize(Roles = Roles.ExternalHpdidAccess)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HpdidLookup>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> HpdidLookup([FromQuery] string[] hpdids)
        {
            var result = await _enrolleeService.HpdidLookupAsync(hpdids);

            return Ok(result);
        }

        // POST: api/provisioner-access/gpids/123456789/validate
        /// <summary>
        /// Validates the supplied information against the enrollee record with the given GPID. Requires a valid direct access grant token.
        /// </summary>
        [HttpPost("gpids/{gpid}/validate", Name = nameof(ValidateGpid))]
        [Authorize(Roles = Roles.ExternalGpidValidation)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<GpidValidationResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<GpidValidationResponse>> ValidateGpid(string gpid, GpidValidationParameters parameters)
        {
            if (parameters == null)
            {
                return BadRequest(ApiResponse.Message($"Must supply validation parameters"));
            }

            var response = await _enrolleeService.ValidateProvisionerDataAsync(gpid, parameters);

            if (response == null)
            {
                return NotFound($"Enrollee not found with GPID {gpid}");
            }

            return Ok(response);
        }
    }
}

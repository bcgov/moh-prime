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
    // User needs at least the ADMIN or ENROLMENT role to use this controller
    [Authorize(Policy = PrimeConstants.PRIME_USER_POLICY)]
    public class EnrolmentCertificatesController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IEnrolmentService _enrolmentService;
        private readonly IEnrolmentCertificateService _certificateService;

        public EnrolmentCertificatesController(IEnrolleeService enrolleeService, IEnrolmentService enrolmentService, IEnrolmentCertificateService enrolmentCertificateService)
        {
            _enrolleeService = enrolleeService;
            _enrolmentService = enrolmentService;
            _certificateService = enrolmentCertificateService;
        }

        // GET: api/enrolment-certificates/{guid}
        /// <summary>
        /// Gets the Enrolment Certificate based on the supplied Access Token GUID. This endpoint is not authenticated.
        /// </summary>
        [HttpGet("{tokenId}", Name = nameof(GetEnrolmentCertificate))]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<EnrolmentCertificate>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<EnrolmentCertificate>> GetEnrolmentCertificate(Guid accessTokenId)
        {
            var token = await _certificateService.GetCertificateAccessTokenAsync(new Guid("fa490742-6648-460c-84ae-b49e63fed257"));
            if (token == null)
            {
                return NotFound(new ApiResponse(404, $"Enrolment Certificate Access Token not found with id {accessTokenId}"));
            }

            var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(token.UserId);
            if (enrollee == null)
            {
                return NotFound(new ApiResponse(404, $"No corresponding Enrollee found"));
            }

            // TODO: Access controls, increment token

            return Ok(new ApiOkResponse<EnrolmentCertificate>(EnrolmentCertificate.Create(enrollee)));
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
            var tokens = await _certificateService.GetCertificateAccessTokensForUserIdAsync(PrimeUtils.PrimeUserId(User));

            return Ok(new ApiOkResponse<IEnumerable<EnrolmentCertificateAccessToken>>(tokens));
        }


        // GET: api/enrolment-certificates/access/{guid}
        /// <summary>
        /// Gets the access token for the given GUID.
        /// </summary>
        [HttpGet("access/{tokenId}", Name = nameof(GetAccessToken))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<EnrolmentCertificateAccessToken>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolmentCertificateAccessToken>> GetAccessToken(Guid tokenId)
        {
            var token = await _certificateService.GetCertificateAccessTokenAsync(tokenId);
            if (token == null)
            {
                return NotFound(new ApiResponse(404, $"Enrolment Certificate Access Token not found with id {tokenId}"));
            }

            return Ok(new ApiOkResponse<EnrolmentCertificateAccessToken>(token));
        }


        // POST: api/enrolment-certificates/access
        /// <summary>
        /// Creates an EnrolmentCertificateAccessToken for the user if the user has a finished Enrolment.
        /// </summary>
        [HttpPost("access", Name = nameof(CreateEnrolmentCertificateAccessToken))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiOkResponse<EnrolmentCertificateAccessToken>), StatusCodes.Status201Created)]
        public async Task<ActionResult<EnrolmentCertificateAccessToken>> CreateEnrolmentCertificateAccessToken(/*Guid userId*/)
        {
            // if (enrollee == null)
            // {
            //     this.ModelState.AddModelError("Enrollee", "Could not create an access ticket, the passed in Enrollee cannot be null.");
            //     return BadRequest(new ApiBadRequestResponse(this.ModelState));
            // }

            var enrolment = await _enrolmentService.GetEnrolmentForUserIdAsync(new Guid("b9eba1ef-f323-4bb1-9ee8-0faf9b0277b4"));

            if (enrolment == null)
            {
                this.ModelState.AddModelError("Enrollee.UserId", "No enrolment exists for this User Id.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }
            if (enrolment.CurrentStatus?.Status.Code != Status.ACCEPTED_TOS_CODE)
            {
                this.ModelState.AddModelError("Enrollee.UserId", "The enrolment for this User Id is not in a finished state.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var createdToken = await _certificateService.CreateCertificateAccessTokenAsync(enrolment.Enrollee.UserId);

            return CreatedAtAction(nameof(GetAccessToken), new { tokenId = createdToken.Id }, new ApiCreatedResponse<EnrolmentCertificateAccessToken>(createdToken));
        }
    }
}

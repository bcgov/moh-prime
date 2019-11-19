using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Models;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    // User needs at least the ADMIN or ENROLMENT role to use this controller
    [Authorize(Policy = PrimeConstants.PRIME_USER_POLICY)]
    public class EnrolmentCertificateController : ControllerBase
    {
        private readonly IEnrolmentService _enrolmentService;
        private readonly IEnrolmentCertificateAccessService _certificateAccessService;

        public EnrolmentCertificateController(IEnrolmentService enrolmentService, IEnrolmentCertificateAccessService enrolmentCertificateAccessService)
        {
            _enrolmentService = enrolmentService;
            _certificateAccessService = enrolmentCertificateAccessService;
        }

        // POST: api/enrolmentCertificate/access
        /// <summary>
        /// Creates an EnrolmentCertificateAccessToken for the user if the user has a finished Enrolment.
        /// </summary>
        [HttpPost("access", Name = nameof(CreateEnrolmentCertificateAccessToken))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
            
            var createdToken = await _certificateAccessService.CreateEnrolmentCertificateAccessTokenAsync(enrolment.EnrolleeId);

            // return CreatedAtAction(nameof(GetEnrolmentById), new { enrolmentId = createdEnrolmentId }, new ApiCreatedResponse<Enrolment>(enrolment));
            return Ok(new ApiCreatedResponse<EnrolmentCertificateAccessToken>(createdToken));
        }
    }
}

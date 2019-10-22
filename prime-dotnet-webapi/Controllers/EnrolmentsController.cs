using System;
using System.Collections.Generic;
using System.Linq;
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
    public class EnrolmentsController : ControllerBase
    {
        private readonly IEnrolmentService _enrolmentService;

        public EnrolmentsController(IEnrolmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }

        // GET: api/Enrolment
        /// <summary>
        /// Gets all of the enrolments for the user, or all enrolments if user has ADMIN role.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrolments))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Enrolment>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Enrolment>>> GetEnrolments()
        {
            IEnumerable<Enrolment> enrolments = null;

            // User must have the ADMIN role to see all enrolments
            if (User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE))
            {
                enrolments = await _enrolmentService.GetEnrolmentsAsync();
            }
            else
            {
                enrolments = await _enrolmentService.GetEnrolmentsForUserIdAsync(
                                        PrimeUtils.PrimeUserId(User));
            }

            return Ok(new ApiOkResponse<IEnumerable<Enrolment>>(enrolments.ToList()));
        }

        // GET: api/Enrolment/5
        /// <summary>
        /// Gets a specific Enrolment.
        /// </summary>
        /// <param name="enrolmentId"></param> 
        [HttpGet("{enrolmentId}", Name = nameof(GetEnrolmentById))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<Enrolment>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrolment>> GetEnrolmentById(int enrolmentId)
        {
            var enrolment = await _enrolmentService.GetEnrolmentAsync(enrolmentId);

            if (enrolment == null)
            {
                return NotFound(new ApiResponse(404, $"Enrolment not found with id {enrolmentId}"));
            }

            // if the user is not an ADMIN, make sure the enrolment matches the enrollee, otherwise return not authorized
            if (!BelongsToEnrollee(enrolment))
            {
                return Forbid();
            }

            return Ok(new ApiOkResponse<Enrolment>(enrolment));
        }

        // POST: api/Enrolment
        /// <summary>
        /// Creates a new Enrolment.
        /// </summary>
        [HttpPost(Name = nameof(CreateEnrolment))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Enrolment>> CreateEnrolment(Enrolment enrolment)
        {
            var createdEnrolmentId = await _enrolmentService.CreateEnrolmentAsync(enrolment);

            return CreatedAtAction(nameof(GetEnrolmentById), new { enrolmentId = createdEnrolmentId }, enrolment);
        }

        // PUT: api/Enrolment/5
        /// <summary>
        /// Updates a specific Enrolment.
        /// </summary>
        /// <param name="enrolmentId"></param>
        /// <param name="enrolment"></param> 
        [HttpPut("{enrolmentId}", Name = nameof(UpdateEnrolment))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEnrolment(int enrolmentId, Enrolment enrolment)
        {
            if (enrolmentId != enrolment.Id)
            {
                this.ModelState.AddModelError("Enrolment.Id", "Enrolment Id does not match with the payload.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            if (enrolment.Enrollee == null
                    || enrolment.Enrollee.Id == null)
            {
                this.ModelState.AddModelError("Enrollee.Id", "Enrollee Id is required to make updates.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            if (!_enrolmentService.EnrolmentExists(enrolmentId))
            {
                return NotFound(new ApiResponse(404, $"Enrolment not found with id {enrolmentId}"));
            }

            // if the user is not an ADMIN, make sure the enrolleeId matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrolment))
            {
                return Forbid();
            }

            await _enrolmentService.UpdateEnrolmentAsync(enrolment);

            return NoContent();
        }

        // DELETE: api/Enrolment/5
        /// <summary>
        /// Deletes a specific Enrolment.
        /// </summary>
        /// <param name="enrolmentId"></param> 
        [HttpDelete("{enrolmentId}", Name = nameof(DeleteEnrolment))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<Enrolment>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrolment>> DeleteEnrolment(int enrolmentId)
        {
            var enrolment = await _enrolmentService.GetEnrolmentAsync(enrolmentId);
            if (enrolment == null)
            {
                return NotFound(new ApiResponse(404, $"Enrolment not found with id {enrolmentId}"));
            }

            // if the user is not an ADMIN, make sure the enrolleeId matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrolment))
            {
                return Forbid();
            }

            await _enrolmentService.DeleteEnrolmentAsync(enrolmentId);

            return Ok(new ApiOkResponse<Enrolment>(enrolment));
        }

        private bool BelongsToEnrollee(Enrolment enrolment)
        {
            bool belongsToEnrollee = false;

            // check to see if the logged in user is an admin
            belongsToEnrollee = User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE);

            // if user is not ADMIN, check that user belongs to the enrolment
            if (!belongsToEnrollee)
            {
                // get the prime user id from the logged in user - note: this returns 'Guid.Empty' if there is no logged in user
                Guid PrimeUserId = PrimeUtils.PrimeUserId(User);

                // check to see if the logged in user id is not 'Guid.Empty', and matches the one in the enrolment
                belongsToEnrollee = !PrimeUserId.Equals(Guid.Empty)
                        && PrimeUserId.Equals(enrolment.Enrollee.UserId);
            }

            return belongsToEnrollee;
        }
    }
}

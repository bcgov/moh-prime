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
        private readonly IHibcApiService _hibcApiService;

        public EnrolmentsController(IEnrolmentService enrolmentService, IHibcApiService hibcApiService)
        {
            _enrolmentService = enrolmentService;
            _hibcApiService = hibcApiService;
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

        // GET: api/Enrolments
        /// <summary>
        /// Gets all of the enrolments for the user, or all enrolments if user has ADMIN role.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrolments))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Enrolment>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Enrolment>>> GetEnrolments(
            [FromQuery]EnrolmentSearchOptions searchOptions)
        {
            IEnumerable<Enrolment> enrolments = null;

            // User must have the ADMIN role to see all enrolments
            if (User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE))
            {
                enrolments = await _enrolmentService.GetEnrolmentsAsync(searchOptions);
            }
            else
            {
                enrolments = await _enrolmentService.GetEnrolmentsForUserIdAsync(
                                        PrimeUtils.PrimeUserId(User));
            }

            return Ok(new ApiOkResponse<IEnumerable<Enrolment>>(enrolments.ToList()));
        }

        // GET: api/Enrolments/5
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

        // POST: api/Enrolments
        /// <summary>
        /// Creates a new Enrolment.
        /// </summary>
        [HttpPost(Name = nameof(CreateEnrolment))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiCreatedResponse<Enrolment>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Enrolment>> CreateEnrolment(Enrolment enrolment)
        {
            if (enrolment == null)
            {
                this.ModelState.AddModelError("Enrolment", "Could not create an enrolment, the passed in Enrolment cannot be null.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            // check to see if this userId already has an enrolment, if so, reject creating another
            var existingEnrolment = await _enrolmentService.GetEnrolmentForUserIdAsync(enrolment.Enrollee.UserId);

            if (existingEnrolment != null)
            {
                this.ModelState.AddModelError("Enrollee.UserId", "An enrolment already exists for this User Id, only one enrolment is allowed per User Id.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var createdEnrolmentId = await _enrolmentService.CreateEnrolmentAsync(enrolment);

            return CreatedAtAction(nameof(GetEnrolmentById), new { enrolmentId = createdEnrolmentId }, new ApiCreatedResponse<Enrolment>(enrolment));
        }

        // PUT: api/Enrolments/5
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
            if (enrolment == null)
            {
                this.ModelState.AddModelError("Enrolment", "Could not update the enrolment, the passed in Enrolment cannot be null.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

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

            // if the enrolment is not in the status of 'In Progress', it cannot be updated
            if (!(await _enrolmentService.IsEnrolmentInStatusAsync(enrolmentId, Status.IN_PROGRESS_CODE)))
            {
                this.ModelState.AddModelError("Enrolment.CurrentStatus", "Enrolment can not be updated when the current status is not 'In Progress'.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            // if the user is not an ADMIN, make sure the enrolleeId matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrolment))
            {
                return Forbid();
            }

            await _enrolmentService.UpdateEnrolmentAsync(enrolment);

            return NoContent();
        }

        // DELETE: api/Enrolments/5
        /// <summary>
        /// Deletes a specific Enrolment.
        /// </summary>
        /// <param name="enrolmentId"></param> 
        [HttpDelete("{enrolmentId}", Name = nameof(DeleteEnrolment))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<Enrolment>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteEnrolment(int enrolmentId)
        {
            string resp = await _hibcApiService.ValidateCollegeLicense();

            return Ok(new ApiOkResponse<string>(resp));
        }
        // TODO X
        // public async Task<ActionResult<Enrolment>> DeleteEnrolment(int enrolmentId)
        // {
        //     var enrolment = await _enrolmentService.GetEnrolmentAsync(enrolmentId);
        //     if (enrolment == null)
        //     {
        //         return NotFound(new ApiResponse(404, $"Enrolment not found with id {enrolmentId}"));
        //     }

        //     // if the user is not an ADMIN, make sure the enrolleeId matches the user, otherwise return not authorized
        //     if (!BelongsToEnrollee(enrolment))
        //     {
        //         return Forbid();
        //     }

        //     await _enrolmentService.DeleteEnrolmentAsync(enrolmentId);

        //     return Ok(new ApiOkResponse<Enrolment>(enrolment));
        // }

        // GET: api/Enrolments/5/availableStatuses
        /// <summary>
        /// Gets a list of the statuses that the enrolment can change to.
        /// </summary>
        /// <param name="enrolmentId"></param> 
        [HttpGet("{enrolmentId}/availableStatuses", Name = nameof(GetAvailableEnrolmentStatuses))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Status>>> GetAvailableEnrolmentStatuses(int enrolmentId)
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

            var availableEnrolmentStatuses = await _enrolmentService.GetAvailableEnrolmentStatusesAsync(enrolmentId);

            return Ok(new ApiOkResponse<IEnumerable<Status>>(availableEnrolmentStatuses));
        }

        // GET: api/Enrolments/5/statuses
        /// <summary>
        /// Gets all of the status changes for a specific Enrolment.
        /// </summary>
        /// <param name="enrolmentId"></param> 
        [HttpGet("{enrolmentId}/statuses", Name = nameof(GetEnrolmentStatuses))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolmentStatus>>> GetEnrolmentStatuses(int enrolmentId)
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

            var enrolments = await _enrolmentService.GetEnrolmentStatusesAsync(enrolmentId);

            return Ok(new ApiOkResponse<IEnumerable<EnrolmentStatus>>(enrolments));
        }

        // POST: api/Enrolments/5/statuses
        /// <summary>
        /// Adds a status change for a specific Enrolment.
        /// </summary>
        [HttpPost("{enrolmentId}/statuses", Name = nameof(CreateEnrolmentStatus))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolmentStatus>> CreateEnrolmentStatus(int enrolmentId, Status status)
        {
            var enrolment = await _enrolmentService.GetEnrolmentAsync(enrolmentId);

            if (enrolment == null)
            {
                return NotFound(new ApiResponse(404, $"Enrolment not found with id {enrolmentId}"));
            }

            if (status?.Code == null || status.Code < 1)
            {
                this.ModelState.AddModelError("Status.Code", "Status Code is required to create statuses.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            // if the user is not an ADMIN, make sure the enrolment matches the enrollee, otherwise return not authorized
            if (!BelongsToEnrollee(enrolment))
            {
                return Forbid();
            }

            if (!_enrolmentService.IsStatusChangeAllowed(enrolment.CurrentStatus?.Status, status))
            {
                this.ModelState.AddModelError("Status.Code", $"Cannot change from current Status Code: {enrolment.CurrentStatus?.Status?.Code} to the new Status Code: {status.Code}");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var enrolmentStatus = await _enrolmentService.CreateEnrolmentStatusAsync(enrolmentId, status);

            return Ok(new ApiOkResponse<EnrolmentStatus>(enrolmentStatus));
        }
    }
}

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
    // User needs at least the ADMIN or ENROLLEE role to use this controller
    [Authorize(Policy = PrimeConstants.PRIME_USER_POLICY)]
    public class EnrolleesController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;

        public EnrolleesController(IEnrolleeService enrolleeService)
        {
            _enrolleeService = enrolleeService;
        }

        // GET: api/Enrollees
        /// <summary>
        /// Gets all of the enrollees for the user, or all enrollees if user has ADMIN role.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrollees))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Enrollee>>), StatusCodes.Status200OK)]
        [Authorize(Policy = PrimeConstants.PRIME_ADMIN_POLICY)]
        public async Task<ActionResult<IEnumerable<Enrollee>>> GetEnrollees(
            [FromQuery]EnrolmentSearchOptions searchOptions)
        {
            IEnumerable<Enrollee> enrollees = await _enrolleeService.GetEnrolleesAsync(searchOptions);
            return Ok(new ApiOkResponse<IEnumerable<Enrollee>>(enrollees.ToList()));
        }

        // GET: api/Enrollees/5
        /// <summary>
        /// Gets a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}", Name = nameof(GetEnrolleeById))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<Enrollee>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrollee>> GetEnrolleeById(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}"));
            }

            return Ok(new ApiOkResponse<Enrollee>(enrollee));
        }

        // POST: api/Enrollees
        /// <summary>
        /// Creates a new Enrollee.
        /// </summary>
        [HttpPost(Name = nameof(CreateEnrollee))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiCreatedResponse<Enrollee>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Enrollee>> CreateEnrollee(Enrollee enrollee)
        {
            if (enrollee == null)
            {
                this.ModelState.AddModelError("Enrollee", "Could not create an enrollee, the passed in Enrollee cannot be null.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var createdEnrolleeId = await _enrolleeService.CreateEnrolleeAsync(enrollee);

            return CreatedAtAction(nameof(GetEnrolleeById), new { enrolleeId = createdEnrolleeId }, new ApiCreatedResponse<Enrollee>(enrollee));
        }

        // PUT: api/Enrollees/5
        /// <summary>
        /// Updates a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="enrollee"></param>
        [HttpPut("{enrolleeId}", Name = nameof(UpdateEnrollee))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEnrollee(int enrolleeId, Enrollee enrollee)
        {
            if (enrollee == null)
            {
                this.ModelState.AddModelError("Enrollee", "Could not update the enrollee, the passed in Enrollee cannot be null.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            if (enrolleeId != enrollee.Id)
            {
                this.ModelState.AddModelError("Enrollee.Id", "Enrollee Id does not match with the payload.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            if (!_enrolleeService.EnrolleeExists(enrolleeId))
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}"));
            }

            // if the enrollee is not in the status of 'In Progress', it cannot be updated
            if (!(await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, Status.IN_PROGRESS_CODE)))
            {
                this.ModelState.AddModelError("Enrollee.CurrentStatus", "Enrollee can not be updated when the current status is not 'In Progress'.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            await _enrolleeService.UpdateEnrolleeAsync(enrollee);

            return NoContent();
        }

        // DELETE: api/Enrollees/5
        /// <summary>
        /// Deletes a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}", Name = nameof(DeleteEnrollee))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<Enrollee>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrollee>> DeleteEnrollee(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            if (enrollee == null)
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}"));
            }

            await _enrolleeService.DeleteEnrolleeAsync(enrolleeId);

            return Ok(new ApiOkResponse<Enrollee>(enrollee));
        }

        // GET: api/Enrollees/5/availableStatuses
        /// <summary>
        /// Gets a list of the statuses that the enrollee can change to.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/availableStatuses", Name = nameof(GetAvailableEnrolmentStatuses))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Status>>> GetAvailableEnrolmentStatuses(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}"));
            }

            var availableEnrolmentStatuses = await _enrolleeService.GetAvailableEnrolmentStatusesAsync(enrolleeId);

            return Ok(new ApiOkResponse<IEnumerable<Status>>(availableEnrolmentStatuses));
        }

        // GET: api/Enrollees/5/statuses
        /// <summary>
        /// Gets all of the status changes for a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/statuses", Name = nameof(GetEnrolmentStatuses))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolmentStatus>>> GetEnrolmentStatuses(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}"));
            }

            var enrollees = await _enrolleeService.GetEnrolmentStatusesAsync(enrolleeId);

            return Ok(new ApiOkResponse<IEnumerable<EnrolmentStatus>>(enrollees));
        }

        // POST: api/Enrollees/5/statuses
        /// <summary>
        /// Adds a status change for a specific Enrollee.
        /// </summary>
        [HttpPost("{enrolleeId}/statuses", Name = nameof(CreateEnrolmentStatus))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolmentStatus>> CreateEnrolmentStatus(int enrolleeId, Status status)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}"));
            }

            if (status?.Code == null || status.Code < 1)
            {
                this.ModelState.AddModelError("Status.Code", "Status Code is required to create statuses.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            if (!_enrolleeService.IsStatusChangeAllowed(enrollee.CurrentStatus?.Status, status))
            {
                this.ModelState.AddModelError("Status.Code", $"Cannot change from current Status Code: {enrollee.CurrentStatus?.Status?.Code} to the new Status Code: {status.Code}");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var enrolmentStatus = await _enrolleeService.CreateEnrolmentStatusAsync(enrolleeId, status);

            return Ok(new ApiOkResponse<EnrolmentStatus>(enrolmentStatus));
        }

    }
}

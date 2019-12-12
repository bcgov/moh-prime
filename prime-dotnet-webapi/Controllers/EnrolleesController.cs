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
    // [Authorize(Policy = PrimeConstants.PRIME_USER_POLICY)]
    public class EnrolleesController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;

        public EnrolleesController(IEnrolleeService enrolleeService)
        {
            _enrolleeService = enrolleeService;
        }

        private bool BelongsToEnrollee(Enrollee enrollee)
        {
            bool belongsToEnrollee = false;

            // Check to see if the logged in user is an admin
            belongsToEnrollee = User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE);

            // If user is not ADMIN, check that user belongs to the enrolment
            if (!belongsToEnrollee)
            {
                // Get the prime user id from the logged in user - note: this returns 'Guid.Empty' if there is no logged in user
                Guid PrimeUserId = User.GetPrimeUserId();
                // Check to see if the logged in user id is not 'Guid.Empty', and matches the one in the enrolment
                belongsToEnrollee = !PrimeUserId.Equals(Guid.Empty) && PrimeUserId.Equals(enrollee.UserId);
            }

            return belongsToEnrollee;
        }

        // GET: api/Enrollees
        /// <summary>
        /// Gets all of the enrollees for the user, or all enrollees if user has ADMIN role.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrollees))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Enrollee>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Enrollee>>> GetEnrollees(
            [FromQuery]EnrolleeSearchOptions searchOptions)
        {
            IEnumerable<Enrollee> enrollees = null;

            // User must have the ADMIN role to see all enrollees
            if (User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE))
            {
                enrollees = await _enrolleeService.GetEnrolleesAsync(searchOptions);
            }
            else
            {
                var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(User.GetPrimeUserId());
                enrollees = new List<Enrollee>();

                if (enrollee != null)
                {
                    enrollees = enrollees.Append(enrollee);
                }
            }

            return Ok(new ApiOkResponse<IEnumerable<Enrollee>>(enrollees));
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

            // if the user is not an ADMIN, make sure the enrolleeId matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrollee))
            {
                return Forbid();
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

            // Check to see if this userId is already an enrollee, if so, reject creating another
            var existingEnrolment = await _enrolleeService.GetEnrolleeForUserIdAsync(enrollee.UserId);

            if (existingEnrolment != null)
            {
                this.ModelState.AddModelError("Enrollee.UserId", "An enrollee already exists for this User Id, only one enrollee is allowed per User Id.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var createdEnrolleeId = await _enrolleeService.CreateEnrolleeAsync(enrollee);

            return CreatedAtAction(
                nameof(GetEnrolleeById),
                new { enrolleeId = createdEnrolleeId },
                new ApiCreatedResponse<Enrollee>(enrollee)
            );
        }

        // PUT: api/Enrollees/5
        /// <summary>
        /// Updates a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="enrollee"></param>
        /// <param name="beenThroughTheWizard"></param>
        [HttpPut("{enrolleeId}", Name = nameof(UpdateEnrollee))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEnrollee(int enrolleeId, Enrollee enrollee, [FromQuery]bool beenThroughTheWizard)
        {
            if (enrollee == null)
            {
                this.ModelState.AddModelError("Enrollee", "Could not update the enrollee, the passed in Enrollee cannot be null.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            if (enrollee == null || enrollee.Id == null)
            {
                this.ModelState.AddModelError("Enrollee.Id", "Enrollee Id is required to make updates.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            if (enrolleeId != enrollee.Id)
            {
                this.ModelState.AddModelError("Enrollee.Id", "Enrollee Id does not match with the payload.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            if (!await _enrolleeService.EnrolleeExists(enrolleeId))
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}"));
            }

            // If the enrollee is not in the status of 'In Progress', enrollee can only be updated by an ADMIN
            if (!User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE) && !(await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, Status.IN_PROGRESS_CODE)))
            {
                this.ModelState.AddModelError("Enrollee.CurrentStatus", "Enrollee can not be updated when the current status is not 'In Progress'.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            // If the user is not an ADMIN, make sure the enrolleeId matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrollee))
            {
                return Forbid();
            }

            await _enrolleeService.UpdateEnrolleeAsync(enrollee, beenThroughTheWizard);

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

            // if the user is not an ADMIN, make sure the enrolleeId matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrollee))
            {
                return Forbid();
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

            // if the user is not an ADMIN, make sure the enrolleeId matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrollee))
            {
                return Forbid();
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

            // if the user is not an ADMIN, make sure the enrolleeId matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrollee))
            {
                return Forbid();
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

            // if the user is not an ADMIN, make sure the enrolleeId matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrollee))
            {
                return Forbid();
            }

            if (!_enrolleeService.IsStatusChangeAllowed(enrollee.CurrentStatus?.Status, status))
            {
                this.ModelState.AddModelError("Status.Code", $"Cannot change from current Status Code: {enrollee.CurrentStatus?.Status?.Code} to the new Status Code: {status.Code}");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var enrolmentStatus = await _enrolleeService.CreateEnrolmentStatusAsync(enrolleeId, status);

            return Ok(new ApiOkResponse<EnrolmentStatus>(enrolmentStatus));
        }

        // GET: api/Enrollees/5/adjudicator-notes
        /// <summary>
        /// Gets all of the adjudicator notes for a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/adjudicator-notes", Name = nameof(GetAdjudicatorNotes))]
        [Authorize(Policy = PrimeConstants.PRIME_ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AdjudicatorNote>>> GetAdjudicatorNotes(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}"));
            }

            // If the user is not an ADMIN, make sure the enrollee ID matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrollee))
            {
                return Forbid();
            }

            var adjudicationNotes = await _enrolleeService.GetEnrolleeAdjudicatorNotesAsync(enrollee);

            return Ok(new ApiOkResponse<IEnumerable<AdjudicatorNote>>(adjudicationNotes));
        }

        // POST: api/Enrollees/5/adjudicator-notes
        /// <summary>
        /// Creates a new adjudicator note on an enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="adjudicatorNote"></param>
        [HttpPost("{enrolleeId}/adjudicator-notes", Name = nameof(CreateAdjudicatorNote))]
        [Authorize(Policy = PrimeConstants.PRIME_ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiCreatedResponse<AdjudicatorNote>), StatusCodes.Status201Created)]
        public async Task<ActionResult<AdjudicatorNote>> CreateAdjudicatorNote(int enrolleeId, AdjudicatorNote adjudicatorNote)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (!await _enrolleeService.EnrolleeExists(enrolleeId))
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}"));
            }

            // If the user is not an ADMIN, make sure the enrollee ID matches the user, otherwise return not authorized
            if (!BelongsToEnrollee(enrollee))
            {
                return Forbid();
            }

            if (enrolleeId != adjudicatorNote.EnrolleeId)
            {
                this.ModelState.AddModelError("Enrollee.Id", "Enrollee Id is required to make updates.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var createdAdjudicatorNote = await _enrolleeService.CreateEnrolleeAdjudicatorNoteAsync(enrolleeId, adjudicatorNote);

            return CreatedAtAction(
                nameof(GetAdjudicatorNotes),
                new { enrolleeId = enrolleeId },
                new ApiCreatedResponse<AdjudicatorNote>(createdAdjudicatorNote)
            );
        }

        // PUT: api/Enrollees/5
        /// <summary>
        /// Updates a specific note on an Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="note"></param>
        /// <param name="noteType"></param>
        [HttpPut("{enrolleeId}/enrollee-notes", Name = nameof(UpdateEnrolleeNote))]
        // [Authorize(Policy = PrimeConstants.PRIME_ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEnrolleeNote(int enrolleeId, [FromBody] string note, [FromQuery] NoteType noteType)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}."));
            }

            if (!Enum.IsDefined(typeof(NoteType), noteType) && !noteType.Equals(NoteType.AdjudicatorNote))
            {
                return BadRequest(new ApiResponse(400, $"Note type can not be updated."));
            }

            await _enrolleeService.UpdateEnrolleeNoteAsync(enrolleeId, note, noteType);

            return NoContent();
        }
    }
}

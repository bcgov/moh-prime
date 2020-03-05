using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    // User needs at least the READONLY ADMIN or ENROLLEE role to use this controller
    [Authorize(Policy = PrimeConstants.USER_POLICY)]
    public class EnrolleesController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAccessTermService _accessTermService;
        private readonly IEnrolleeProfileVersionService _enrolleeProfileVersionService;
        private readonly IAdminService _adminService;
        private readonly IBusinessEventService _businessEventService;

        public EnrolleesController(
            IEnrolleeService enrolleeService,
            IAccessTermService accessTermService,
            IEnrolleeProfileVersionService enrolleeProfileVersionService,
            IAdminService adminService,
            IBusinessEventService businessEventService)
        {
            _enrolleeService = enrolleeService;
            _accessTermService = accessTermService;
            _enrolleeProfileVersionService = enrolleeProfileVersionService;
            _adminService = adminService;
            _businessEventService = businessEventService;
        }


        // GET: api/Enrollees
        /// <summary>
        /// Gets all of the enrollees for the user, or all enrollees if user has ADMIN role.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrollees))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Enrollee>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Enrollee>>> GetEnrollees([FromQuery]EnrolleeSearchOptions searchOptions)
        {
            IEnumerable<Enrollee> enrollees = null;

            // User must have the RO_ADMIN or ADMIN role to see all enrollees
            if (User.IsAdmin() || User.HasAdminView())
            {
                enrollees = await _enrolleeService.GetEnrolleesAsync(searchOptions);
            }
            else
            {
                var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(User.GetPrimeUserId());
                enrollees = enrollee != null ? new[] { enrollee } : new Enrollee[0];
            }

            return Ok(ApiResponse.Result(enrollees));
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
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Enrollee>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrollee>> GetEnrolleeById(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanView(enrollee))
            {
                return Forbid();
            }

            return Ok(ApiResponse.Result(enrollee));
        }

        // POST: api/Enrollees
        /// <summary>
        /// Creates a new Enrollee.
        /// </summary>
        [HttpPost(Name = nameof(CreateEnrollee))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Enrollee>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Enrollee>> CreateEnrollee(Enrollee enrollee)
        {
            if (enrollee == null)
            {
                this.ModelState.AddModelError("Enrollee", "Could not create an enrollee, the passed in Enrollee cannot be null.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            if (!User.CanEdit(enrollee))
            {
                return Forbid();
            }

            // Check to see if this userId is already an enrollee, if so, reject creating another
            if (await _enrolleeService.EnrolleeUserIdExistsAsync(enrollee.UserId))
            {
                this.ModelState.AddModelError("Enrollee.UserId", "An enrollee already exists for this User Id, only one enrollee is allowed per User Id.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            var createdEnrolleeId = await _enrolleeService.CreateEnrolleeAsync(enrollee);

            return CreatedAtAction(
                nameof(GetEnrolleeById),
                new { enrolleeId = createdEnrolleeId },
                ApiResponse.Result(enrollee)
            );
        }

        // PUT: api/Enrollees/5
        /// <summary>
        /// Updates a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="enrolleeProfile"></param>
        /// <param name="beenThroughTheWizard"></param>
        [HttpPut("{enrolleeId}", Name = nameof(UpdateEnrollee))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEnrollee(int enrolleeId, EnrolleeProfileViewModel enrolleeProfile, [FromQuery]bool beenThroughTheWizard)
        {
            var enrollee = await _enrolleeService.GetEnrolleeNoTrackingAsync(enrolleeId);
            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanEdit(enrollee))
            {
                return Forbid();
            }

            // If the enrollee is not in the status of 'Active', it cannot be updated
            if (!(await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, Status.ACTIVE_CODE)))
            {
                this.ModelState.AddModelError("Enrollee.CurrentStatus", "Enrollee can not be updated when the current status is not 'Active'.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            await _enrolleeService.UpdateEnrolleeAsync(enrolleeId, enrolleeProfile, beenThroughTheWizard);

            return NoContent();
        }

        // DELETE: api/Enrollees/5
        /// <summary>
        /// Deletes a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}", Name = nameof(DeleteEnrollee))]
        [Authorize(Policy = PrimeConstants.SUPER_ADMIN_POLICY)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Enrollee>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrollee>> DeleteEnrollee(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanEdit(enrollee))
            {
                return Forbid();
            }

            await _enrolleeService.DeleteEnrolleeAsync(enrolleeId);

            return Ok(ApiResponse.Result(enrollee));
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
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Status>>> GetAvailableEnrolmentStatuses(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(new ApiMessageResponse($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanView(enrollee))
            {
                return Forbid();
            }

            var availableEnrolmentStatuses = await _enrolleeService.GetAvailableEnrolmentStatusesAsync(enrolleeId);

            return Ok(new ApiResultResponse<IEnumerable<Status>>(availableEnrolmentStatuses));
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
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolmentStatus>>> GetEnrolmentStatuses(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanView(enrollee))
            {
                return Forbid();
            }

            var enrollees = await _enrolleeService.GetEnrolmentStatusesAsync(enrolleeId);

            return Ok(ApiResponse.Result(enrollees));
        }

        // POST: api/Enrollees/5/statuses
        /// <summary>
        /// Adds a status change for a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="status"></param>
        /// <param name="acceptedAccessTerm"></param>
        [HttpPost("{enrolleeId}/statuses", Name = nameof(CreateEnrolmentStatus))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolmentStatus>> CreateEnrolmentStatus(int enrolleeId, Status status, [FromQuery]bool acceptedAccessTerm)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            int? adminId = null;

            if (enrollee == null)
            {
                return NotFound(new ApiMessageResponse($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanEdit(enrollee))
            {
                return Forbid();
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

            if (User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE))
            {
                var admin = await _adminService.GetAdminForUserIdAsync(User.GetPrimeUserId());
                adminId = admin.Id;
            }

            var enrolmentStatus = await _enrolleeService.CreateEnrolmentStatusAsync(enrolleeId, status, acceptedAccessTerm, adminId);
            return Ok(new ApiResultResponse<EnrolmentStatus>(enrolmentStatus));
        }

        // GET: api/Enrollees/5/adjudicator-notes
        /// <summary>
        /// Gets all of the adjudicator notes for a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/adjudicator-notes", Name = nameof(GetAdjudicatorNotes))]
        [Authorize(Policy = PrimeConstants.READONLY_ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AdjudicatorNote>>> GetAdjudicatorNotes(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            var adjudicationNotes = await _enrolleeService.GetEnrolleeAdjudicatorNotesAsync(enrollee);

            return Ok(ApiResponse.Result(adjudicationNotes));
        }

        // POST: api/Enrollees/5/adjudicator-notes
        /// <summary>
        /// Creates a new adjudicator note on an enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="note"></param>
        [HttpPost("{enrolleeId}/adjudicator-notes", Name = nameof(CreateAdjudicatorNote))]
        [Authorize(Policy = PrimeConstants.ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AdjudicatorNote>), StatusCodes.Status201Created)]
        public async Task<ActionResult<AdjudicatorNote>> CreateAdjudicatorNote(int enrolleeId, FromBodyText note)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            if (string.IsNullOrWhiteSpace(note))
            {
                this.ModelState.AddModelError("note", "Adjudicator notes can't be null or empty.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            var admin = await _adminService.GetAdminForUserIdAsync(User.GetPrimeUserId());
            var createdAdjudicatorNote = await _enrolleeService.CreateEnrolleeAdjudicatorNoteAsync(enrolleeId, note, admin.Id);

            return CreatedAtAction(
                nameof(GetAdjudicatorNotes),
                new { enrolleeId = enrolleeId },
                ApiResponse.Result(createdAdjudicatorNote)
            );
        }

        // PUT: api/Enrollees/5/access-agreement-notes
        /// <summary>
        /// Updates an access agreement note.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="accessAgreementNote"></param>
        [HttpPut("{enrolleeId}/access-agreement-notes", Name = nameof(UpdateAccessAgreementNote))]
        [Authorize(Policy = PrimeConstants.ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AccessAgreementNote>), StatusCodes.Status200OK)]
        public async Task<ActionResult<AccessAgreementNote>> UpdateAccessAgreementNote(int enrolleeId, AccessAgreementNote accessAgreementNote)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}."));
            }

            if (accessAgreementNote.EnrolleeId != 0 && enrolleeId != accessAgreementNote.EnrolleeId)
            {
                this.ModelState.AddModelError("AccessAgreementNote.EnrolleeId", "Enrollee Id does not match with the payload.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, Status.UNDER_REVIEW_CODE))
            {
                this.ModelState.AddModelError("Enrollee.CurrentStatus", "Access agreement notes can not be updated when the current status is 'Active'.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            var admin = await _adminService.GetAdminForUserIdAsync(User.GetPrimeUserId());
            var updatedNote = await _enrolleeService.UpdateEnrolleeNoteAsync(enrolleeId, accessAgreementNote, admin.Id);

            return Ok(ApiResponse.Result(updatedNote));
        }

        // GET: api/Enrollees/5/versions
        /// <summary>
        /// Get a list of enrollee profile versions.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/versions", Name = nameof(GetEnrolleeProfileVersions))]
        [Authorize(Policy = PrimeConstants.READONLY_ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeProfileVersion>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolleeProfileVersion>>> GetEnrolleeProfileVersions(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            var enrolleeProfileHistories = await _enrolleeProfileVersionService.GetEnrolleeProfileVersionsAsync(enrolleeId);

            return Ok(ApiResponse.Result(enrolleeProfileHistories));
        }

        // GET: api/Enrollees/5/versions/1
        /// <summary>
        /// Get an enrollee profile version.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="enrolleeProfileVersionId"></param>
        [HttpGet("{enrolleeId}/versions/{enrolleeProfileVersionId}", Name = nameof(GetEnrolleeProfileVersion))]
        [Authorize(Policy = PrimeConstants.READONLY_ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeProfileVersion>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeProfileVersion>> GetEnrolleeProfileVersion(int enrolleeId, int enrolleeProfileVersionId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            var enrolleeProfileVersion = await _enrolleeProfileVersionService.GetEnrolleeProfileVersionAsync(enrolleeProfileVersionId);

            return Ok(ApiResponse.Result(enrolleeProfileVersion));
        }

        // PATCH: api/Enrollees/5/always-manual
        /// <summary>
        /// Updates an enrollees always manual flag, forcing them to always be sent to manual adjudication
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="alwaysManual"></param>
        [HttpPatch("{enrolleeId}/always-manual", Name = nameof(UpdateEnrolleeAlwaysManual))]
        [Authorize(Policy = PrimeConstants.ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Enrollee>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrollee>> UpdateEnrolleeAlwaysManual(int enrolleeId, FromBodyData<bool> alwaysManual)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(new ApiMessageResponse($"Enrollee not found with id {enrolleeId}."));
            }

            var updatedEnrollee = await _enrolleeService.UpdateEnrolleeAlwaysManualAsync(enrolleeId, alwaysManual);

            return Ok(new ApiResultResponse<Enrollee>(updatedEnrollee));
        }

        // PUT: api/Enrollees/5/adjudicator
        /// <summary>
        /// Add an enrollee's assigned adjudicator.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPut("{enrolleeId}/adjudicator", Name = nameof(SetEnrolleeAdjudicator))]
        [Authorize(Policy = PrimeConstants.ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Enrollee>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrollee>> SetEnrolleeAdjudicator(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}."));
            }

            var adjudicatorUserId = User.GetPrimeUserId();
            var admin = await _adminService.GetAdminForUserIdAsync(User.GetPrimeUserId());
            var updatedEnrollee = await _enrolleeService.UpdateEnrolleeAdjudicator(enrollee.Id, adjudicatorUserId);
            await _businessEventService.CreateAdminClaimEventAsync(enrolleeId, "Admin claimed enrollee", admin.Id);

            return Ok(ApiResponse.Result(updatedEnrollee));
        }

        // DELETE: api/Enrollees/5/adjudicator
        /// <summary>
        /// Remove an enrollee's assigned adjudicator.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}/adjudicator", Name = nameof(RemoveEnrolleeAdjudicator))]
        [Authorize(Policy = PrimeConstants.ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Enrollee>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrollee>> RemoveEnrolleeAdjudicator(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}."));
            }

            var updatedEnrollee = await _enrolleeService.UpdateEnrolleeAdjudicator(enrollee.Id);
            var admin = await _adminService.GetAdminForUserIdAsync(User.GetPrimeUserId());
            await _businessEventService.CreateAdminClaimEventAsync(enrolleeId, "Admin disclaimed enrollee", admin.Id);

            return Ok(ApiResponse.Result(updatedEnrollee));
        }
    }
}

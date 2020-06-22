using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;
using System.Reflection;
using System.IO;
using System.Linq;
using System;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    // User needs at least the READONLY ADMIN or ENROLLEE role to use this controller
    [Authorize(Policy = AuthConstants.USER_POLICY)]
    public class EnrolleesController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAccessTermService _accessTermService;
        private readonly IEnrolleeProfileVersionService _enrolleeProfileVersionService;
        private readonly IAdminService _adminService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IEmailService _emailService;
        private readonly ISubmissionService _submissionService;
        private readonly IDocumentService _documentService;
        private readonly IRazorConverterService _razorConverterService;

        public EnrolleesController(
            IEnrolleeService enrolleeService,
            IAccessTermService accessTermService,
            IEnrolleeProfileVersionService enrolleeProfileVersionService,
            IAdminService adminService,
            IBusinessEventService businessEventService,
            IEmailService emailService,
            ISubmissionService submissionService,
            IDocumentService documentService,
            IRazorConverterService razorConverterService)
        {
            _enrolleeService = enrolleeService;
            _accessTermService = accessTermService;
            _enrolleeProfileVersionService = enrolleeProfileVersionService;
            _adminService = adminService;
            _businessEventService = businessEventService;
            _emailService = emailService;
            _submissionService = submissionService;
            _documentService = documentService;
            _razorConverterService = razorConverterService;
        }

        // TODO revert after prod gpids are updated
        // PUT: api/Enrollees/UpdateGPIDS
        /// <summary>
        /// Updates all non HL7 compliant gpid's for all enrollees
        /// </summary>
        [HttpPut(Name = nameof(UpdateGPIDs))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateGPIDs()
        {
            if (!User.IsAdmin())
            {
                return Forbid();
            }

            await _submissionService.UpdateNonCompliantGPIDs();

            return NoContent();
        }

        // GET: api/Enrollees
        /// <summary>
        /// Gets all of the enrollees for the user, or all enrollees if user has ADMIN role.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrollees))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Enrollee>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Enrollee>>> GetEnrollees([FromQuery] EnrolleeSearchOptions searchOptions)
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
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId, User.HasAdminView());

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanView(enrollee))
            {
                return Forbid();
            }

            if (User.IsAdmin())
            {
                await _businessEventService.CreateAdminViewEventAsync(enrollee.Id, "Admin viewing the current Enrolment");
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

            enrollee.IdentityAssuranceLevel = User.GetIdentityAssuranceLevel();
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
        public async Task<IActionResult> UpdateEnrollee(int enrolleeId, EnrolleeProfileViewModel enrolleeProfile, [FromQuery] bool beenThroughTheWizard)
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

            // If the enrollee is not in the status of 'Editable', it cannot be updated
            if (!(await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.Editable)))
            {
                this.ModelState.AddModelError("Enrollee.CurrentStatus", "Enrollee can not be updated when the current status is not 'Editable'.");
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
        [Authorize(Policy = AuthConstants.SUPER_ADMIN_POLICY)]
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

        // GET: api/Enrollees/5/adjudicator-notes
        /// <summary>
        /// Gets all of the adjudicator notes for a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/adjudicator-notes", Name = nameof(GetAdjudicatorNotes))]
        [Authorize(Policy = AuthConstants.READONLY_ADMIN_POLICY)]
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
        /// <param name="link"></param>
        [HttpPost("{enrolleeId}/adjudicator-notes", Name = nameof(CreateAdjudicatorNote))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AdjudicatorNote>), StatusCodes.Status201Created)]
        public async Task<ActionResult<AdjudicatorNote>> CreateAdjudicatorNote(int enrolleeId, FromBodyText note, [FromQuery] bool link)
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

            if (link)
            {
                // Link Adjudicator note to most recent status change on an enrollee if request
                var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
                await _enrolleeService.AddAdjudicatorNoteToReferenceIdAsync(enrollee.CurrentStatus.Id, createdAdjudicatorNote.Id);
            }

            return CreatedAtAction(
                nameof(CreateAdjudicatorNote),
                new { enrolleeId = enrolleeId },
                ApiResponse.Result(createdAdjudicatorNote)
            );
        }

        // POST: api/Enrollees/5/status-reference
        /// <summary>
        /// Creates a new Enrolment Status Reference on the enrollee's current status.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/status-reference", Name = nameof(CreateEnrolmentReference))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AdjudicatorNote>), StatusCodes.Status201Created)]
        public async Task<ActionResult<AdjudicatorNote>> CreateEnrolmentReference(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            var admin = await _adminService.GetAdminForUserIdAsync(User.GetPrimeUserId());
            var createdEnrolmentStatusReference = await _enrolleeService.CreateEnrolmentStatusReferenceAsync(enrollee.CurrentStatus.Id, admin.Id);

            return CreatedAtAction(
                nameof(CreateEnrolmentReference),
                new { enrolleeId = enrolleeId },
                ApiResponse.Result(createdEnrolmentStatusReference)
            );
        }

        // PUT: api/Enrollees/5/access-agreement-notes
        /// <summary>
        /// Updates an access agreement note.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="accessAgreementNote"></param>
        [HttpPut("{enrolleeId}/access-agreement-notes", Name = nameof(UpdateAccessAgreementNote))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
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

            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.UnderReview))
            {
                this.ModelState.AddModelError("Enrollee.CurrentStatus", "Access agreement notes can not be updated when the current status is 'Editable'.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            var updatedNote = await _enrolleeService.UpdateEnrolleeNoteAsync(enrolleeId, accessAgreementNote);

            return Ok(ApiResponse.Result(updatedNote));
        }

        // GET: api/Enrollees/5/versions
        /// <summary>
        /// Get a list of enrollee profile versions.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/versions", Name = nameof(GetEnrolleeProfileVersions))]
        [Authorize(Policy = AuthConstants.READONLY_ADMIN_POLICY)]
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
        [Authorize(Policy = AuthConstants.READONLY_ADMIN_POLICY)]
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

        // PUT: api/Enrollees/5/adjudicator
        /// <summary>
        /// Add an enrollee's assigned adjudicator.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPut("{enrolleeId}/adjudicator", Name = nameof(SetEnrolleeAdjudicator))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
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

            var admin = await _adminService.GetAdminForUserIdAsync(User.GetPrimeUserId());
            var updatedEnrollee = await _enrolleeService.UpdateEnrolleeAdjudicator(enrollee.Id, admin);
            await _businessEventService.CreateAdminActionEventAsync(enrolleeId, "Admin claimed enrollee");

            return Ok(ApiResponse.Result(updatedEnrollee));
        }

        // DELETE: api/Enrollees/5/adjudicator
        /// <summary>
        /// Remove an enrollee's assigned adjudicator.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}/adjudicator", Name = nameof(RemoveEnrolleeAdjudicator))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
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
            await _businessEventService.CreateAdminActionEventAsync(enrolleeId, "Admin disclaimed enrollee");

            return Ok(ApiResponse.Result(updatedEnrollee));
        }

        // GET: api/Enrollees/5/events
        /// <summary>
        /// Gets a list of enrollee events.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/events", Name = nameof(getEnrolleeBusinessEvents))]
        [Authorize(Policy = AuthConstants.READONLY_ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<BusinessEvent>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BusinessEvent>>> getEnrolleeBusinessEvents(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            var events = await _enrolleeService.GetEnrolleeBusinessEvents(enrolleeId);

            return Ok(ApiResponse.Result(events));
        }

        // POST: api/Enrollees/5/reminder
        /// <summary>
        /// Send an enrollee a reminder email.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/reminder", Name = nameof(sendEnrolleeReminderEmail))]
        [Authorize(Policy = AuthConstants.READONLY_ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> sendEnrolleeReminderEmail(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            await _emailService.SendReminderEmailAsync(enrollee);

            return NoContent();
        }

        // POST: api/Enrollees/5/self-declaration-document
        /// <summary>
        /// Create Self Declaration Document Link
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="selfDeclarationDocument"></param>
        [HttpPost("{enrolleeId}/self-declaration-document", Name = nameof(createSelfDeclarationDocument))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<SelfDeclarationDocument>> createSelfDeclarationDocument(int enrolleeId, SelfDeclarationDocument selfDeclarationDocument)
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

            var sdd = await _enrolleeService.AddSelfDeclarationDocumentAsync(enrolleeId, selfDeclarationDocument);

            return Ok(ApiResponse.Result(sdd));
        }

        // GET: api/Enrollees/{enrolleeId}/self-declaration-document/{selfDeclarationDocumentId}
        /// <summary>
        /// Get the self Declaration document data.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="selfDeclarationDocumentId"></param>
        [HttpGet("{enrolleeId}/self-declaration-document/{selfDeclarationDocumentId}", Name = nameof(getSelfDeclarationDocument))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> getSelfDeclarationDocument(int enrolleeId, int selfDeclarationDocumentId)
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

            var document = await _documentService.GetDocumentBySelfDeclarationDocumentId(selfDeclarationDocumentId);

            return Ok(ApiResponse.Result(document.Data));
        }
    }
}

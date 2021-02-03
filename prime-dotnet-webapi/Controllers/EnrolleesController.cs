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
using Prime.ViewModels;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    // User needs at least the READONLY ADMIN or ENROLLEE role to use this controller
    [Authorize(Policy = Policies.User)]
    public class EnrolleesController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAgreementService _agreementService;
        private readonly IEnrolleeSubmissionService _enrolleeSubmissionService;
        private readonly IAdminService _adminService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IEmailService _emailService;
        private readonly IDocumentService _documentService;
        private readonly IRazorConverterService _razorConverterService;

        public EnrolleesController(
            IEnrolleeService enrolleeService,
            IAgreementService agreementService,
            IEnrolleeSubmissionService enrolleeSubmissionService,
            IAdminService adminService,
            IBusinessEventService businessEventService,
            IEmailService emailService,
            IDocumentService documentService,
            IRazorConverterService razorConverterService)
        {
            _enrolleeService = enrolleeService;
            _agreementService = agreementService;
            _enrolleeSubmissionService = enrolleeSubmissionService;
            _adminService = adminService;
            _businessEventService = businessEventService;
            _emailService = emailService;
            _documentService = documentService;
            _razorConverterService = razorConverterService;
        }

        // GET: api/Enrollees
        /// <summary>
        /// Gets all of the enrollees for the user, or all enrollees if user has ADMIN role.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrollees))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Enrollee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<EnrolleeListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrollees([FromQuery] EnrolleeSearchOptions searchOptions)
        {
            if (User.HasAdminView())
            {
                var notifiedIds = await _enrolleeService.GetNotifiedEnrolleeIdsForAdminAsync(User);
                var enrollees = await _enrolleeService.GetEnrolleesAsync(searchOptions);
                var result = enrollees.Select(e => e.SetNotification(notifiedIds.Contains(e.Id)));
                return Ok(ApiResponse.Result(result));
            }
            else
            {
                var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(User.GetPrimeUserId());
                return Ok(ApiResponse.Result(enrollee == null ? Enumerable.Empty<Enrollee>() : new[] { enrollee }));
            }
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
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeViewModel>> GetEnrolleeById(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId, User.HasAdminView());
            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (!enrollee.PermissionsRecord().ViewableBy(User))
            {
                return Forbid();
            }

            if (User.IsAdmin())
            {
                await _businessEventService.CreateAdminViewEventAsync(enrolleeId, "Admin viewing the current Enrolment");
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
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult<EnrolleeViewModel>> CreateEnrollee(EnrolleeCreatePayload payload)
        {
            if (payload == null || payload.Enrollee == null)
            {
                ModelState.AddModelError("Enrollee", "Could not create an enrollee, the passed in Enrollee cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            if (await _enrolleeService.UserIdExistsAsync(User.GetPrimeUserId()))
            {
                ModelState.AddModelError("Enrollee.UserId", "An enrollee already exists for this User Id, only one enrollee is allowed per User Id.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var createModel = payload.Enrollee;
            createModel.MapConditionalProperties(User);

            if (createModel.IsUnder18())
            {
                return Forbid();
            }

            string filename = null;
            if (!createModel.IsBcServicesCard())
            {
                if (payload.IdentificationDocumentGuid != null)
                {
                    filename = await _documentService.FinalizeDocumentUpload((Guid)payload.IdentificationDocumentGuid, "identification_document");
                    if (string.IsNullOrWhiteSpace(filename))
                    {
                        ModelState.AddModelError("documentGuid", "Identification document could not be created; network error or upload is already submitted");
                        return BadRequest(ApiResponse.BadRequest(ModelState));
                    }
                }
                else
                {
                    ModelState.AddModelError("documentGuid", "Identification Document Guid was not supplied with request; Cannot create enrollee without identification.");
                    return BadRequest(ApiResponse.BadRequest(ModelState));
                }
            }

            var createdEnrolleeId = await _enrolleeService.CreateEnrolleeAsync(createModel);
            var enrollee = await _enrolleeService.GetEnrolleeAsync(createdEnrolleeId);

            if (filename != null)
            {
                await _enrolleeService.CreateIdentificationDocument(enrollee.Id, (Guid)payload.IdentificationDocumentGuid, filename);
            }

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
        /// <param name="enrollee"></param>
        /// <param name="beenThroughTheWizard"></param>
        [HttpPut("{enrolleeId}", Name = nameof(UpdateEnrollee))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEnrollee(int enrolleeId, EnrolleeUpdateModel enrollee, [FromQuery] bool beenThroughTheWizard)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (!record.EditableBy(User))
            {
                return Forbid();
            }

            // If the enrollee is not in the status of 'Editable', it cannot be updated
            if (!(await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.Editable)))
            {
                ModelState.AddModelError("Enrollee.CurrentStatus", "Enrollee can not be updated when the current status is not 'Editable'.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            enrollee.SetTokenProperties(User);

            await _enrolleeService.UpdateEnrolleeAsync(enrolleeId, enrollee, beenThroughTheWizard);

            return NoContent();
        }

        // DELETE: api/Enrollees/5
        /// <summary>
        /// Deletes a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}", Name = nameof(DeleteEnrollee))]
        [Authorize(Policy = Policies.SuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeViewModel>> DeleteEnrollee(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
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
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (!record.ViewableBy(User))
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
        [Authorize(Policy = Policies.ReadonlyAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolleeNote>>> GetAdjudicatorNotes(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            var adjudicationNotes = await _enrolleeService.GetEnrolleeAdjudicatorNotesAsync(enrollee.Id);

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
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeNote>), StatusCodes.Status201Created)]
        public async Task<ActionResult<EnrolleeNote>> CreateAdjudicatorNote(int enrolleeId, FromBodyText note, [FromQuery] bool link)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (string.IsNullOrWhiteSpace(note))
            {
                ModelState.AddModelError("note", "Adjudicator notes can't be null or empty.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());
            var createdAdjudicatorNote = await _enrolleeService.CreateEnrolleeAdjudicatorNoteAsync(enrolleeId, note, admin.Id);

            if (link)
            {
                // Link Adjudicator note to most recent status change on an enrollee if request
                var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
                await _enrolleeService.AddAdjudicatorNoteToReferenceIdAsync(enrollee.CurrentStatus.Id, createdAdjudicatorNote.Id);
            }

            return CreatedAtAction(
                nameof(CreateAdjudicatorNote),
                new { enrolleeId },
                ApiResponse.Result(createdAdjudicatorNote)
            );
        }

        // POST: api/Enrollees/5/status-reference
        /// <summary>
        /// Creates a new Enrolment Status Reference on the enrollee's current status.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/status-reference", Name = nameof(CreateEnrolmentReference))]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeNote>), StatusCodes.Status201Created)]
        public async Task<ActionResult<EnrolleeNote>> CreateEnrolmentReference(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());
            var createdEnrolmentStatusReference = await _enrolleeService.CreateEnrolmentStatusReferenceAsync(enrollee.CurrentStatus.Id, admin.Id);

            return CreatedAtAction(
                nameof(CreateEnrolmentReference),
                new { enrolleeId },
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
        [Authorize(Policy = Policies.Admin)]
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
                ModelState.AddModelError("AccessAgreementNote.EnrolleeId", "Enrollee Id does not match with the payload.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.UnderReview))
            {
                ModelState.AddModelError("Enrollee.CurrentStatus", "Access agreement notes can not be updated when the current status is 'Editable'.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var updatedNote = await _enrolleeService.UpdateEnrolleeNoteAsync(enrolleeId, accessAgreementNote);

            return Ok(ApiResponse.Result(updatedNote));
        }

        // PUT: api/Enrollees/5/adjudicator
        /// <summary>
        /// Add an enrollee's assigned adjudicator.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="adjudicatorId"></param>
        [HttpPut("{enrolleeId}/adjudicator", Name = nameof(SetEnrolleeAdjudicator))]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeViewModel>> SetEnrolleeAdjudicator(int enrolleeId, [FromQuery] int? adjudicatorId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}."));
            }

            Admin admin = (adjudicatorId.HasValue)
                ? await _adminService.GetAdminAsync(adjudicatorId.Value)
                : await _adminService.GetAdminAsync(User.GetPrimeUserId());

            if (admin == null)
            {
                return NotFound(ApiResponse.Message($"Admin not found with id {adjudicatorId.Value}."));
            }

            var updatedEnrollee = await _enrolleeService.UpdateEnrolleeAdjudicator(enrollee.Id, admin.Id);
            await _businessEventService.CreateAdminActionEventAsync(enrolleeId, "Admin claimed enrollee");

            return Ok(ApiResponse.Result(updatedEnrollee));
        }

        // DELETE: api/Enrollees/5/adjudicator
        /// <summary>
        /// Remove an enrollee's assigned adjudicator.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}/adjudicator", Name = nameof(RemoveEnrolleeAdjudicator))]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeViewModel>> RemoveEnrolleeAdjudicator(int enrolleeId)
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

        // GET: api/Enrollees/5/events?businessEventTypeCodes=1&businessEventTypeCodes=2
        /// <summary>
        /// Gets a list of enrollee events.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="businessEventTypeCodes"></param>
        [HttpGet("{enrolleeId}/events", Name = nameof(GetEnrolleeBusinessEvents))]
        [Authorize(Policy = Policies.ReadonlyAdmin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<BusinessEvent>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BusinessEvent>>> GetEnrolleeBusinessEvents(int enrolleeId, [FromQuery] IEnumerable<int> businessEventTypeCodes)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            var events = await _enrolleeService.GetEnrolleeBusinessEventsAsync(enrolleeId, businessEventTypeCodes);

            return Ok(ApiResponse.Result(events));
        }

        // POST: api/Enrollees/5/reminder
        /// <summary>
        /// Send an enrollee a reminder email.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/reminder", Name = nameof(SendEnrolleeReminderEmail))]
        [Authorize(Policy = Policies.ReadonlyAdmin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendEnrolleeReminderEmail(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());
            var username = admin.IDIR.Replace("@idir", "");
            await _emailService.SendReminderEmailAsync(enrollee.Id);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, $"Email reminder sent to Enrollee by {username}");

            return NoContent();
        }

        // POST: api/Enrollees/5/events/email-initiated
        /// <summary>
        /// Logs a business event for email initiated
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/events/email-initiated", Name = nameof(CreateInitiatedEnrolleeEmailEvent))]
        [Authorize(Policy = Policies.ReadonlyAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateInitiatedEnrolleeEmailEvent(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());
            var username = admin.IDIR.Replace("@idir", "");

            await _businessEventService.CreateEmailEventAsync(enrolleeId, $"Email Initiated to Enrollee by {username}");

            return NoContent();
        }

        // POST: api/Enrollees/5/self-declaration-document
        /// <summary>
        /// Create Self Declaration Document Link
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="selfDeclarationDocument"></param>
        [HttpPost("{enrolleeId}/self-declaration-document", Name = nameof(CreateSelfDeclarationDocument))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<SelfDeclarationDocument>> CreateSelfDeclarationDocument(int enrolleeId, SelfDeclarationDocument selfDeclarationDocument)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (!record.EditableBy(User))
            {
                return Forbid();
            }

            var sdd = await _enrolleeService.AddSelfDeclarationDocumentAsync(enrolleeId, selfDeclarationDocument);

            return Ok(ApiResponse.Result(sdd));
        }

        // GET: api/Enrollees/{enrolleeId}/self-declaration-document/{selfDeclarationDocumentId}
        /// <summary>
        /// Get the self Declaration document download token.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="selfDeclarationDocumentId"></param>
        [HttpGet("{enrolleeId}/self-declaration-document/{selfDeclarationDocumentId}", Name = nameof(GetSelfDeclarationDocument))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetSelfDeclarationDocument(int enrolleeId, int selfDeclarationDocumentId)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (!record.ViewableBy(User))
            {
                return Forbid();
            }

            var token = await _documentService.GetDownloadTokenForSelfDeclarationDocument(selfDeclarationDocumentId);

            return Ok(ApiResponse.Result(token));
        }

        // GET: api/Enrollees/{enrolleeId}/identification-document/{identificationDocumentId}
        /// <summary>
        /// Get the Identification Document download token.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="identificationDocumentId"></param>
        [HttpGet("{enrolleeId}/identification-document/{identificationDocumentId}", Name = nameof(GetIdentificationDocument))]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetIdentificationDocument(int enrolleeId, int identificationDocumentId)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (!record.ViewableBy(User))
            {
                return Forbid();
            }

            var token = await _documentService.GetDownloadTokenForIdentificationDocument(identificationDocumentId);

            return Ok(ApiResponse.Result(token));
        }

        // POST: api/enrollees/5/adjudication-documents
        /// <summary>
        /// Creates a new enrollee adjudication document for an enrollee.
        /// </summary>
        /// <param name="documentGuid"></param>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/adjudication-documents", Name = nameof(CreateEnrolleeAdjudicationDocument))]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeAdjudicationDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeAdjudicationDocument>> CreateEnrolleeAdjudicationDocument(int enrolleeId, [FromQuery] Guid documentGuid)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());

            var document = await _enrolleeService.AddEnrolleeAdjudicationDocumentAsync(enrolleeId, documentGuid, admin.Id);
            if (document == null)
            {
                ModelState.AddModelError("documentGuid", "Enrollee Adjudication Document could not be created; network error or upload is already submitted");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            return Ok(ApiResponse.Result(document));
        }

        // GET: api/enrollees/5/adjudication-documents
        /// <summary>
        /// Gets all enrollee adjudication documents for an enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/adjudication-documents", Name = nameof(GetEnrolleeAdjudicationDocuments))]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeAdjudicationDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolleeAdjudicationDocument>>> GetEnrolleeAdjudicationDocuments(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            var documents = await _enrolleeService.GetEnrolleeAdjudicationDocumentsAsync(enrolleeId);

            return Ok(ApiResponse.Result(documents));
        }

        // GET: api/Enrollees/{enrolleeId}/adjudication-documents/{documentId}
        /// <summary>
        /// Get the enrollee adjudication documents download token.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="documentId"></param>
        [HttpGet("{enrolleeId}/adjudication-documents/{documentId}", Name = nameof(GetEnrolleeAdjudicationDocument))]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetEnrolleeAdjudicationDocument(int enrolleeId, int documentId)
        {
            var enrollee = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            var token = await _documentService.GetDownloadTokenForEnrolleeAdjudicationDocument(documentId);

            return Ok(ApiResponse.Result(token));
        }

        // DELETE: api/Enrollees/{enrolleeId}/adjudication-documents/{documentId}
        /// <summary>
        /// Delete the enrollee's adjudication document
        /// </summary>
        /// <param name="documentId"></param>
        [HttpDelete("{enrolleeId}/adjudication-documents/{documentId}", Name = nameof(DeleteEnrolleeAdjudicationDocument))]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeAdjudicationDocument>> DeleteEnrolleeAdjudicationDocument(int documentId)
        {
            var document = await _enrolleeService.GetEnrolleeAdjudicationDocumentAsync(documentId);
            if (document == null)
            {
                return NotFound(ApiResponse.Message($"Document not found with id {documentId}"));
            }

            await _enrolleeService.DeleteEnrolleeAdjudicationDocumentAsync(documentId);

            return Ok(ApiResponse.Result(document));
        }

        // GET: api/Enrollees/{enrolleeId}/current-status
        /// <summary>
        /// Get the enrollees current status
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/current-status", Name = nameof(GetEnrolleeCurrentStatus))]
        [Authorize(Policy = Policies.ReadonlyAdmin)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolmentStatus>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetEnrolleeCurrentStatus(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            var status = await _enrolleeService.GetEnrolleeCurrentStatusAsync(enrolleeId);

            return Ok(ApiResponse.Result(status));
        }

        // POST: api/Enrollees/5/adjudicator-notes/6/notification
        /// <summary>
        /// Creates a new enrollee notification on an enrollee note.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="adjudicatorNoteId"></param>
        /// <param name="assigneeId"></param>
        [HttpPost("{enrolleeId}/adjudicator-notes/{adjudicatorNoteId}/notification", Name = nameof(CreateEnrolleeNotification))]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeNotification>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeNotification>> CreateEnrolleeNotification(int enrolleeId, int adjudicatorNoteId, FromBodyData<int> assigneeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            var note = await _enrolleeService.GetEnrolleeAdjudicatorNoteAsync(enrolleeId, adjudicatorNoteId);
            if (note == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee note not found with id {adjudicatorNoteId}"));
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());
            var notification = await _enrolleeService.CreateEnrolleeNotificationAsync(note.Id, admin.Id, assigneeId);

            return Ok(ApiResponse.Result(notification));
        }

        // DELETE: api/Enrollees/5/adjudicator-notes/6/notification
        /// <summary>
        /// deletes the notification on an enrollees note.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="adjudicatorNoteId"></param>
        [HttpDelete("{enrolleeId}/adjudicator-notes/{adjudicatorNoteId}/notification", Name = nameof(DeleteEnrolleeNotification))]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteEnrolleeNotification(int enrolleeId, int adjudicatorNoteId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            var note = await _enrolleeService.GetEnrolleeAdjudicatorNoteAsync(enrolleeId, adjudicatorNoteId);
            if (note == null || note.EnrolleeNotification == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee note with notification not found with id {adjudicatorNoteId}"));
            }

            await _enrolleeService.RemoveEnrolleeNotificationAsync(note.EnrolleeNotification.Id);

            return Ok();
        }

        // Get: api/Enrollees/5/notifications
        /// <summary>
        /// Get the enrollee note on an enrollee that has a notification  for current admin user.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/notifications", Name = nameof(GetNotifications))]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeNoteViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeNoteViewModel>> GetNotifications(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());

            var notes = await _enrolleeService.GetNotificationsAsync(enrolleeId, admin.Id);

            return Ok(ApiResponse.Result(notes));
        }

        // Delete: api/Enrollees/5/notifications
        /// <summary>
        /// Delete all notifications on an enrollee
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}/notifications", Name = nameof(DeleteEnrolleeNotifications))]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeNoteViewModel>> DeleteEnrolleeNotifications(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            await _enrolleeService.RemoveNotificationsAsync(enrolleeId);

            return Ok();
        }
    }
}

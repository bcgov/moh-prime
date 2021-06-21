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
using Prime.HttpClients.DocumentManagerApiDefinitions;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewEnrollee)]
    public class EnrolleesController : PrimeControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAdminService _adminService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IEmailService _emailService;
        private readonly IDocumentService _documentService;

        public EnrolleesController(
            IEnrolleeService enrolleeService,
            IAdminService adminService,
            IBusinessEventService businessEventService,
            IEmailService emailService,
            IDocumentService documentService)
        {
            _enrolleeService = enrolleeService;
            _adminService = adminService;
            _businessEventService = businessEventService;
            _emailService = emailService;
            _documentService = documentService;
        }

        // GET: api/Enrollees
        /// <summary>
        /// Gets all of the enrollees for the user, or all enrollees if user has ADMIN role.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrollees))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Enrollee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<EnrolleeListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrollees([FromQuery] EnrolleeSearchOptions searchOptions)
        {
            if (User.IsAdministrant())
            {
                var notifiedIds = await _enrolleeService.GetNotifiedEnrolleeIdsForAdminAsync(User);
                var enrollees = await _enrolleeService.GetEnrolleesAsync(searchOptions);
                var result = enrollees.Select(e => e.SetNotification(notifiedIds.Contains(e.Id)));
                return Ok(result);
            }
            else
            {
                var enrollee = await _enrolleeService.GetEnrolleeForUserIdAsync(User.GetPrimeUserId());
                return Ok(enrollee == null ? Enumerable.Empty<Enrollee>() : new[] { enrollee });
            }
        }

        // GET: api/Enrollees/1/adjacent
        /// <summary>
        /// Gets adjacent next and previous enrollee IDs for a given enrolleeId
        /// </summary>
        [HttpGet("{enrolleeId}/adjacent", Name = nameof(GetAdjacentEnrolleeId))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeNavigation>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAdjacentEnrolleeId(int enrolleeId)
        {
            var result = await _enrolleeService.GetAdjacentEnrolleeIdAsync(enrolleeId);
            return Ok(result);
        }

        // GET: api/Enrollees/5
        /// <summary>
        /// Gets a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}", Name = nameof(GetEnrolleeById))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeViewModel>> GetEnrolleeById(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId, User.IsAdministrant());
            if (enrollee == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!enrollee.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            if (User.IsAdministrant())
            {
                await _businessEventService.CreateAdminViewEventAsync(enrolleeId, "Admin viewing the current Enrolment");
            }

            return Ok(enrollee);
        }

        // POST: api/Enrollees
        /// <summary>
        /// Creates a new Enrollee.
        /// </summary>
        [HttpPost(Name = nameof(CreateEnrollee))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult<EnrolleeViewModel>> CreateEnrollee(EnrolleeCreatePayload payload)
        {
            if (payload?.Enrollee == null)
            {
                return BadRequest("Could not create an enrollee, the passed in Enrollee cannot be null.");
            }

            if (await _enrolleeService.UserIdExistsAsync(User.GetPrimeUserId()))
            {
                return BadRequest("An enrollee already exists for this User Id, only one enrollee is allowed per User Id.");
            }

            var createModel = payload.Enrollee;
            createModel.SetPropertiesFromToken(User);

            if (!createModel.Validate(User))
            {
                return BadRequest("One or more Properties did not match the information on the card.");
            }

            string filename = null;
            if (!createModel.IsBcServicesCard())
            {
                if (payload.IdentificationDocumentGuid.HasValue)
                {
                    filename = await _documentService.FinalizeDocumentUpload(payload.IdentificationDocumentGuid.Value, DestinationFolders.IdentificationDocuments);
                    if (string.IsNullOrWhiteSpace(filename))
                    {
                        return BadRequest("Identification document could not be created; network error or upload is already submitted");
                    }
                }
                else
                {
                    return BadRequest("Identification Document Guid was not supplied with request; Cannot create enrollee without identification.");
                }
            }

            var createdEnrolleeId = await _enrolleeService.CreateEnrolleeAsync(createModel);
            var enrollee = await _enrolleeService.GetEnrolleeAsync(createdEnrolleeId);

            if (filename != null)
            {
                await _enrolleeService.CreateIdentificationDocument(enrollee.Id, payload.IdentificationDocumentGuid.Value, filename);
            }

            return CreatedAtAction(
                nameof(GetEnrolleeById),
                new { enrolleeId = createdEnrolleeId },
                enrollee
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
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEnrollee(int enrolleeId, EnrolleeUpdateModel enrollee, [FromQuery] bool beenThroughTheWizard)
        {
            if (enrollee == null)
            {
                return BadRequest("Profile update model cannot be null.");
            }

            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!record.MatchesUserIdOf(User))
            {
                return Forbid();
            }

            enrollee.SetPropertiesFromToken(User);

            if (!enrollee.Validate(User))
            {
                return BadRequest("One or more Properties did not match the information on the card.");
            }

            // If the enrollee is not in the status of 'Editable', it cannot be updated
            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.Editable))
            {
                return BadRequest("Enrollee can not be updated when the current status is not 'Editable'.");
            }

            await _enrolleeService.UpdateEnrolleeAsync(enrolleeId, enrollee, beenThroughTheWizard);

            return NoContent();
        }

        // DELETE: api/Enrollees/5
        /// <summary>
        /// Deletes a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}", Name = nameof(DeleteEnrollee))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeViewModel>> DeleteEnrollee(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }

            await _enrolleeService.DeleteEnrolleeAsync(enrolleeId);

            return Ok(enrollee);
        }

        // GET: api/Enrollees/5/statuses
        /// <summary>
        /// Gets all of the status changes for a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/statuses", Name = nameof(GetEnrolmentStatuses))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolmentStatus>>> GetEnrolmentStatuses(int enrolleeId)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var enrollees = await _enrolleeService.GetEnrolmentStatusesAsync(enrolleeId);

            return Ok(enrollees);
        }

        // GET: api/Enrollees/5/adjudicator-notes
        /// <summary>
        /// Gets all of the adjudicator notes for a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/adjudicator-notes", Name = nameof(GetAdjudicatorNotes))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolleeNote>>> GetAdjudicatorNotes(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            if (enrollee == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }

            var adjudicationNotes = await _enrolleeService.GetEnrolleeAdjudicatorNotesAsync(enrollee.Id);

            return Ok(adjudicationNotes);
        }

        // POST: api/Enrollees/5/adjudicator-notes
        /// <summary>
        /// Creates a new adjudicator note on an enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="note"></param>
        /// <param name="link"></param>
        [HttpPost("{enrolleeId}/adjudicator-notes", Name = nameof(CreateAdjudicatorNote))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeNote>), StatusCodes.Status201Created)]
        public async Task<ActionResult<EnrolleeNote>> CreateAdjudicatorNote(int enrolleeId, FromBodyText note, [FromQuery] bool link)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (string.IsNullOrWhiteSpace(note))
            {
                return BadRequest("Adjudicator notes can't be null or empty.");
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
                createdAdjudicatorNote
            );
        }

        // POST: api/Enrollees/5/status-reference
        /// <summary>
        /// Creates a new Enrolment Status Reference on the enrollee's current status.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/status-reference", Name = nameof(CreateEnrolmentReference))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeNote>), StatusCodes.Status201Created)]
        public async Task<ActionResult<EnrolleeNote>> CreateEnrolmentReference(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());
            var createdEnrolmentStatusReference = await _enrolleeService.CreateEnrolmentStatusReferenceAsync(enrollee.CurrentStatus.Id, admin.Id);

            return CreatedAtAction(
                nameof(CreateEnrolmentReference),
                new { enrolleeId },
                createdEnrolmentStatusReference
            );
        }

        // PUT: api/Enrollees/5/access-agreement-notes
        /// <summary>
        /// Updates an access agreement note.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="accessAgreementNote"></param>
        [HttpPut("{enrolleeId}/access-agreement-notes", Name = nameof(UpdateAccessAgreementNote))]
        [Authorize(Roles = Roles.ManageEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AccessAgreementNote>), StatusCodes.Status200OK)]
        public async Task<ActionResult<AccessAgreementNote>> UpdateAccessAgreementNote(int enrolleeId, AccessAgreementNote accessAgreementNote)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}.");
            }

            if (accessAgreementNote.EnrolleeId != 0 && enrolleeId != accessAgreementNote.EnrolleeId)
            {
                return BadRequest("Enrollee Id does not match with the payload.");
            }

            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.UnderReview))
            {
                return BadRequest("Access agreement notes can not be updated when the current status is 'Editable'.");
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());
            var updatedNote = await _enrolleeService.UpdateEnrolleeNoteAsync(enrolleeId, admin.Id, accessAgreementNote);

            return Ok(updatedNote);
        }

        // PUT: api/Enrollees/5/adjudicator?adjudicatorId=1
        /// <summary>
        /// Set an enrollee's assigned adjudicator.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="adjudicatorId"></param>
        [HttpPut("{enrolleeId}/adjudicator", Name = nameof(SetEnrolleeAdjudicator))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> SetEnrolleeAdjudicator(int enrolleeId, [FromQuery] int adjudicatorId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}.");
            }

            var idir = await _adminService.GetAdminIdirAsync(adjudicatorId);
            if (idir == null)
            {
                return NotFound($"Admin not found with id {adjudicatorId}.");
            }

            await _enrolleeService.UpdateEnrolleeAdjudicator(enrolleeId, adjudicatorId);
            await _businessEventService.CreateAdminActionEventAsync(enrolleeId, "Admin claimed enrollee");

            return Ok(idir);
        }

        // DELETE: api/Enrollees/5/adjudicator
        /// <summary>
        /// Remove an enrollee's assigned adjudicator.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}/adjudicator", Name = nameof(RemoveEnrolleeAdjudicator))]
        [Authorize(Roles = Roles.ManageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> RemoveEnrolleeAdjudicator(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}.");
            }

            await _enrolleeService.UpdateEnrolleeAdjudicator(enrolleeId);
            await _businessEventService.CreateAdminActionEventAsync(enrolleeId, "Admin disclaimed enrollee");

            return NoContent();
        }

        // GET: api/Enrollees/5/events?businessEventTypeCodes=1&businessEventTypeCodes=2
        /// <summary>
        /// Gets a list of enrollee events.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="businessEventTypeCodes"></param>
        [HttpGet("{enrolleeId}/events", Name = nameof(GetEnrolleeBusinessEvents))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<BusinessEvent>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BusinessEvent>>> GetEnrolleeBusinessEvents(int enrolleeId, [FromQuery] IEnumerable<int> businessEventTypeCodes)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }

            var events = await _enrolleeService.GetEnrolleeBusinessEventsAsync(enrolleeId, businessEventTypeCodes);

            return Ok(events);
        }

        // POST: api/Enrollees/5/reminder
        /// <summary>
        /// Send an enrollee a reminder email.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/reminder", Name = nameof(SendEnrolleeReminderEmail))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendEnrolleeReminderEmail(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
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
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateInitiatedEnrolleeEmailEvent(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
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
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<SelfDeclarationDocument>> CreateSelfDeclarationDocument(int enrolleeId, SelfDeclarationDocument selfDeclarationDocument)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!record.MatchesUserIdOf(User))
            {
                return Forbid();
            }

            var sdd = await _enrolleeService.AddSelfDeclarationDocumentAsync(enrolleeId, selfDeclarationDocument);

            return Ok(sdd);
        }

        // GET: api/Enrollees/{enrolleeId}/self-declaration-document/{selfDeclarationDocumentId}
        /// <summary>
        /// Get the self Declaration document download token.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="selfDeclarationDocumentId"></param>
        [HttpGet("{enrolleeId}/self-declaration-document/{selfDeclarationDocumentId}", Name = nameof(GetSelfDeclarationDocument))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetSelfDeclarationDocument(int enrolleeId, int selfDeclarationDocumentId)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var token = await _documentService.GetDownloadTokenForSelfDeclarationDocument(selfDeclarationDocumentId);

            return Ok(token);
        }

        // GET: api/Enrollees/{enrolleeId}/identification-document/{identificationDocumentId}
        /// <summary>
        /// Get the Identification Document download token.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="identificationDocumentId"></param>
        [HttpGet("{enrolleeId}/identification-document/{identificationDocumentId}", Name = nameof(GetIdentificationDocument))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetIdentificationDocument(int enrolleeId, int identificationDocumentId)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var token = await _documentService.GetDownloadTokenForIdentificationDocument(identificationDocumentId);

            return Ok(token);
        }

        // POST: api/enrollees/5/adjudication-documents
        /// <summary>
        /// Creates a new enrollee adjudication document for an enrollee.
        /// </summary>
        /// <param name="documentGuid"></param>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/adjudication-documents", Name = nameof(CreateEnrolleeAdjudicationDocument))]
        [Authorize(Roles = Roles.ApproveEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeAdjudicationDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeAdjudicationDocument>> CreateEnrolleeAdjudicationDocument(int enrolleeId, [FromQuery] Guid documentGuid)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());

            var document = await _enrolleeService.AddEnrolleeAdjudicationDocumentAsync(enrolleeId, documentGuid, admin.Id);
            if (document == null)
            {
                return BadRequest("Enrollee Adjudication Document could not be created; network error or upload is already submitted");
            }

            return Ok(document);
        }

        // GET: api/enrollees/5/adjudication-documents
        /// <summary>
        /// Gets all enrollee adjudication documents for an enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/adjudication-documents", Name = nameof(GetEnrolleeAdjudicationDocuments))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeAdjudicationDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolleeAdjudicationDocument>>> GetEnrolleeAdjudicationDocuments(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }

            var documents = await _enrolleeService.GetEnrolleeAdjudicationDocumentsAsync(enrolleeId);

            return Ok(documents);
        }

        // GET: api/Enrollees/{enrolleeId}/adjudication-documents/{documentId}
        /// <summary>
        /// Get the enrollee adjudication documents download token.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="documentId"></param>
        [HttpGet("{enrolleeId}/adjudication-documents/{documentId}", Name = nameof(GetEnrolleeAdjudicationDocument))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetEnrolleeAdjudicationDocument(int enrolleeId, int documentId)
        {
            var enrollee = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (enrollee == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }

            var token = await _documentService.GetDownloadTokenForEnrolleeAdjudicationDocument(documentId);

            return Ok(token);
        }

        // DELETE: api/Enrollees/{enrolleeId}/adjudication-documents/{documentId}
        /// <summary>
        /// Delete the enrollee's adjudication document
        /// </summary>
        /// <param name="documentId"></param>
        [HttpDelete("{enrolleeId}/adjudication-documents/{documentId}", Name = nameof(DeleteEnrolleeAdjudicationDocument))]
        [Authorize(Roles = Roles.ApproveEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeAdjudicationDocument>> DeleteEnrolleeAdjudicationDocument(int documentId)
        {
            var document = await _enrolleeService.GetEnrolleeAdjudicationDocumentAsync(documentId);
            if (document == null)
            {
                return NotFound($"Document not found with id {documentId}");
            }

            await _enrolleeService.DeleteEnrolleeAdjudicationDocumentAsync(documentId);

            return Ok(document);
        }

        // GET: api/Enrollees/{enrolleeId}/current-status
        /// <summary>
        /// Get the enrollees current status
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/current-status", Name = nameof(GetEnrolleeCurrentStatus))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolmentStatus>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetEnrolleeCurrentStatus(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (enrollee == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }

            var status = await _enrolleeService.GetEnrolleeCurrentStatusAsync(enrolleeId);

            return Ok(status);
        }

        // POST: api/Enrollees/5/adjudicator-notes/6/notification
        /// <summary>
        /// Creates a new enrollee notification on an enrollee note.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="adjudicatorNoteId"></param>
        /// <param name="assigneeId"></param>
        [HttpPost("{enrolleeId}/adjudicator-notes/{adjudicatorNoteId}/notification", Name = nameof(CreateEnrolleeNotification))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeNotification>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeNotification>> CreateEnrolleeNotification(int enrolleeId, int adjudicatorNoteId, FromBodyData<int> assigneeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            var note = await _enrolleeService.GetEnrolleeAdjudicatorNoteAsync(enrolleeId, adjudicatorNoteId);
            if (note == null)
            {
                return NotFound($"Enrollee note not found with id {adjudicatorNoteId}");
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());
            var notification = await _enrolleeService.CreateEnrolleeNotificationAsync(note.Id, admin.Id, assigneeId);

            return Ok(notification);
        }

        // DELETE: api/Enrollees/5/adjudicator-notes/6/notification
        /// <summary>
        /// deletes the notification on an enrollees note.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="adjudicatorNoteId"></param>
        [HttpDelete("{enrolleeId}/adjudicator-notes/{adjudicatorNoteId}/notification", Name = nameof(DeleteEnrolleeNotification))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteEnrolleeNotification(int enrolleeId, int adjudicatorNoteId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            var note = await _enrolleeService.GetEnrolleeAdjudicatorNoteAsync(enrolleeId, adjudicatorNoteId);
            if (note == null || note.EnrolleeNotification == null)
            {
                return NotFound($"Enrollee note with notification not found with id {adjudicatorNoteId}");
            }

            await _enrolleeService.RemoveEnrolleeNotificationAsync(note.EnrolleeNotification.Id);

            return Ok();
        }

        // Get: api/Enrollees/5/notifications
        /// <summary>
        /// Get the enrollee note on an enrollee that has a notification for current admin user.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/notifications", Name = nameof(GetNotifications))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeNoteViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeNoteViewModel>> GetNotifications(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());

            var notes = await _enrolleeService.GetNotificationsAsync(enrolleeId, admin.Id);

            return Ok(notes);
        }

        // Delete: api/Enrollees/5/notifications
        /// <summary>
        /// Delete all notifications on an enrollee
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}/notifications", Name = nameof(DeleteEnrolleeNotifications))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeNoteViewModel>> DeleteEnrolleeNotifications(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }

            await _enrolleeService.RemoveNotificationsAsync(enrolleeId);

            return Ok();
        }

        // GET: api/Enrollees/emails
        /// <summary>
        /// Gets all of the enrollee emails that match the type
        /// </summary>
        /// <param name="bulkEmailType"></param>
        [HttpGet("emails", Name = nameof(GetEnrolleeEmails))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<string>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrolleeEmails([FromQuery] BulkEmailType bulkEmailType)
        {
            var emails = await _enrolleeService.GetEnrolleeEmails(bulkEmailType);
            return Ok(emails);
        }
    }
}

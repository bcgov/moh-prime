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
using Prime.ViewModels.Plr;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/enrollees")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewEnrollee)]
    public class EnrolleesExtendedController : PrimeControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAdminService _adminService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IEmailService _emailService;
        private readonly IDocumentService _documentService;
        private readonly IPlrProviderService _plrProviderService;

        public EnrolleesExtendedController(
            IEnrolleeService enrolleeService,
            IAdminService adminService,
            IBusinessEventService businessEventService,
            IEmailService emailService,
            IPlrProviderService plrProviderService,
            IDocumentService documentService)
        {
            _enrolleeService = enrolleeService;
            _adminService = adminService;
            _businessEventService = businessEventService;
            _emailService = emailService;
            _documentService = documentService;
            _plrProviderService = plrProviderService;
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

        // GET: api/Enrollees/5/statuses
        /// <summary>
        /// Gets all of the status changes for a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/statuses", Name = nameof(GetEnrolmentStatuses))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<EnrolmentStatus>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrolmentStatuses(int enrolleeId)
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
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<EnrolleeNoteViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAdjudicatorNotes(int enrolleeId)
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
        public async Task<ActionResult> CreateAdjudicatorNote(int enrolleeId, FromBodyText note, [FromQuery] bool link)
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolmentStatusReference>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateEnrolmentReference(int enrolleeId)
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
        public async Task<ActionResult> UpdateAccessAgreementNote(int enrolleeId, AccessAgreementNote accessAgreementNote)
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
        public async Task<ActionResult> GetEnrolleeBusinessEvents(int enrolleeId, [FromQuery] IEnumerable<int> businessEventTypeCodes)
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
        public async Task<ActionResult> CreateSelfDeclarationDocument(int enrolleeId, SelfDeclarationDocument selfDeclarationDocument)
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
        public async Task<ActionResult> GetSelfDeclarationDocument(int enrolleeId, int selfDeclarationDocumentId)
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
        public async Task<ActionResult> GetIdentificationDocument(int enrolleeId, int identificationDocumentId)
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
        public async Task<ActionResult> CreateEnrolleeAdjudicationDocument(int enrolleeId, [FromQuery] Guid documentGuid)
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
        public async Task<ActionResult> GetEnrolleeAdjudicationDocuments(int enrolleeId)
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
        public async Task<ActionResult> GetEnrolleeAdjudicationDocument(int enrolleeId, int documentId)
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
        public async Task<ActionResult> DeleteEnrolleeAdjudicationDocument(int documentId)
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
        public async Task<ActionResult> GetEnrolleeCurrentStatus(int enrolleeId)
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
        public async Task<ActionResult> CreateEnrolleeNotification(int enrolleeId, int adjudicatorNoteId, FromBodyData<int> assigneeId)
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
        public async Task<ActionResult> GetNotifications(int enrolleeId)
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
        public async Task<ActionResult> DeleteEnrolleeNotifications(int enrolleeId)
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

        // GET: api/Enrollees/1/qrcode
        /// <summary>
        /// Gets and Enrollee's Verifiable Credential qrcode invitation
        /// </summary>
        [HttpGet("{enrolleeId}/qrcode", Name = nameof(GetQrCode))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetQrCode(int enrolleeId)
        {
            var result = await _enrolleeService.GetCredentialAsync(enrolleeId);
            return Ok(result?.Base64QRCode);
        }

        // GET: api/Enrollees/5/plrs
        /// <summary>
        /// Gets all PLR data matching the Enrollee's License Number(s)
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/plrs", Name = nameof(GetPlrData))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<PlrViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPlrData(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }

            var certifications = await _enrolleeService.GetCertificationsAsync(enrolleeId);

            return Ok(await _plrProviderService.GetMatchingPlrDataAsync(certifications.Select(c => c.LicenseNumber)));
        }
    }
}

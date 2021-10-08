using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;
using Prime.HttpClients.DocumentManagerApiDefinitions;
using Prime.ViewModels.Plr;

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
        private readonly IPlrProviderService _plrProviderService;

        public EnrolleesController(
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

        // GET: api/enrollees
        /// <summary>
        /// Gets all of the enrollees.
        /// </summary>
        [HttpGet(Name = nameof(GetEnrollees))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<EnrolleeListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrollees([FromQuery] EnrolleeSearchOptions searchOptions)
        {
            var notifiedIds = await _enrolleeService.GetNotifiedEnrolleeIdsForAdminAsync(User);
            var enrollees = await _enrolleeService.GetEnrolleesAsync(searchOptions);

            foreach (var enrollee in enrollees)
            {
                enrollee.HasNotification = notifiedIds.Contains(enrollee.Id);
            }

            return Ok(enrollees);
        }

        // POST: api/enrollees
        /// <summary>
        /// Creates a new Enrollee.
        /// </summary>
        [HttpPost(Name = nameof(CreateEnrollee))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateEnrollee(EnrolleeCreatePayload payload)
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

        // GET: api/enrollees/5
        /// <summary>
        /// Gets a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId:int}", Name = nameof(GetEnrolleeById))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrolleeById(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId, User.IsAdministrant());
            if (enrollee == null)
            {
                return NotFound($"Enrollee not found with ID {enrolleeId}");
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

        // GET: api/enrollees/b529e73f-8dbe-4868-b672-65bb14412699
        /// <summary>
        /// Gets a specific Enrollee by User ID.
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet("{userId:guid}", Name = nameof(GetEnrolleeByUserId))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        public async Task<ActionResult> GetEnrolleeByUserId(Guid userId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeStubAsync(userId);
            if (enrollee == null)
            {
                return NotFound($"Enrollee not found with User ID {userId}");
            }
            if (!enrollee.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            return RedirectToAction(nameof(GetEnrolleeById), new { enrolleeId = enrollee.Id });
        }

        // PUT: api/enrollees/5
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
        public async Task<ActionResult> UpdateEnrollee(int enrolleeId, EnrolleeUpdateModel enrollee, [FromQuery] bool beenThroughTheWizard)
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

        // DELETE: api/enrollees/5
        /// <summary>
        /// Deletes a specific Enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}", Name = nameof(DeleteEnrollee))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteEnrollee(int enrolleeId)
        {
            await _enrolleeService.DeleteEnrolleeAsync(enrolleeId);

            return NoContent();
        }


        // --------- New Stuff --------------------------------------------------------------------------------------------------------------------------------


        // GET: api/enrollees/5/access-agreement-notes
        /// <summary>
        /// Gets an Enrollee's Access Agreement note.
        /// </summary>
        /// <param name="enrolleeId"></param>
        // URL based on existing PUT
        [HttpGet("{enrolleeId}/access-agreement-notes", Name = nameof(GetAccessAgreementNote))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AccessAgreementNoteViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAccessAgreementNote(int enrolleeId)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}.");
            }

            return Ok(await _enrolleeService.GetAccessAgreementNoteAsync(enrolleeId));
        }

        // GET: api/enrollees/5/care-settings
        /// <summary>
        /// Gets an Enrollee's Care Setting(s) and/or Health Authorities (as applicable).
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/care-settings", Name = nameof(GetCareSettingCodes))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<CareSettingViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetCareSettingCodes(int enrolleeId)
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

            return Ok(await _enrolleeService.GetCareSettingsAsync(enrolleeId));
        }

        // GET: api/enrollees/5/certifications
        /// <summary>
        /// Gets an Enrollee's Certifications.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/certifications", Name = nameof(GetCertifications))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<CertificationViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetCertifications(int enrolleeId)
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

            return Ok(await _enrolleeService.GetCertificationsAsync(enrolleeId));
        }

        // GET: api/enrollees/5/remote-users
        /// <summary>
        /// Gets an Enrollee's Remote Users.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/remote-users", Name = nameof(GetEnrolleeRemoteUsers))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<EnrolleeRemoteUserViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetEnrolleeRemoteUsers(int enrolleeId)
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

            return Ok(await _enrolleeService.GetEnrolleeRemoteUsersAsync(enrolleeId));
        }

        // GET: api/enrollees/5/obo-sites
        /// <summary>
        /// Gets an Enrollee's Obo Sites.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/obo-sites", Name = nameof(GetOboSites))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<OboSiteViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOboSites(int enrolleeId)
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

            return Ok(await _enrolleeService.GetOboSitesAsync(enrolleeId));
        }

        // GET: api/enrollees/5/remote-locations
        /// <summary>
        /// Gets an Enrollee's Remote Access Locations.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/remote-locations", Name = nameof(GetRemoteAccessLocations))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<RemoteAccessLocationViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetRemoteAccessLocations(int enrolleeId)
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

            return Ok(await _enrolleeService.GetRemoteAccessLocationsAsync(enrolleeId));
        }

        // GET: api/enrollees/5/remote-sites
        /// <summary>
        /// Gets an Enrollee's Remote Access Sites.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/remote-sites", Name = nameof(GetRemoteAccessSites))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<RemoteAccessSiteViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetRemoteAccessSites(int enrolleeId)
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

            return Ok(await _enrolleeService.GetRemoteAccessSitesAsync(enrolleeId));
        }

        // GET: api/enrollees/5/self-declarations
        /// <summary>
        /// Gets an Enrollee's Self Declarations.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/self-declarations", Name = nameof(GetSelfDeclarations))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<SelfDeclarationViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSelfDeclarations(int enrolleeId)
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

            return Ok(await _enrolleeService.GetSelfDeclarationsAsync(enrolleeId));
        }

        // GET: api/enrollees/5/self-declarations/documents
        /// <summary>
        /// Gets an Enrollee's Self Declaration Documents.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/self-declarations/documents", Name = nameof(GetSelfDeclarationDocuments))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<SelfDeclarationDocumentViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSelfDeclarationDocuments(int enrolleeId)
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

            return Ok(await _enrolleeService.GetSelfDeclarationDocumentsAsync(enrolleeId));
        }

        // POST: api/Enrollees/5/absences
        /// <summary>
        /// Creates a new enrollee absence.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="createModel"></param>
        [HttpPost("{enrolleeId}/absences", Name = nameof(CreateEnrolleeAbsence))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateEnrolleeAbsence(int enrolleeId, EnrolleeAbsenceViewModel createModel)
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

            var currentOrFutureAbsences = await _enrolleeService.GetEnrolleeAbsencesAsync(enrolleeId, false);
            if (currentOrFutureAbsences.Any())
            {
                return BadRequest("Cannot Create Enrollee Absence when a current or future one exists.");
            }

            await _enrolleeService.CreateEnrolleeAbsenceAsync(enrolleeId, createModel);

            return NoContent();
        }

        // GET: api/Enrollees/5/absences?includesPast=false
        /// <summary>
        /// Gets enrollee absences
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="includesPast"></param>
        [HttpGet("{enrolleeId}/absences", Name = nameof(GetAbsences))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<EnrolleeAbsenceViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAbsences(int enrolleeId, [FromQuery] bool includesPast)
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

            var absences = await _enrolleeService.GetEnrolleeAbsencesAsync(enrolleeId, includesPast);

            return Ok(absences);
        }

        // GET: api/Enrollees/5/absences/current
        /// <summary>
        /// Gets your current absence
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/absences/current", Name = nameof(GetCurrentAbsence))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeAbsenceViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetCurrentAbsence(int enrolleeId)
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

            var absence = await _enrolleeService.GetCurrentEnrolleeAbsenceAsync(enrolleeId);
            return Ok(absence);
        }

        // PUT: api/Enrollees/5/absences/current/end
        /// <summary>
        /// Ends an current enrollee absence.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPut("{enrolleeId}/absences/current/end", Name = nameof(EndCurrentEnrolleeAbsence))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> EndCurrentEnrolleeAbsence(int enrolleeId)
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

            await _enrolleeService.EndCurrentEnrolleeAbsenceAsync(enrolleeId);

            return NoContent();
        }

        // DELETE: api/Enrollees/5/absences/1
        /// <summary>
        /// Deletes a specific Enrollee absence.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="absenceId"></param>
        [HttpDelete("{enrolleeId}/absences/{absenceId}", Name = nameof(DeleteFutureEnrolleeAbsence))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteFutureEnrolleeAbsence(int enrolleeId, int absenceId)
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

            await _enrolleeService.DeleteFutureEnrolleeAbsenceAsync(enrolleeId, absenceId);

            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels.PaperEnrollees;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/enrollees")]
    [ApiController]
    public class EnrolleePaperSubmissionsController : PrimeControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IEmailService _emailService;
        private readonly IEnrolleePaperSubmissionService _enrolleePaperSubmissionService;
        private readonly IEnrolleeService _enrolleeService;

        public EnrolleePaperSubmissionsController(
            IAdminService adminService,
            IEmailService emailService,
            IEnrolleePaperSubmissionService enrolleePaperSubmissionService,
            IEnrolleeService enrolleeService
        )
        {
            _adminService = adminService;
            _emailService = emailService;
            _enrolleePaperSubmissionService = enrolleePaperSubmissionService;
            _enrolleeService = enrolleeService;
        }

        // POST: api/enrollees/paper-submissions
        /// <summary>
        /// Creates a new Enrollee Paper Submission.
        /// </summary>
        [HttpPost("paper-submissions", Name = nameof(CreateEnrolleePaperSubmission))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Enrollee>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateEnrolleePaperSubmission(PaperEnrolleeDemographicViewModel payload)
        {
            var createdEnrollee = await _enrolleePaperSubmissionService.CreateEnrolleeAsync(payload);

            return CreatedAtAction(
                nameof(EnrolleesController.GetEnrolleeById),
                "enrollees",
                new { enrolleeId = createdEnrollee.Id },
                createdEnrollee
            );
        }

        // PUT: api/enrollees/5/paper-submissions/agreement
        /// <summary>
        /// Updates a Paper Submission's Agreement.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/agreement", Name = nameof(UpdateEnrolleePaperSubmissionAgreement))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionAgreement(int enrolleeId, PaperEnrolleeAgreementViewModel payload)
        {
            if (!await _enrolleePaperSubmissionService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleePaperSubmissionService.UpdateAgreementAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/care-settings
        /// <summary>
        /// Updates a Paper Submission's Care Settings.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/care-settings", Name = nameof(UpdateEnrolleePaperSubmissionCareSettings))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionCareSettings(int enrolleeId, PaperEnrolleeCareSettingViewModel payload)
        {
            if (!await _enrolleePaperSubmissionService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleePaperSubmissionService.UpdateCareSettingsAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/certifications
        /// <summary>
        /// Updates a Paper Submission's Certifications.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/certifications", Name = nameof(UpdateEnrolleePaperSubmissionCertifications))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionCertifications(int enrolleeId, ICollection<PaperEnrolleeCertificationViewModel> payload)
        {
            if (!await _enrolleePaperSubmissionService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleePaperSubmissionService.UpdateCertificationsAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/device-provider
        /// <summary>
        /// Updates a Paper Submission's Device Provider Informaion.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/device-provider", Name = nameof(UpdateEnrolleePaperSubmissionDeviceProvider))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionDeviceProvider(int enrolleeId, FromBodyText deviceProviderIdentifier)
        {
            if (!await _enrolleePaperSubmissionService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleePaperSubmissionService.UpdateDeviceProviderAsync(enrolleeId, deviceProviderIdentifier);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/demographics
        /// <summary>
        /// Updates a Paper Submission's demographic information.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/demographics", Name = nameof(UpdateEnrolleePaperSubmissionDemographics))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionDemographics(int enrolleeId, PaperEnrolleeDemographicViewModel payload)
        {
            if (!await _enrolleePaperSubmissionService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleePaperSubmissionService.UpdateDemographicsAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/obo-sites
        /// <summary>
        /// Updates a Paper Submission's OBO Sites.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/obo-sites", Name = nameof(UpdateEnrolleePaperSubmissionOboSites))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionOboSites(int enrolleeId, IEnumerable<PaperEnrolleeOboSiteViewModel> payload)
        {
            if (!await _enrolleePaperSubmissionService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleePaperSubmissionService.UpdateOboSitesAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/self-declarations
        /// <summary>
        /// Updates a Paper Submission's Self Declaration information.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/self-declarations", Name = nameof(UpdateEnrolleePaperSubmissionSelfDeclarations))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionSelfDeclarations(int enrolleeId, IEnumerable<PaperEnrolleeSelfDeclarationViewModel> payload)
        {
            if (!await _enrolleePaperSubmissionService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleePaperSubmissionService.UpdateSelfDeclarationsAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/documents
        /// <summary>
        /// Updates a Paper Submission's Documents.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/documents", Name = nameof(UpdateEnrolleePaperSubmissionDocuments))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionDocuments(int enrolleeId, IEnumerable<PaperEnrolleeDocumentViewModel> payload)
        {
            if (!await _enrolleePaperSubmissionService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }
            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());

            await _enrolleePaperSubmissionService.AddEnrolleeAdjudicationDocumentsAsync(enrolleeId, admin.Id, payload);

            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/profile-completed
        /// <summary>
        /// Sets the Paper Submission's profile as "completed", allowing frontend and backend behavioural changes.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/profile-completed", Name = nameof(SetEnrolleePaperSubmissionProfileCompleted))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> SetEnrolleePaperSubmissionProfileCompleted(int enrolleeId)
        {
            if (!await _enrolleePaperSubmissionService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleePaperSubmissionService.SetProfileCompletedAsync(enrolleeId);

            return Ok();
        }

        // GET: api/enrollees/5/paper-submissions/documents
        /// <summary>
        /// Gets all enrollee adjudication documents for a paper enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/paper-submissions/documents", Name = nameof(GetAdjudicationDocuments))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeAdjudicationDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAdjudicationDocuments(int enrolleeId)
        {
            if (!await _enrolleePaperSubmissionService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            var documents = await _enrolleePaperSubmissionService.GetEnrolleeAdjudicationDocumentsAsync(enrolleeId);

            return Ok(documents);
        }

        // POST: api/enrollees/5/paper-submissions/finalize
        /// <summary>
        /// Finalizes a Paper Submission.
        /// </summary>
        [HttpPost("{enrolleeId}/paper-submissions/finalize", Name = nameof(FinalizeEnrolleePaperSubmission))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> FinalizeEnrolleePaperSubmission(int enrolleeId)
        {
            if (!await _enrolleePaperSubmissionService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleePaperSubmissionService.FinalizeSubmissionAsync(enrolleeId);
            await _emailService.SendPaperEnrolmentSubmissionEmailAsync(enrolleeId);

            return Ok();
        }

        // HEAD: api/enrollees/paper-submissions?dateOfBirth=1977-09-22
        /// <summary>
        /// Checks if there are any unclaimed paper Enrollees submissions with the supplied date of birth.
        /// </summary>
        /// <param name="dateOfBirth"></param>
        [HttpHead("paper-submissions", Name = nameof(CheckForMatchingPaperSubmission))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CheckForMatchingPaperSubmission([FromQuery] DateTime dateOfBirth)
        {
            if (await _enrolleePaperSubmissionService.MatchingSubmissionExistsAsync(dateOfBirth))
            {
                return Ok();
            }

            return NotFound();
        }

        // PUT: api/enrollees/5/linked-gpid
        /// <summary>
        /// User supplied GPID to match with a previously submitted Paper Enrolment.
        /// Cannot set a linked GPID on Paper Submissions or on Enrollees already linked to a Paper Submission.
        /// </summary>
        [HttpPut("{enrolleeId}/linked-gpid", Name = nameof(CreateOrUpdateLinkedGpid))]
        [Authorize(Roles = Roles.TriageEnrollee + "," + Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateOrUpdateLinkedGpid(int enrolleeId, FromBodyText gpid)
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

            if (await _enrolleePaperSubmissionService.SetLinkedGpidAsync(enrolleeId, gpid))
            {
                return NoContent();
            }

            return Conflict($"Could not create/update linked GPID. Enrollee with id {enrolleeId} is either a Paper Submission or is already linked to a Paper Submission.");
        }

        // GET: api/enrollees/5/linked-gpid
        /// <summary>
        /// Gets the linked GPID
        /// </summary>
        [HttpGet("{enrolleeId}/linked-gpid", Name = nameof(GetLinkedGpid))]
        [Authorize(Roles = Roles.TriageEnrollee + "," + Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetLinkedGpid(int enrolleeId)
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

            return Ok(await _enrolleePaperSubmissionService.GetLinkedGpidAsync(enrolleeId));
        }
    }
}

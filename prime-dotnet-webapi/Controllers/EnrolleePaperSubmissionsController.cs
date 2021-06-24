using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels.PaperEnrollees;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/enrollees")]
    [ApiController]
    [Authorize(Roles = Roles.TriageEnrollee)]
    public class EnrolleePaperSubmissionsController : PrimeControllerBase
    {

        private readonly IEnrolleePaperSubmissionService _enrolleeService;
        private readonly IAdminService _adminService;

        public EnrolleePaperSubmissionsController(
            IEnrolleePaperSubmissionService enrolleeService,
            IAdminService adminService
        )
        {
            _enrolleeService = enrolleeService;
            _adminService = adminService;
        }

        // POST: api/enrollees/paper-submissions
        /// <summary>
        /// Creates a new Enrollee Paper Submission.
        /// </summary>
        [HttpPost("paper-submissions", Name = nameof(CreateEnrolleePaperSubmission))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Enrollee>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateEnrolleePaperSubmission(PaperEnrolleeDemographicViewModel payload)
        {
            var createdEnrollee = await _enrolleeService.CreateEnrolleeAsync(payload);

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionAgreement(int enrolleeId, PaperEnrolleeAgreementViewModel payload)
        {
            if (!await _enrolleeService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleeService.UpdateAgreementAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/care-settings
        /// <summary>
        /// Updates a Paper Submission's Care Settings.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/care-settings", Name = nameof(UpdateEnrolleePaperSubmissionCareSettings))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionCareSettings(int enrolleeId, PaperEnrolleeCareSettingViewModel payload)
        {
            if (!await _enrolleeService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleeService.UpdateCareSettingsAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/certifications
        /// <summary>
        /// Updates a Paper Submission's Certifications.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/certifications", Name = nameof(UpdateEnrolleePaperSubmissionCertifications))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionCertifications(int enrolleeId, ICollection<PaperEnrolleeCertificationViewModel> payload)
        {
            if (!await _enrolleeService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleeService.UpdateCertificationsAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/demographics
        /// <summary>
        /// Updates a Paper Submission's demographic information.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/demographics", Name = nameof(UpdateEnrolleePaperSubmissionDemographics))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionDemographics(int enrolleeId, PaperEnrolleeDemographicViewModel payload)
        {
            if (!await _enrolleeService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleeService.UpdateDemographicsAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/obo-sites
        /// <summary>
        /// Updates a Paper Submission's OBO Sites.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/obo-sites", Name = nameof(UpdateEnrolleePaperSubmissionOboSites))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionOboSites(int enrolleeId, IEnumerable<PaperEnrolleeOboSiteViewModel> payload)
        {
            if (!await _enrolleeService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleeService.UpdateOboSitesAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/self-declarations
        /// <summary>
        /// Updates a Paper Submission's Self Declaration information.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/self-declarations", Name = nameof(UpdateEnrolleePaperSubmissionSelfDeclarations))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionSelfDeclarations(int enrolleeId, IEnumerable<PaperEnrolleeSelfDeclarationViewModel> payload)
        {
            if (!await _enrolleeService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleeService.UpdateSelfDeclarationsAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/documents
        /// <summary>
        /// Updates a Paper Submission's Documents.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/documents", Name = nameof(UpdateEnrolleePaperSubmissionDocuments))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionDocuments(int enrolleeId, IEnumerable<Guid> payload)
        {
            if (!await _enrolleeService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }
            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());

            await _enrolleeService.AddEnrolleeAdjudicationDocumentsAsync(enrolleeId, admin.Id, payload);

            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/profile-completed
        /// <summary>
        /// Sets the Paper Submission's profile as "completed", allowing frontend and backend behavioural changes.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/profile-completed", Name = nameof(SetEnrolleePaperSubmissionProfileCompleted))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> SetEnrolleePaperSubmissionProfileCompleted(int enrolleeId)
        {
            if (!await _enrolleeService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleeService.SetProfileCompletedAsync(enrolleeId);

            return Ok();
        }

        // GET: api/enrollees/5/paper-submissions/documents
        /// <summary>
        /// Gets all enrollee adjudication documents for a paper enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/paper-submissions/documents", Name = nameof(GetAdjudicationDocuments))]
        [Authorize(Roles = Roles.ViewEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeAdjudicationDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnrolleeAdjudicationDocument>>> GetAdjudicationDocuments(int enrolleeId)
        {
            if (!await _enrolleeService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            var documents = await _enrolleeService.GetEnrolleeAdjudicationDocumentsAsync(enrolleeId);

            return Ok(documents);
        }

        // POST: api/enrollees/5/paper-submissions/finalize
        /// <summary>
        /// Finalizes a Paper Submission.
        /// </summary>
        [HttpPost("{enrolleeId}/paper-submissions/finalize", Name = nameof(FinalizeEnrolleePaperSubmission))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> FinalizeEnrolleePaperSubmission(int enrolleeId)
        {
            if (!await _enrolleeService.PaperSubmissionIsEditableAsync(enrolleeId))
            {
                return NotFound($"No Editable Paper Submission found with Enrollee Id {enrolleeId}");
            }

            await _enrolleeService.FinailizeSubmissionAsync(enrolleeId);

            return Ok();
        }
    }
}

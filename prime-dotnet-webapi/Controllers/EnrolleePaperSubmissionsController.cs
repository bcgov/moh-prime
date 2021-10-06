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
    public class EnrolleePaperSubmissionsController : PrimeControllerBase
    {
        private readonly IEnrolleePaperSubmissionService _enrolleePaperSubmissionService;
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAdminService _adminService;

        public EnrolleePaperSubmissionsController(
            IEnrolleePaperSubmissionService enrolleePaperSubmissionService,
            IEnrolleeService enrolleeService,
            IAdminService adminService
        )
        {
            _enrolleePaperSubmissionService = enrolleePaperSubmissionService;
            _enrolleeService = enrolleeService;
            _adminService = adminService;
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
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
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

            return Ok();
        }

        // HEAD: api/Enrollees/paper-submissions?dateOfBirth=1
        /// <summary>
        /// Gets all paper enrollees and checks whether or not there is a match in dateOfBirth with the current logged on enrollee.
        /// </summary>
        /// <param name="dateOfBirth"></param>
        [HttpHead("paper-submissions")]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPotentialPaperEnrolleeReturneeStatus([FromQuery] DateTime dateOfBirth)
        {
            var result = await _enrolleePaperSubmissionService.PotentialReturneeExistsAsync(dateOfBirth);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        // POST: api/Enrollees/5/potential-paper-enrollee
        /// <summary>
        /// Creates a new Enrollee who may have a a previous paper enrolment.
        /// </summary>
        [HttpPost("{enrolleeId}/potential-paper-enrollee", Name = nameof(CreateLinkWithPotentialPaperEnrollee))]
        [Authorize(Roles = Roles.TriageEnrollee + "," + Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateLinkWithPotentialPaperEnrollee(int enrolleeId, EnrolleeLinkedEnrolmentViewModel payload)
        {
            await _enrolleePaperSubmissionService.CreateInitialLinkAsync(enrolleeId, payload.UserProvidedGpid);
            return Ok();
        }

        // PUT: api/Enrollees/5/linked-gpid
        /// <summary>
        /// Updates the paper enrolment gpid that the user provided
        /// </summary>
        [HttpPut("{enrolleeId}/linked-gpid", Name = nameof(UpdateGpidLinkToPaperEnrollee))]
        [Authorize(Roles = Roles.TriageEnrollee + "," + Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> UpdateGpidLinkToPaperEnrollee(int enrolleeId, EnrolleeLinkedEnrolmentViewModel payload)
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

            await _enrolleePaperSubmissionService.UpdateLinkedGpidAsync(enrolleeId, payload.UserProvidedGpid);
            return Ok();
        }

        // GET: api/Enrollees/5/linked-gpid
        /// <summary>
        /// Gets the linked gpid
        /// </summary>
        [HttpGet("{enrolleeId}/linked-gpid", Name = nameof(GetLinkedGpid))]
        [Authorize(Roles = Roles.TriageEnrollee + "," + Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

            string gpid = await _enrolleePaperSubmissionService.GetLinkedGpidAsync(enrolleeId);
            return Ok(gpid);
        }
    }
}

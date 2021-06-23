using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
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

        public EnrolleePaperSubmissionsController(
            IEnrolleePaperSubmissionService enrolleeService
        )
        {
            _enrolleeService = enrolleeService;
        }

        // POST: api/enrollees/paper-submissions
        /// <summary>
        /// Creates a new Enrollee Paper Submission.
        /// </summary>
        [HttpPost("paper-submissions", Name = nameof(CreateEnrolleePaperSubmission))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<PaperEnrolleeDemographicViewModel>), StatusCodes.Status201Created)]
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

        // PUT: api/enrollees/5/paper-submissions/care-settings
        /// <summary>
        /// Updates a Paper Submission's Care Settings.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/care-settings", Name = nameof(UpdateEnrolleePaperSubmissionCareSettings))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionCareSettings(int enrolleeId, PaperEnrolleeCareSettingViewModel payload)
        {
            if (!await _enrolleeService.PaperSubmissionExistsAsync(enrolleeId))
            {
                return NotFound($"No Paper Submission found with Enrollee ID {enrolleeId}");
            }

            await _enrolleeService.UpdateCareSettingsAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/certifications
        /// <summary>
        /// Updates a Paper Submission's Certifications.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/certifications", Name = nameof(UpdateEnrolleePaperSubmissionCertifications))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionCertifications(int enrolleeId, PaperEnrolleeCertificationsViewModel payload)
        {
            if (!await _enrolleeService.PaperSubmissionExistsAsync(enrolleeId))
            {
                return NotFound($"No Paper Submission found with Enrollee ID {enrolleeId}");
            }

            await _enrolleeService.UpdateCertificationsAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/demographics
        /// <summary>
        /// Updates a Paper Submission's demographic information.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/demographics", Name = nameof(UpdateEnrolleePaperSubmissionDemographics))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionDemographics(int enrolleeId, PaperEnrolleeDemographicViewModel payload)
        {
            if (!await _enrolleeService.PaperSubmissionExistsAsync(enrolleeId))
            {
                return NotFound($"No Paper Submission found with Enrollee ID {enrolleeId}");
            }

            await _enrolleeService.UpdateDemographicsAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/obo-sites
        /// <summary>
        /// Updates a Paper Submission's OBO Sites.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/obo-sites", Name = nameof(UpdateEnrolleePaperSubmissionOboSites))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionOboSites(int enrolleeId, PaperEnrolleeOboSiteViewModel payload)
        {
            if (!await _enrolleeService.PaperSubmissionExistsAsync(enrolleeId))
            {
                return NotFound($"No Paper Submission found with Enrollee ID {enrolleeId}");
            }

            await _enrolleeService.UpdateOboSitesAsync(enrolleeId, payload);
            return Ok();
        }

        // PUT: api/enrollees/5/paper-submissions/self-declarations
        /// <summary>
        /// Updates a Paper Submission's Self Declaration information.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/self-declarations", Name = nameof(UpdateEnrolleePaperSubmissionSelfDeclarations))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionSelfDeclarations(int enrolleeId, PaperEnrolleeSelfDeclarationViewModel payload)
        {
            if (!await _enrolleeService.PaperSubmissionExistsAsync(enrolleeId))
            {
                return NotFound($"No Paper Submission found with Enrollee ID {enrolleeId}");
            }

            await _enrolleeService.UpdateSelfDeclarationsAsync(enrolleeId, payload);
            return Ok();
        }
    }
}

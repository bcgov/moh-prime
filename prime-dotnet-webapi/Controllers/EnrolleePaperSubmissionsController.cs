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
    // [Authorize(Roles = Roles.TriageEnrollee)]
    [AllowAnonymous]
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
                nameof(EnrolleesController),
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionCareSettings(int enrolleeId)
        {
            return Ok("Care Setting Success!");
        }

        // PUT: api/enrollees/5/paper-submissions/certifications
        /// <summary>
        /// Updates a Paper Submission's Certifications.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/certifications", Name = nameof(UpdateEnrolleePaperSubmissionCertifications))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionCertifications(int enrolleeId)
        {
            return Ok("Certification Success!");
        }

        // PUT: api/enrollees/5/paper-submissions/demographics
        /// <summary>
        /// Updates a Paper Submission's demographic information.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/demographics", Name = nameof(UpdateEnrolleePaperSubmissionDemographics))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionDemographics(int enrolleeId)
        {
            return Ok("Demographic Success!");
        }

        // PUT: api/enrollees/5/paper-submissions/obo-sites
        /// <summary>
        /// Updates a Paper Submission's OBO Sites.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/obo-sites", Name = nameof(UpdateEnrolleePaperSubmissionOboSites))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionOboSites(int enrolleeId)
        {
            return Ok("OBO Site Success!");
        }

        // PUT: api/enrollees/5/paper-submissions/self-declarations
        /// <summary>
        /// Updates a Paper Submission's Self Declaration information.
        /// </summary>
        [HttpPut("{enrolleeId}/paper-submissions/self-declarations", Name = nameof(UpdateEnrolleePaperSubmissionSelfDeclarations))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateEnrolleePaperSubmissionSelfDeclarations(int enrolleeId)
        {
            return Ok("Self Declaration Success!");
        }
    }
}

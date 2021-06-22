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
    [Route("api/enrollees/paper-submissions")]
    [ApiController]
    // [Authorize(Roles = Roles.TriageEnrollee)]
    [AllowAnonymous]
    public class EnrolleePaperSubmissionsController : PrimeControllerBase
    {

        private readonly IEnrolleeService _enrolleeService;

        public EnrolleePaperSubmissionsController(
            IEnrolleeService enrolleeService
        )
        {
            _enrolleeService = enrolleeService;
        }

        // POST: api/enrollees/paper-submissions
        /// <summary>
        /// Creates a new Enrollee Paper Submission.
        /// </summary>
        [HttpPost(Name = nameof(CreateEnrolleePaperSubmission))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<PaperEnrolleeDemographicViewModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateEnrolleePaperSubmission(PaperEnrolleeDemographicViewModel payload)
        {
            var createdEnrolleeId = await _enrolleeService.CreateEnrolleeAsync(payload);
            var enrollee = await _enrolleeService.GetEnrolleeAsync(createdEnrolleeId);

            return CreatedAtAction(
                nameof(EnrolleesController.GetEnrolleeById),
                nameof(EnrolleesController),
                new { enrolleeId = createdEnrolleeId },
                enrollee
            );
        }
    }
}

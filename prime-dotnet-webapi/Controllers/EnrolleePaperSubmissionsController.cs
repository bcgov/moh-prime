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
    [Authorize(Roles = Roles.TriageEnrollee)]
    public class EnrolleePaperSubmissionsController : PrimeControllerBase
    {


        public EnrolleePaperSubmissionsController()
        {

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
        public async Task<ActionResult> CreateEnrolleePaperSubmission(PaperEnrolleeDemographicViewModel enrollee)
        {
            return Ok("Create Success!");
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    // User needs the User role to use this controller
    [Authorize(Policy = AuthConstants.USER_POLICY)]
    public class FeedbackController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;

        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IEnrolleeService adminService, IFeedbackService feedbackService)
        {
            _enrolleeService = adminService;
            _feedbackService = feedbackService;
        }

        // POST: api/Feedback
        /// <summary>
        /// Creates user Feedback.
        /// </summary>
        [HttpPost(Name = nameof(CreateFeedback))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Feedback>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Feedback>> CreateFeedback(Feedback feedback)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(feedback.EnrolleeId);
            if (record == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {feedback.EnrolleeId}"));
            }
            if (!record.ViewableBy(User))
            {
                return Forbid();
            }

            var createFeedback = await _feedbackService.CreateFeedbackAsync(feedback);

            return Ok(ApiResponse.Result(createFeedback));
        }
    }
}

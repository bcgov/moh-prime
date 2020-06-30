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
    [Authorize(Policy = Policies.EnrolleeOnly)]
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
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResultResponse<Admin>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Feedback>> CreateFeedback(Feedback feedback)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(feedback.EnrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {feedback.EnrolleeId}"));
            }

            if (!User.CanView(enrollee))
            {
                return Forbid();
            }

            var createFeedback = await _feedbackService.CreateFeedbackAsync(feedback);

            return CreatedAtAction(
                nameof(CreateFeedback),
                new { enrolleeId = feedback.EnrolleeId },
                ApiResponse.Result(createFeedback)
            );
        }
    }
}

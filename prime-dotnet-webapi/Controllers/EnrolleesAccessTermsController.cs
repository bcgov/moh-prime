using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Models;
using Prime.Services;

namespace Prime.Controllers
{

    [Produces("application/json")]
    [Route("api/enrollees")]
    [ApiController]
    // User needs at least the ADMIN or ENROLLEE role to use this controller
    [Authorize(Policy = PrimeConstants.USER_POLICY)]
    public class EnrolleesAccessTermsController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAccessTermService _accessTermService;
        private readonly IEnrolleeProfileVersionService _enrolleeProfileVersionService;

        public EnrolleesAccessTermsController(
            IEnrolleeService enrolleeService,
            IAccessTermService accessTermService,
            IEnrolleeProfileVersionService enrolleeProfileVersionService)
        {
            _enrolleeService = enrolleeService;
            _accessTermService = accessTermService;
            _enrolleeProfileVersionService = enrolleeProfileVersionService;
        }

        // GET: api/Enrollees/access-terms
        /// <summary>
        /// Get the enrollee's Access Terms.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpGet("{enrolleeId}/access-terms", Name = nameof(GetAccessTerms))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<AccessTerm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AccessTerm>>> GetAccessTerms(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanAccess(enrollee))
            {
                return Forbid();
            }

            var accessTerms = await _accessTermService.GetAcceptedAccessTerms(enrolleeId);

            return Ok(new ApiOkResponse<IEnumerable<AccessTerm>>(accessTerms));
        }

        // GET: api/Enrollees/5/access-terms
        /// <summary>
        /// Get the enrollee's term of access.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="accessTermId"></param>
        [HttpGet("{enrolleeId}/access-terms/{accessTermId}", Name = nameof(GetAccessTerm))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<AccessTerm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<AccessTerm>> GetAccessTerm(int enrolleeId, int accessTermId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanAccess(enrollee))
            {
                return Forbid();
            }

            if (!await _accessTermService.AccessTermExistsAsync(accessTermId))
            {
                return NotFound(new ApiResponse(404, $"Access term not found with id {accessTermId}"));
            }

            // Prevent access to the enrollee's current terms of access based on status
            if (await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, Status.IN_PROGRESS_CODE, Status.DECLINED_CODE, Status.DECLINED_TOS_CODE))
            {
                this.ModelState.AddModelError("Enrollee.CurrentStatus", "Enrollee terms of service can not be retrieved when the current status is 'IN_PROGRESS', 'DECLINED', or 'DECLINED_TOA'.");
                return BadRequest(new ApiBadRequestResponse(this.ModelState));
            }

            var accessTerms = await _accessTermService.GetEnrolleesAccessTermAsync(enrolleeId, accessTermId);

            return Ok(new ApiOkResponse<AccessTerm>(accessTerms));
        }

        // GET: api/Enrollees/5/access-terms/latest?signed=true
        /// <summary>
        /// Get the enrollee's term of access.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="signed"></param>
        [HttpGet("{enrolleeId}/access-terms/latest", Name = nameof(GetAccessTermLatest))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiOkResponse<AccessTerm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<AccessTerm>> GetAccessTermLatest(int enrolleeId, [FromQuery] bool signed)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(new ApiResponse(404, $"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanAccess(enrollee))
            {
                return Forbid();
            }

            // TODO: Update when statuses are refactored!
            // Prevent access to the enrollee's current terms of access based on status
            // if (await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, Status.IN_PROGRESS_CODE, Status.DECLINED_CODE, Status.DECLINED_TOS_CODE))
            // {
            //     this.ModelState.AddModelError("Enrollee.CurrentStatus", "Enrollee terms of service can not be retrieved when the current status is 'IN_PROGRESS', 'DECLINED', or 'DECLINED_TOA'.");
            //     return BadRequest(new ApiBadRequestResponse(this.ModelState));
            // }

            AccessTerm accessTerm;
            if (signed) {
                accessTerm = await _accessTermService.GetMostRecentAcceptedEnrolleesAccessTermAsync(enrolleeId);
            } else {
                accessTerm = await _accessTermService.GetMostRecentNotAcceptedEnrolleesAccessTermAsync(enrolleeId);
            }
            return Ok(new ApiOkResponse<AccessTerm>(accessTerm));
        }
    }
}

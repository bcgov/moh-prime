using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Services;
using Prime.Models.Api;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/enrollees")]
    [ApiController]
    // User needs at least the RO_ADMIN or ENROLLEE role to use this controller
    [Authorize(Policy = AuthConstants.USER_POLICY)]
    public class EnrolleesAccessTermsController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAccessTermService _accessTermService;
        private readonly IEnrolleeProfileVersionService _enrolleeProfileVersionService;
        private readonly IRazorConverterService _razorConverterService;
        private readonly IBusinessEventService _businessEventService;

        public EnrolleesAccessTermsController(
            IEnrolleeService enrolleeService,
            IAccessTermService accessTermService,
            IEnrolleeProfileVersionService enrolleeProfileVersionService,
            IRazorConverterService razorConverterService,
            IBusinessEventService businessEventService)
        {
            _enrolleeService = enrolleeService;
            _accessTermService = accessTermService;
            _enrolleeProfileVersionService = enrolleeProfileVersionService;
            _razorConverterService = razorConverterService;
            _businessEventService = businessEventService;
        }

        // GET: api/Enrollees/5/access-terms
        /// <summary>
        /// Get a list of the enrollee's access terms.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="filters"></param>
        [HttpGet("{enrolleeId}/access-terms", Name = nameof(GetAccessTerms))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<AccessTerm>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AccessTerm>>> GetAccessTerms(int enrolleeId, [FromQuery] AccessTermFilters filters)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanView(enrollee))
            {
                return Forbid();
            }

            var accessTerms = await _accessTermService.GetAccessTermsAsync(enrolleeId, filters);

            if (User.IsAdmin())
            {
                await _businessEventService.CreateAdminViewEventAsync(enrollee.Id, "Admin viewing PRIME History");
            }

            return Ok(ApiResponse.Result(accessTerms));
        }

        // GET: api/Enrollees/5/access-terms/2
        /// <summary>
        /// Get a specific access term for an enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="accessTermId"></param>
        [HttpGet("{enrolleeId}/access-terms/{accessTermId}", Name = nameof(GetAccessTerm))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AccessTerm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<AccessTerm>> GetAccessTerm(int enrolleeId, int accessTermId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanView(enrollee))
            {
                return Forbid();
            }

            AccessTerm accessTerm = await _accessTermService.GetEnrolleeAccessTermAsync(enrolleeId, accessTermId, true);

            if (accessTerm == null)
            {
                return NotFound(ApiResponse.Message($"Access term not found with id {accessTermId} on enrollee with id {enrolleeId}"));
            }

            if (User.IsAdmin())
            {
                await _businessEventService.CreateAdminViewEventAsync(enrollee.Id, "Admin viewing Terms of Access");
            }

            return Ok(ApiResponse.Result(accessTerm));
        }

        // GET: api/Enrollees/5/access-terms/3/enrolment
        /// <summary>
        /// Get the Profile Snapshot used for the given access term.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="accessTermId"></param>
        [HttpGet("{enrolleeId}/access-terms/{accessTermId}/enrolment", Name = nameof(GetEnrolmentForAccessTerm))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeProfileVersion>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeProfileVersion>> GetEnrolmentForAccessTerm(int enrolleeId, int accessTermId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanView(enrollee))
            {
                return Forbid();
            }

            AccessTerm accessTerm = await _accessTermService.GetEnrolleeAccessTermAsync(enrolleeId, accessTermId);
            if (accessTerm == null || accessTerm.AcceptedDate == null)
            {
                return NotFound(ApiResponse.Message($"Accepted Access Term not found with id {accessTermId} for enrollee with id {enrolleeId}"));
            }

            var enrolleeProfileHistory = await _enrolleeProfileVersionService.GetEnrolleeProfileVersionBeforeDateAsync(enrolleeId, accessTerm.AcceptedDate.Value);
            if (enrolleeProfileHistory == null)
            {
                return NotFound(ApiResponse.Message($"No enrolment profile history found for Access Term with id {accessTermId} for enrollee with id {enrolleeId}."));
            }

            if (User.IsAdmin())
            {
                await _businessEventService.CreateAdminViewEventAsync(enrollee.Id, "Admin viewing Enrolment in PRIME History");
            }

            return Ok(ApiResponse.Result(enrolleeProfileHistory));
        }
    }
}

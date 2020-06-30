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
        /// <param name="year"></param>
        [HttpGet("{enrolleeId}/access-terms", Name = nameof(GetAccessTerms))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AccessTerm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AccessTerm>>> GetAccessTerms(int enrolleeId, [FromQuery] int year)
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

            var accessTerms = await _accessTermService.GetAcceptedAccessTerms(enrolleeId, year);

            if (User.IsAdmin())
            {
                await _businessEventService.CreateAdminViewEventAsync(enrollee.Id, "Admin viewing PRIME History");
            }

            return Ok(ApiResponse.Result(accessTerms));
        }

        // GET: api/Enrollees/5/access-terms/7
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

            if (!await _accessTermService.AccessTermExistsOnEnrolleeAsync(accessTermId, enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Access term not found with id {accessTermId} for enrollee id: {enrolleeId}"));
            }

            AccessTerm accessTerm = await _accessTermService.GetEnrolleesAccessTermAsync(enrolleeId, accessTermId);
            accessTerm.TermsOfAccess = await _razorConverterService.RenderViewToStringAsync("/Views/TermsOfAccess.cshtml", accessTerm);

            if (User.IsAdmin())
            {
                await _businessEventService.CreateAdminViewEventAsync(enrollee.Id, "Admin viewing Terms of Access");
            }

            return Ok(ApiResponse.Result(accessTerm));
        }

        // GET: api/Enrollees/5/access-terms/latest?signed=true
        /// <summary>
        /// Get the latest access term for an enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="signed"></param>
        [HttpGet("{enrolleeId}/access-terms/latest", Name = nameof(GetAccessTermLatest))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AccessTerm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<AccessTerm>> GetAccessTermLatest(int enrolleeId, [FromQuery] bool signed)
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

            AccessTerm accessTerm = (signed)
                ? await _accessTermService.GetMostRecentAcceptedEnrolleesAccessTermAsync(enrolleeId)
                : await _accessTermService.GetMostRecentNotAcceptedEnrolleesAccessTermAsync(enrolleeId);
            accessTerm.TermsOfAccess = await _razorConverterService.RenderViewToStringAsync("/Views/TermsOfAccess.cshtml", accessTerm);

            return Ok(ApiResponse.Result(accessTerm));
        }

        // GET: api/Enrollees/5/access-terms/3/enrolment
        /// <summary>
        /// Get the enrolment used for the given access term.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="accessTermId"></param>
        [HttpGet("{enrolleeId}/access-terms/{accessTermId}/enrolment", Name = nameof(GetEnrolmentForAccessTerm))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AccessTerm>), StatusCodes.Status200OK)]
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

            AccessTerm acceptedAccessTerm = await _accessTermService.GetEnrolleesAccessTermAsync(enrolleeId, accessTermId);
            if (acceptedAccessTerm == null)
            {
                return NotFound(ApiResponse.Message($"Accepted Access Term not found with id {accessTermId} for enrollee with id {enrolleeId}"));
            }

            var enrolleeProfileHistory = await _enrolleeProfileVersionService
                    .GetEnrolleeProfileVersionBeforeDateAsync(enrolleeId, (DateTimeOffset)acceptedAccessTerm.AcceptedDate);

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

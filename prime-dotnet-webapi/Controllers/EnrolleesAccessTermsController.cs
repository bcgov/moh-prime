using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Models;
using Prime.Services;
using Prime.Models.Api;

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
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AccessTerm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AccessTerm>>> GetAccessTerms(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanAccess(enrollee))
            {
                return Forbid();
            }

            var accessTerms = await _accessTermService.GetAcceptedAccessTerms(enrolleeId);

            return Ok(ApiResponse.Result(accessTerms));
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
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AccessTerm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<AccessTerm>> GetAccessTerm(int enrolleeId, int accessTermId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanAccess(enrollee))
            {
                return Forbid();
            }

            if (!await _accessTermService.AccessTermExistsOnEnrolleeAsync(accessTermId, enrolleeId))
            {
                return NotFound(ApiResponse.Message($"Access term not found with id {accessTermId} for enrollee id: {enrolleeId}"));
            }

            var accessTerms = await _accessTermService.GetEnrolleesAccessTermAsync(enrolleeId, accessTermId);

            return Ok(ApiResponse.Result(accessTerms));
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
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AccessTerm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<AccessTerm>> GetAccessTermLatest(int enrolleeId, [FromQuery] bool signed)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }

            if (!User.CanAccess(enrollee))
            {
                return Forbid();
            }

            AccessTerm accessTerm;

            if (signed)
            {
                accessTerm = await _accessTermService.GetMostRecentAcceptedEnrolleesAccessTermAsync(enrolleeId);
            }
            else
            {
                accessTerm = await _accessTermService.GetMostRecentNotAcceptedEnrolleesAccessTermAsync(enrolleeId);
            }
            return Ok(ApiResponse.Result(accessTerm));
        }

        // GET: api/Enrollees/5/access-terms/3/enrolment
        /// <summary>
        /// Get the enrolment history used for the given access term
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

            if (!User.CanAccess(enrollee))
            {
                return Forbid();
            }

            AccessTerm acceptedAccessTerm = await _accessTermService.GetEnrolleesAccessTermAsync(enrolleeId, accessTermId);
            if (acceptedAccessTerm == null)
            {
                return NotFound(ApiResponse.Message($"Accepted Access Term not found with id {accessTermId} for enrollee with id {enrolleeId}"));
            }

            var enrolleeProfileHistory = await _enrolleeProfileVersionService
                    .GetEnrolleeProfileVersionBeforeDateAsync(enrolleeId, (DateTime)acceptedAccessTerm.AcceptedDate);

            if (enrolleeProfileHistory == null)
            {
                return NotFound(ApiResponse.Message($"No enrolment profile history found for Access Term with id {accessTermId} for enrollee with id {enrolleeId}."));
            }

            return Ok(ApiResponse.Result(enrolleeProfileHistory));
        }
    }
}

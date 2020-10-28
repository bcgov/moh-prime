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
    public class EnrolleeAgreementsController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAgreementService _agreementService;
        private readonly IEnrolleeProfileVersionService _enrolleeProfileVersionService;
        private readonly IRazorConverterService _razorConverterService;
        private readonly IBusinessEventService _businessEventService;

        private readonly IPdfService _pdfService;

        public EnrolleeAgreementsController(
            IEnrolleeService enrolleeService,
            IAgreementService agreementService,
            IEnrolleeProfileVersionService enrolleeProfileVersionService,
            IRazorConverterService razorConverterService,
            IBusinessEventService businessEventService,
            IPdfService pdfService)
        {
            _enrolleeService = enrolleeService;
            _agreementService = agreementService;
            _enrolleeProfileVersionService = enrolleeProfileVersionService;
            _razorConverterService = razorConverterService;
            _businessEventService = businessEventService;
            _pdfService = pdfService;
        }

        // GET: api/Enrollees/5/agreements
        /// <summary>
        /// Get a list of the enrollee's agreements.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="filters"></param>
        [HttpGet("{enrolleeId}/agreements", Name = nameof(GetEnrolleeAgreements))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Agreement>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Agreement>>> GetEnrolleeAgreements(int enrolleeId, [FromQuery] AgreementFilters filters)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (!record.ViewableBy(User))
            {
                return Forbid();
            }

            var agreements = await _agreementService.GetEnrolleeAgreementsAsync(enrolleeId, filters);

            if (User.IsAdmin())
            {
                await _businessEventService.CreateAdminViewEventAsync(enrolleeId, "Admin viewing PRIME History");
            }

            return Ok(ApiResponse.Result(agreements));
        }

        // GET: api/Enrollees/5/agreements/2
        /// <summary>
        /// Get a specific agreement for an enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="agreementId"></param>
        [HttpGet("{enrolleeId}/agreements/{agreementId}", Name = nameof(GetAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Agreement>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Agreement>> GetAgreement(int enrolleeId, int agreementId)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (!record.ViewableBy(User))
            {
                return Forbid();
            }

            Agreement agreement = await _agreementService.GetEnrolleeAgreementAsync(enrolleeId, agreementId, true);
            if (agreement == null)
            {
                return NotFound(ApiResponse.Message($"Agreement not found with id {agreementId} on enrollee with id {enrolleeId}"));
            }

            if (User.IsAdmin())
            {
                await _businessEventService.CreateAdminViewEventAsync(enrolleeId, "Admin viewing Agreement");
            }

            return Ok(ApiResponse.Result(agreement));
        }

        // GET: api/Enrollees/5/agreements/3/enrolment
        /// <summary>
        /// Get the Profile Snapshot used for the given agreement.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="agreementId"></param>
        [HttpGet("{enrolleeId}/agreements/{agreementId}/enrolment", Name = nameof(GetEnrolmentForAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeProfileVersion>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeProfileVersion>> GetEnrolmentForAgreement(int enrolleeId, int agreementId)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (!record.ViewableBy(User))
            {
                return Forbid();
            }

            Agreement agreement = await _agreementService.GetEnrolleeAgreementAsync(enrolleeId, agreementId);
            if (agreement == null || agreement.AcceptedDate == null)
            {
                return NotFound(ApiResponse.Message($"Accepted Agreement not found with id {agreementId} for enrollee with id {enrolleeId}"));
            }

            var enrolleeProfileHistory = await _enrolleeProfileVersionService.GetEnrolleeProfileVersionBeforeDateAsync(enrolleeId, agreement.AcceptedDate.Value);
            if (enrolleeProfileHistory == null)
            {
                return NotFound(ApiResponse.Message($"No enrolment profile history found for Agreement with id {agreementId} for enrollee with id {enrolleeId}."));
            }

            if (User.IsAdmin())
            {
                await _businessEventService.CreateAdminViewEventAsync(enrolleeId, "Admin viewing Enrolment in PRIME History");
            }

            return Ok(ApiResponse.Result(enrolleeProfileHistory));
        }

        // GET: api/Enrollees/5/agreements/2/signable
        /// <summary>
        /// Downloads a specific unsigned access term for an enrollee.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="agreementId"></param>
        [HttpGet("{enrolleeId}/agreements/{agreementId}/signable", Name = nameof(GetAccessTermSignable))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<byte[]>), StatusCodes.Status200OK)]
        public async Task<ActionResult<byte[]>> GetAccessTermSignable(int enrolleeId, int agreementId)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (!record.ViewableBy(User))
            {
                return Forbid();
            }

            Agreement agreement = await _agreementService.GetEnrolleeAgreementAsync(enrolleeId, agreementId, true);

            if (agreement == null)
            {
                return NotFound(ApiResponse.Message($"Agreement not found with id {agreementId} on enrollee with id {enrolleeId}"));
            }

            var html = await _razorConverterService.RenderViewToStringAsync("/Views/TermsOfAccessPdf.cshtml", agreement);
            var download = _pdfService.Generate(html);

            return Ok(ApiResponse.Result(download));
        }
    }
}

using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Services;
using Prime.Models.Api;
using Prime.ViewModels;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/enrollees")]
    [ApiController]
    [Authorize(Policy = AuthConstants.USER_POLICY)]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;
        private readonly IAdminService _adminService;
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAgreementService _agreementService;
        private readonly IEnrolleeSubmissionService _enrolleeSubmissionService;
        private readonly IBusinessEventService _businessEventService;

        public SubmissionsController(
            ISubmissionService submissionService,
            IAdminService adminService,
            IEnrolleeService enrolleeService,
            IAgreementService agreementService,
            IEnrolleeSubmissionService enrolleeSubmissionService,
            IBusinessEventService businessEventService)
        {
            _submissionService = submissionService;
            _adminService = adminService;
            _enrolleeService = enrolleeService;
            _agreementService = agreementService;
            _enrolleeSubmissionService = enrolleeSubmissionService;
            _businessEventService = businessEventService;
        }

        // POST: api/enrollees/5/submission
        /// <summary>
        /// Submits the given enrollee through Auto/manual adjudication.
        /// </summary>
        [HttpPost("{enrolleeId}/submission", Name = nameof(Submit))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeViewModel>> Submit(int enrolleeId, EnrolleeUpdateModel updatedProfile)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (!record.EditableBy(User))
            {
                return Forbid();
            }
            if (updatedProfile == null)
            {
                this.ModelState.AddModelError("EnrolleeUpdateModel", "New profile cannot be null.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.Editable))
            {
                this.ModelState.AddModelError("Enrollee.CurrentStatus", "Application can not be submitted when the current status is not 'Active'.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            updatedProfile.IdentityAssuranceLevel = User.GetIdentityAssuranceLevel();
            updatedProfile.IdentityProvider = User.GetIdentityProvider();
            await _submissionService.SubmitApplicationAsync(enrolleeId, updatedProfile);

            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            return Ok(ApiResponse.Result(enrollee));
        }

        // POST: api/enrollees/5/submission/accept-toa
        /// <summary>
        /// Performs a submission-related action on an Enrolle, such as an adjudicator approving an application.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="submissionAction"></param>
        /// <param name="documentGuid"></param>
        [HttpPost("{enrolleeId}/submission/{submissionAction:submissionAction}", Name = nameof(SubmissionAction))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeViewModel>> SubmissionAction(int enrolleeId, SubmissionAction submissionAction, [FromQuery] Guid? documentGuid = null)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}"));
            }
            if (!record.EditableBy(User))
            {
                return Forbid();
            }

            var success = await _submissionService.PerformSubmissionActionAsync(enrolleeId, submissionAction, User.IsAdmin(), documentGuid);
            if (!success)
            {
                this.ModelState.AddModelError("Enrollee.CurrentStatus", "Action could not be performed.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            return Ok(ApiResponse.Result(enrollee));
        }

        // PUT: api/Enrollees/5/submission/assignment
        /// <summary>
        /// Assign a TOA agreement type to the latest submission.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="agreementType"></param>
        [HttpPut("{enrolleeId}/submissions/latest/type", Name = nameof(AssignToaAgreementType))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeViewModel>> AssignToaAgreementType(int enrolleeId, AgreementType? agreementType)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}."));
            }

            if (agreementType.HasValue && !Enum.IsDefined(typeof(AgreementType), agreementType))
            {
                return NotFound(ApiResponse.Message($"Agreement type not found with id {agreementType}."));
            }

            if (agreementType.HasValue && !Enum.GetValues(typeof(AgreementType))
                .Cast<AgreementType>()
                .Where(v =>
                    v != AgreementType.CommunityPracticeOrgAgreement &&
                    v != AgreementType.CommunityPharmacyOrgAgreement)
                .ToList()
                .Contains(agreementType.Value))
            {
                this.ModelState.AddModelError("AgreementType", "Agreement type must be a TOA.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.UnderReview))
            {
                this.ModelState.AddModelError("Enrollee.CurrentStatus", "Assigned agreement type can not be updated when the current status is 'Editable'.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            // TODO submission is current not approved already (eg. approved flag)

            await _enrolleeService.AssignToaAgreementType(enrollee.Id, agreementType);
            await _businessEventService.CreateAdminActionEventAsync(enrolleeId, "Admin assigned agreement");

            var updatedEnrollee = await _enrolleeService.GetEnrolleeAsync(enrollee.Id);

            return Ok(ApiResponse.Result(updatedEnrollee));
        }

        // PUT: api/enrollees/5/always-manual
        /// <summary>
        /// Sets an Enrollee's always manual flag, forcing them to always be sent to manual adjudication
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPut("{enrolleeId}/always-manual", Name = nameof(SetEnrolleeManualFlag))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SetEnrolleeManualFlag(int enrolleeId)
        {
            return await ManualFlagInternal(enrolleeId, true);
        }

        // DELETE: api/enrollees/5/always-manual
        /// <summary>
        /// Removes an Enrollee's always manual flag, allowing them to go through the adjudication rules engine normally.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}/always-manual", Name = nameof(RemoveEnrolleeManualFlag))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> RemoveEnrolleeManualFlag(int enrolleeId)
        {
            return await ManualFlagInternal(enrolleeId, false);
        }

        private async Task<ActionResult> ManualFlagInternal(int enrolleeId, bool alwaysManual)
        {
            var enrolleeExists = await _enrolleeService.EnrolleeExistsAsync(enrolleeId);
            if (!enrolleeExists)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}."));
            }

            // TODO business event
            await _submissionService.UpdateAlwaysManualAsync(enrolleeId, alwaysManual);

            return NoContent();
        }
    }
}

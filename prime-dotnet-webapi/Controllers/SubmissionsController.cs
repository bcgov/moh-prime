using System;
using System.Threading.Tasks;
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
    public class SubmissionsController : PrimeControllerBase
    {
        private readonly ISubmissionService _submissionService;
        private readonly IEnrolleeService _enrolleeService;
        private readonly IBusinessEventService _businessEventService;

        public SubmissionsController(
            ISubmissionService submissionService,
            IEnrolleeService enrolleeService,
            IBusinessEventService businessEventService)
        {
            _submissionService = submissionService;
            _enrolleeService = enrolleeService;
            _businessEventService = businessEventService;
        }

        // POST: api/enrollees/5/submission
        /// <summary>
        /// Submits the given enrollee through Auto/manual adjudication.
        /// </summary>
        [HttpPost("{enrolleeId}/submission", Name = nameof(Submit))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeViewModel>> Submit(int enrolleeId, EnrolleeUpdateModel updatedProfile)
        {
            if (updatedProfile == null)
            {
                ModelState.AddModelError("EnrolleeUpdateModel", "New profile cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!record.MatchesUserIdOf(User))
            {
                return Forbid();
            }

            updatedProfile.SetPropertiesFromToken(User);

            if (!updatedProfile.Validate(User))
            {
                ModelState.AddModelError("EnrolleeUpdateModel", "One or more Properties did not match the information on the card.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.Editable))
            {
                ModelState.AddModelError("Enrollee.CurrentStatus", "Application can not be submitted when the current status is not 'Active'.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            await _submissionService.SubmitApplicationAsync(enrolleeId, updatedProfile);

            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            return OkResult(enrollee);
        }

        // POST: api/enrollees/5/submission/approve
        /// <summary>
        /// Approves the current submission for an Enrolle.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/submission/approve", Name = nameof(ApproveSubmission))]
        [Authorize(Roles = Roles.ApproveEnrollee)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ApproveSubmission(int enrolleeId)
        {
            return await SubmissionActionInternal(enrolleeId, SubmissionAction.Approve);
        }

        // POST: api/enrollees/5/submission/accept-toa?documentGuid=12345-54321
        /// <summary>
        /// Accepts the current TOA for an Enrolle.
        /// Document GUID of a collaborating ID document is required for users with Identity Assurance less than three.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="documentGuid"></param>
        [HttpPost("{enrolleeId}/submission/accept-toa", Name = nameof(AcceptToa))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> AcceptToa(int enrolleeId, [FromQuery] Guid? documentGuid = null)
        {
            return await SubmissionActionInternal(enrolleeId, SubmissionAction.AcceptToa, documentGuid);
        }

        // POST: api/enrollees/5/submission/decline-toa
        /// <summary>
        /// Declines the current TOA for an Enrolle.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/submission/decline-toa", Name = nameof(DeclineToa))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeclineToa(int enrolleeId)
        {
            return await SubmissionActionInternal(enrolleeId, SubmissionAction.DeclineToa);
        }

        // POST: api/enrollees/5/submission/enable-editing
        /// <summary>
        /// Puts the Enrolle back into an editable state.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/submission/enable-editing", Name = nameof(EnableEditing))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> EnableEditing(int enrolleeId)
        {
            return await SubmissionActionInternal(enrolleeId, SubmissionAction.EnableEditing);
        }

        // POST: api/enrollees/7/submission/cancel-toa
        /// <summary>
        /// Puts the Enrolle back into Under Review from Requires TOA.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/submission/cancel-toa", Name = nameof(CancelToaAssignment))]
        [Authorize(Roles = Roles.ApproveEnrollee)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CancelToaAssignment(int enrolleeId)
        {
            return await SubmissionActionInternal(enrolleeId, SubmissionAction.CancelToaAssignment);
        }

        // POST: api/enrollees/5/submission/lock-profile
        /// <summary>
        /// Locks the Enrolle's profile.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/submission/lock-profile", Name = nameof(LockProfile))]
        [Authorize(Roles = Roles.ManageEnrollee)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> LockProfile(int enrolleeId)
        {
            return await SubmissionActionInternal(enrolleeId, SubmissionAction.LockProfile);
        }

        // POST: api/enrollees/5/submission/decline-profile
        /// <summary>
        /// Declines the Enrolle's profile, expiring their credentials and Terms of Access.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/submission/decline-profile", Name = nameof(DeclineProfile))]
        [Authorize(Roles = Roles.ManageEnrollee)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeclineProfile(int enrolleeId)
        {
            return await SubmissionActionInternal(enrolleeId, SubmissionAction.DeclineProfile);
        }

        // POST: api/enrollees/5/submission/rerun-rules
        /// <summary>
        /// Re-runs the automatic adjudication rules for an Enrollee under review.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/submission/rerun-rules", Name = nameof(RerunRules))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> RerunRules(int enrolleeId)
        {
            return await SubmissionActionInternal(enrolleeId, SubmissionAction.RerunRules);
        }

        private async Task<ActionResult> SubmissionActionInternal(int enrolleeId, SubmissionAction action, object additionalParameters = null)
        {
            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var success = await _submissionService.PerformSubmissionActionAsync(enrolleeId, action, additionalParameters);
            if (!success)
            {
                ModelState.AddModelError("Enrollee.CurrentStatus", "Action could not be performed.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            return OkResult(enrollee);
        }

        // PUT: api/Enrollees/5/submissions/latest/type
        /// <summary>
        /// Assign a TOA agreement type to the latest submission.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="agreementType"></param>
        [HttpPut("{enrolleeId}/submissions/latest/type", Name = nameof(AssignToaAgreementType))]
        [Authorize(Roles = Roles.ApproveEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeViewModel>> AssignToaAgreementType(int enrolleeId, [FromBody] AgreementType agreementType)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }

            var assignedToaType = (agreementType == 0) ? null : (AgreementType?)agreementType;

            if (assignedToaType.HasValue && !Enum.IsDefined(typeof(AgreementType), agreementType))
            {
                return NotFound($"Agreement type not found with id {agreementType}.");
            }

            if (assignedToaType.HasValue && !agreementType.IsEnrolleeAgreement())
            {
                ModelState.AddModelError("AgreementType", "Agreement type must be a TOA.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.UnderReview))
            {
                ModelState.AddModelError("Enrollee.CurrentStatus", "Assigned agreement type can not be updated when the current status is not 'Under Review'.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            await _enrolleeService.AssignToaAgreementType(enrolleeId, assignedToaType);
            await _businessEventService.CreateAdminActionEventAsync(enrolleeId, "Admin assigned agreement");

            var updatedEnrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            return OkResult(updatedEnrollee);
        }

        // PUT: api/enrollees/5/always-manual
        /// <summary>
        /// Sets an Enrollee's always manual flag, forcing them to always be sent to manual adjudication
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPut("{enrolleeId}/always-manual", Name = nameof(SetEnrolleeManualFlag))]
        [Authorize(Roles = Roles.ManageEnrollee)]
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
        [Authorize(Roles = Roles.ManageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> RemoveEnrolleeManualFlag(int enrolleeId)
        {
            return await ManualFlagInternal(enrolleeId, false);
        }

        // PUT: api/enrollees/5/submissions/latest/confirm
        /// <summary>
        /// Confirm an Enrollee's most recent submission
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPut("{enrolleeId}/submissions/latest/confirm", Name = nameof(ConfirmSubmission))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ConfirmSubmission(int enrolleeId)
        {
            await _submissionService.ConfirmSubmissionAsync(enrolleeId);
            return NoContent();
        }

        private async Task<ActionResult> ManualFlagInternal(int enrolleeId, bool alwaysManual)
        {
            var enrolleeExists = await _enrolleeService.EnrolleeExistsAsync(enrolleeId);
            if (!enrolleeExists)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}.");
            }

            // TODO business event
            await _submissionService.UpdateAlwaysManualAsync(enrolleeId, alwaysManual);

            return NoContent();
        }
    }
}

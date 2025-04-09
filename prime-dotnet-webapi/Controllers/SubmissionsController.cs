using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
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
        private readonly IEnrolleeAgreementService _enrolleeAgreementService;

        public SubmissionsController(
            ISubmissionService submissionService,
            IEnrolleeService enrolleeService,
            IBusinessEventService businessEventService,
            IEnrolleeAgreementService enrolleeAgreementService)
        {
            _submissionService = submissionService;
            _enrolleeService = enrolleeService;
            _businessEventService = businessEventService;
            _enrolleeAgreementService = enrolleeAgreementService;
        }

        // POST: api/enrollees/5/submissions
        /// <summary>
        /// Submits the given enrollee through Auto/manual adjudication.
        /// </summary>
        [HttpPost("{enrolleeId}/submissions", Name = nameof(Submit))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Submit(int enrolleeId, EnrolleeUpdateModel updatedProfile)
        {
            if (updatedProfile == null)
            {
                return BadRequest("New profile cannot be null.");
            }

            var record = await _enrolleeService.GetPermissionsRecordAsync(enrolleeId);
            if (record == null)
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }
            if (!record.MatchesUsernameOf(User))
            {
                return Forbid();
            }

            updatedProfile.SetPropertiesFromToken(User);

            if (!updatedProfile.Validate(User))
            {
                return BadRequest("One or more Properties did not match the information on the card.");
            }

            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.Editable))
            {
                return BadRequest("Application can not be submitted when the current status is not 'Active'.");
            }

            if (!await _enrolleeService.IsEnrolleeProfileCompleteAsync(enrolleeId))
            {
                return BadRequest("Enrollee profile is not completed.");
            }

            await _submissionService.SubmitApplicationAsync(enrolleeId, updatedProfile);

            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            return Ok(enrollee);
        }

        // POST: api/enrollees/5/status-actions/approve
        /// <summary>
        /// Approves the current submission for an Enrolle.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/status-actions/approve", Name = nameof(ApproveSubmission))]
        [Authorize(Roles = Roles.ApproveEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ApproveSubmission(int enrolleeId)
        {
            return await EnrolleeStatusActionInternal(enrolleeId, EnrolleeStatusAction.Approve);
        }

        // POST: api/enrollees/5/status-actions/accept-toa?documentGuid=12345-54321
        /// <summary>
        /// Accepts the current TOA for an Enrolle.
        /// Document GUID of a collaborating ID document is required for users with Identity Assurance less than three.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="documentGuid"></param>
        [HttpPost("{enrolleeId}/status-actions/accept-toa", Name = nameof(AcceptToa))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> AcceptToa(int enrolleeId, [FromQuery] Guid? documentGuid = null)
        {
            return await EnrolleeStatusActionInternal(enrolleeId, EnrolleeStatusAction.AcceptToa, documentGuid);
        }

        // POST: api/enrollees/5/status-actions/decline-toa
        /// <summary>
        /// Declines the current TOA for an Enrolle.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/status-actions/decline-toa", Name = nameof(DeclineToa))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeclineToa(int enrolleeId)
        {
            return await EnrolleeStatusActionInternal(enrolleeId, EnrolleeStatusAction.DeclineToa);
        }

        // POST: api/enrollees/5/status-actions/enable-editing
        /// <summary>
        /// Puts the Enrolle back into an editable state.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/status-actions/enable-editing", Name = nameof(EnableEditing))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> EnableEditing(int enrolleeId)
        {
            return await EnrolleeStatusActionInternal(enrolleeId, EnrolleeStatusAction.EnableEditing);
        }

        // POST: api/enrollees/7/status-actions/cancel-toa
        /// <summary>
        /// Puts the Enrolle back into Under Review from Requires TOA.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/status-actions/cancel-toa", Name = nameof(CancelToaAssignment))]
        [Authorize(Roles = Roles.ApproveEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CancelToaAssignment(int enrolleeId)
        {
            return await EnrolleeStatusActionInternal(enrolleeId, EnrolleeStatusAction.CancelToaAssignment);
        }

        // POST: api/enrollees/5/status-actions/lock-profile
        /// <summary>
        /// Locks the Enrolle's profile.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/status-actions/lock-profile", Name = nameof(LockProfile))]
        [Authorize(Roles = Roles.ManageEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> LockProfile(int enrolleeId)
        {
            return await EnrolleeStatusActionInternal(enrolleeId, EnrolleeStatusAction.LockProfile);
        }

        // POST: api/enrollees/5/status-actions/decline-profile
        /// <summary>
        /// Declines the Enrolle's profile, expiring their credentials and Terms of Access.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/status-actions/decline-profile", Name = nameof(DeclineProfile))]
        [Authorize(Roles = Roles.ManageEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeclineProfile(int enrolleeId)
        {
            return await EnrolleeStatusActionInternal(enrolleeId, EnrolleeStatusAction.DeclineProfile);
        }

        // POST: api/enrollees/5/status-actions/rerun-rules
        /// <summary>
        /// Re-runs the automatic adjudication rules for an Enrollee under review.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/status-actions/rerun-rules", Name = nameof(RerunRules))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> RerunRules(int enrolleeId)
        {
            return await EnrolleeStatusActionInternal(enrolleeId, EnrolleeStatusAction.RerunRules);
        }

        // POST: api/enrollees/5/status-actions/unlock-profile
        /// <summary>
        /// Unlocks the Enrolle's profile.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/status-actions/unlock-profile", Name = nameof(UnlockProfile))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> UnlockProfile(int enrolleeId)
        {
            return await EnrolleeStatusActionInternal(enrolleeId, EnrolleeStatusAction.UnlockedProfile);
        }

        // POST: api/enrollees/5/status-actions/return-to-editing
        /// <summary>
        /// Return the Enrollee back into an editable state if certain conditions apply
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPost("{enrolleeId}/status-actions/return-to-editing", Name = nameof(ReturnToEditing))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ReturnToEditing(int enrolleeId)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            if (enrollee.CurrentStatus.IsType(StatusType.RequiresToa) && enrollee.RequireRedoSelfDeclaration)
            {
                // Remove obsolete TOA
                await _enrolleeAgreementService.DeleteObsoleteEnrolleeAgreementAsync(enrolleeId);

                return await EnrolleeStatusActionInternal(enrolleeId, EnrolleeStatusAction.EnableEditing);
            }
            else
            {
                return BadRequest("Enrollee not in valid state to execute request.");
            }
        }

        // POST: api/enrollees/rerun-rules
        /// <summary>
        /// Re-runs the automatic adjudication rules for all applicable Enrollees.
        /// </summary>
        [HttpPost("rerun-rules", Name = nameof(RerunAutomaticAdjudicationRules))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RerunAutomaticAdjudicationRules()
        {
            await _submissionService.BulkRerunRulesAsync();
            return Ok();
        }

        // POST: api/enrollees/rerun-rules-for-naturopaths
        /// <summary>
        /// Re-runs (most) adjudication rules for applicable naturopaths
        /// </summary>
        [HttpPost("rerun-rules-for-naturopaths", Name = nameof(RerunRulesForNaturopaths))]
        [Authorize(Roles = Roles.PrimeApiServiceAccount)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RerunRulesForNaturopaths([FromQuery] bool listOnly = true)
        {
            await _submissionService.RerunRulesForNaturopathsAsync(listOnly);
            return Ok();
        }

        private async Task<ActionResult> EnrolleeStatusActionInternal(int enrolleeId, EnrolleeStatusAction action, object additionalParameters = null)
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

            var success = await _submissionService.PerformEnrolleeStatusActionAsync(enrolleeId, action, additionalParameters);
            if (!success)
            {
                return BadRequest("Action could not be performed.");
            }

            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            return Ok(enrollee);
        }

        // PUT: api/enrollees/5/submissions/latest/type
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
        public async Task<ActionResult> AssignToaAgreementType(int enrolleeId, [FromBody] AgreementType agreementType)
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
                return BadRequest("Agreement type must be a TOA.");
            }

            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.UnderReview))
            {
                return BadRequest("Assigned agreement type can not be updated when the current status is not 'Under Review'.");
            }

            await _enrolleeService.AssignToaAgreementType(enrolleeId, assignedToaType);
            await _businessEventService.CreateAdminActionEventAsync(enrolleeId, "Admin assigned agreement");

            var updatedEnrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            return Ok(updatedEnrollee);
        }

        // PUT: api/enrollees/5/status-actions/change-toa
        /// <summary>
        ///
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="data"></param>
        [HttpPut("{enrolleeId}/status-actions/change-toa", Name = nameof(ChangeToaAgreementType))]
        [Authorize(Roles = Roles.ManageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ChangeToaAgreementType(int enrolleeId, ChangeToaUpdateViewModel data)
        {
            if (!await _enrolleeService.EnrolleeExistsAsync(enrolleeId))
            {
                return NotFound($"Enrollee not found with id {enrolleeId}");
            }

            var assignedToaType = (data.AgreementType == 0) ? null : (AgreementType?)data.AgreementType;

            if (assignedToaType.HasValue && !Enum.IsDefined(typeof(AgreementType), data.AgreementType))
            {
                return NotFound($"Agreement type not found with id {data.AgreementType}.");
            }

            if (assignedToaType.HasValue && !data.AgreementType.IsEnrolleeAgreement())
            {
                return BadRequest("Agreement type must be a TOA.");
            }

            if (!await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.Editable))
            {
                return BadRequest("Assigned agreement type may be updated only when the current status is Editable.");
            }

            return await EnrolleeStatusActionInternal(enrolleeId, EnrolleeStatusAction.ChangeToa, data);
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
        [HttpPut("{enrolleeId}/submissions/latest/confirm", Name = nameof(ConfirmLatestSubmission))]
        [Authorize(Roles = Roles.TriageEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ConfirmLatestSubmission(int enrolleeId)
        {
            await _submissionService.ConfirmLatestSubmissionAsync(enrolleeId);
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

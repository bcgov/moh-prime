using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.Models.Api;


using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Infrastructure;
using Appccelerate.StateMachine.Persistence;
// using Appccelerate.StateMachine.Machine;
using Appccelerate.StateMachine.AsyncMachine;
using Appccelerate.StateMachine.Machine.Events;
using Appccelerate.StateMachine.Machine.States;
using Appccelerate.StateMachine.Machine.Transitions;
using Appccelerate.StateMachine.Syntax;
using Appccelerate.StateMachine.AsyncSyntax;
using Appccelerate.StateMachine.Extensions;

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SimpleBase;

namespace Prime.Services
{
    public class SubmissionService : BaseService, ISubmissionService
    {
        private readonly IAccessTermService _accessTermService;
        private readonly IAutomaticAdjudicationService _automaticAdjudicationService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IEmailService _emailService;
        private readonly IEnrolleeProfileVersionService _enroleeProfileVersionService;
        private readonly IPrivilegeService _privilegeService;

        public SubmissionService(ApiDbContext context, IHttpContextAccessor httpContext,
            IAccessTermService accessTermService,
            IAutomaticAdjudicationService automaticAdjudicationService,
            IBusinessEventService businessEventService,
            IEmailService emailService,
            IEnrolleeProfileVersionService enrolleeProfileVersionService,
            IPrivilegeService privilegeService)
            : base(context, httpContext)
        {
            _accessTermService = accessTermService;
            _automaticAdjudicationService = automaticAdjudicationService;
            _businessEventService = businessEventService;
            _emailService = emailService;
            _enroleeProfileVersionService = enrolleeProfileVersionService;
            _privilegeService = privilegeService;
        }

        /// <summary>
        /// Performs a submission action on an Enrollee.
        /// </summary>
        /// <exception cref="System.InvalidOperationException"> Thrown when the action is invalid on the given Enrollee due to current state or admin access </exception>
        public async Task PerformSubmissionActionAsync(int enrolleeId, SubmissionAction action, bool isAdmin)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.PhysicalAddress)
                .Include(e => e.MailingAddress)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.Status)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.EnrolmentStatusReasons)
                        .ThenInclude(esr => esr.StatusReason)
                .Include(e => e.Certifications)
                    .ThenInclude(cer => cer.College)
                .Include(e => e.Certifications)
                    .ThenInclude(l => l.License)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            var stateMachine = new SubmissionStateMachine(enrollee.CurrentStatus);
            stateMachine.Submit += async (sender, e) => { await SubmitApplicationAsync(enrollee); };
            stateMachine.Approve += async (sender, e) => { await ApproveApplicationAsync(enrollee); };
            stateMachine.AcceptToa += async (sender, e) => { await ProccessToaAsync(enrollee, true); };
            stateMachine.DeclineToa += async (sender, e) => { await ProccessToaAsync(enrollee, false); };
            stateMachine.EnableEditing += async (sender, e) => { await EnableEditingAsync(enrollee); };
            stateMachine.LockProfile += async (sender, e) => { await LockProfileAsync(enrollee); };

            await stateMachine.PerformAction(action, isAdmin);
        }

        public async Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual)
        {
            var enrollee = await _context.Enrollees
               .SingleAsync(e => e.Id == enrolleeId);

            enrollee.AlwaysManual = alwaysManual;
            await _context.SaveChangesAsync();
        }

        private async Task SubmitApplicationAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(EnrolmentStatusCode.UnderReview);

            await _enroleeProfileVersionService.CreateEnrolleeProfileVersionAsync(enrollee);

            if (await _automaticAdjudicationService.QualifiesForAutomaticAdjudication(enrollee))
            {
                var newStatus = enrollee.AddEnrolmentStatus(EnrolmentStatusType.RequiresToa);
                newStatus.AddStatusReason(StatusReason.AUTOMATIC_CODE);

                await _accessTermService.CreateEnrolleeAccessTermAsync(enrollee);

                //await _businessEventService.CreateStatusChangeEventAsync(enrolleeId, "Automatically Approved", adminId);
            }
            else
            {
                //await _businessEventService.CreateStatusChangeEventAsync(enrolleeId, "Submitted", adminId);
            }
        }
        private async Task ApproveApplicationAsync(Enrollee enrollee)
        {
            var newStatus = enrollee.AddEnrolmentStatus(EnrolmentStatusCode.Active);
            newStatus.AddStatusReason(StatusReason.MANUAL_CODE);

            await _accessTermService.CreateEnrolleeAccessTermAsync(enrollee);

            //await _businessEventService.CreateStatusChangeEventAsync(enrolleeId, "Manually Approved", adminId);
        }

        private async Task ProccessToaAsync(Enrollee enrollee, bool accept)
        {
            enrollee.AddEnrolmentStatus(EnrolmentStatusCode.Active);

            if (accept)
            {
                SetGPID(enrollee);
                await _accessTermService.AcceptCurrentAccessTermAsync(enrollee);
                await _privilegeService.AssignPrivilegesToEnrolleeAsync(enrolleeId, enrollee);
                await _businessEventService.CreateStatusChangeEventAsync(enrolleeId, "Accepted TOA", adminId);
                await UpdateEnrolleeAdjudicator(enrollee.Id);
                await _businessEventService.CreateAdminClaimEventAsync(enrolleeId, "Admin disclaimed after TOA accepted");
            }
        }

        private async Task EnableEditingAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(EnrolmentStatusCode.Active);
            // TODO
            //await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Enabled Editing", adminId);
            await _context.SaveChangesAsync();
        }

        private async Task LockProfileAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(EnrolmentStatusCode.Locked);
            // TODO
            //await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Locked", adminId);
            await _context.SaveChangesAsync();
        }

        private void SetGPID(Enrollee enrollee)
        {
            if (string.IsNullOrWhiteSpace(enrollee.GPID))
            {
                enrollee.GPID = Base85.Ascii85.Encode(Guid.NewGuid().ToByteArray());
            }
        }

        private class SubmissionStateMachine
        {
            private readonly AsyncPassiveStateMachine<EnrolleeState, SubmissionAction> _machine;

            public event EventHandler Submit;
            public event EventHandler Approve;
            public event EventHandler AcceptToa;
            public event EventHandler DeclineToa;
            public event EventHandler EnableEditing;
            public event EventHandler LockProfile;

            public SubmissionStateMachine(EnrolmentStatus initialStatus)
            {
                var stateMachineBuilder = InitBuilder();
                stateMachineBuilder.WithInitialState(FromEnrolmentStatus(initialStatus));

                _machine = stateMachineBuilder.Build().CreatePassiveStateMachine();
                _machine.TransitionDeclined += (sender, e) => { throw new InvalidOperationException(); };

                _machine.Start();
            }

            public async Task PerformAction(SubmissionAction action, bool isAdmin)
            {
                await _machine.Fire(action, isAdmin);
            }

            private enum EnrolleeState
            {
                Active,
                UnderReview,
                RequiresToa,
                Locked
            }

            private StateMachineDefinitionBuilder<EnrolleeState, SubmissionAction> InitBuilder()
            {
                var builder = new StateMachineDefinitionBuilder<EnrolleeState, SubmissionAction>();

                builder.In(EnrolleeState.Active)
                    .On(SubmissionAction.Submit).Execute(HandleSubmit)
                    .On(SubmissionAction.LockProfile).If<bool>(isAdmin => isAdmin).Execute(HandleLockProfile);

                builder.In(EnrolleeState.UnderReview)
                    .On(SubmissionAction.Approve).If<bool>(isAdmin => isAdmin).Execute(HandleApprove)
                    .On(SubmissionAction.EnableEditing).If<bool>(isAdmin => isAdmin).Execute(HandleEnableEditing)
                    .On(SubmissionAction.LockProfile).If<bool>(isAdmin => isAdmin).Execute(HandleLockProfile);

                builder.In(EnrolleeState.RequiresToa)
                    .On(SubmissionAction.AcceptToa).If<bool>(isAdmin => !isAdmin).Execute(HandleAcceptToa)
                    .On(SubmissionAction.DeclineToa).If<bool>(isAdmin => !isAdmin).Execute(HandleDeclineToa)
                    .On(SubmissionAction.EnableEditing).If<bool>(isAdmin => isAdmin).Execute(HandleEnableEditing)
                    .On(SubmissionAction.LockProfile).If<bool>(isAdmin => isAdmin).Execute(HandleLockProfile);

                builder.In(EnrolleeState.Locked)
                    .On(SubmissionAction.EnableEditing).If<bool>(isAdmin => isAdmin).Execute(HandleEnableEditing);

                return builder;
            }

            private void HandleSubmit() { Submit(this, null); }
            private void HandleApprove() { Approve(this, null); }
            private void HandleAcceptToa() { AcceptToa(this, null); }
            private void HandleDeclineToa() { DeclineToa(this, null); }
            private void HandleEnableEditing() { EnableEditing(this, null); }
            private void HandleLockProfile() { LockProfile(this, null); }

            private static EnrolleeState FromEnrolmentStatus(EnrolmentStatus status)
            {
                if (status == null)
                {
                    throw new ArgumentNullException(nameof(status));
                }

                switch (status.StatusCode)
                {
                    case (int)EnrolmentStatusCode.Active:
                        return EnrolleeState.Active;
                    case (int)EnrolmentStatusCode.UnderReview:
                        return EnrolleeState.UnderReview;
                    case (int)EnrolmentStatusCode.RequiresToa:
                        return EnrolleeState.RequiresToa;
                    case (int)EnrolmentStatusCode.Locked:
                        return EnrolleeState.Locked;
                    default:
                        throw new ArgumentException($"State machine cannot recognize status code {status.StatusCode}");
                }
            }
        }
    }
}

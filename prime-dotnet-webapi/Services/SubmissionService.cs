using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.AsyncMachine;
using SimpleBase;

using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public class SubmissionService : BaseService, ISubmissionService
    {
        private readonly IAccessTermService _accessTermService;
        private readonly IAutomaticAdjudicationService _automaticAdjudicationService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IEmailService _emailService;
        private readonly IEnrolleeService _enrolleeService;
        private readonly IEnrolleeProfileVersionService _enroleeProfileVersionService;
        private readonly IPrivilegeService _privilegeService;

        public SubmissionService(IServiceProvider provider,
            IAccessTermService accessTermService,
            IAutomaticAdjudicationService automaticAdjudicationService,
            IBusinessEventService businessEventService,
            IEmailService emailService,
            IEnrolleeService enrolleeService,
            IEnrolleeProfileVersionService enrolleeProfileVersionService,
            IPrivilegeService privilegeService)
            : base(provider)
        {
            _accessTermService = accessTermService;
            _automaticAdjudicationService = automaticAdjudicationService;
            _businessEventService = businessEventService;
            _emailService = emailService;
            _enrolleeService = enrolleeService;
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

            var stateMachine = new SubmissionStateMachine(enrollee, this);

            await stateMachine.PerformAction(action, isAdmin);
        }

        public async Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual)
        {
            var enrollee = await _context.Enrollees
               .SingleAsync(e => e.Id == enrolleeId);

            enrollee.AlwaysManual = alwaysManual;
            await _context.SaveChangesAsync();
        }

        private async Task SubmitApplication(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(EnrolmentStatusType.UnderReview);

            await _enroleeProfileVersionService.CreateEnrolleeProfileVersionAsync(enrollee);

            if (await _automaticAdjudicationService.QualifiesForAutomaticAdjudication(enrollee))
            {
                var newStatus = enrollee.AddEnrolmentStatus(EnrolmentStatusType.RequiresToa);
                newStatus.AddStatusReason(StatusReason.AUTOMATIC_CODE);

                await _accessTermService.CreateEnrolleeAccessTermAsync(enrollee);

                await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Automatically Approved");
            }
            else
            {
                await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Submitted");
            }

            await _context.SaveChangesAsync();
        }

        private async Task ApproveApplicationAsync(Enrollee enrollee)
        {
            var newStatus = enrollee.AddEnrolmentStatus(EnrolmentStatusType.Active);
            newStatus.AddStatusReason(StatusReason.MANUAL_CODE);

            await _accessTermService.CreateEnrolleeAccessTermAsync(enrollee);

            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Manually Approved");
            await _context.SaveChangesAsync();
            await _emailService.SendReminderEmailAsync(enrollee);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, "Email to Enrollee after leaving manual adjudication");
        }

        private async Task ProccessToaAsync(Enrollee enrollee, bool accept)
        {
            enrollee.AddEnrolmentStatus(EnrolmentStatusType.Active);

            if (accept)
            {
                SetGPID(enrollee);
                await _accessTermService.AcceptCurrentAccessTermAsync(enrollee);
                await _privilegeService.AssignPrivilegesToEnrolleeAsync(enrollee.Id, enrollee);
                await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Accepted TOA");
                await _enrolleeService.UpdateEnrolleeAdjudicator(enrollee.Id);
                await _businessEventService.CreateAdminClaimEventAsync(enrollee.Id, "Admin disclaimed after TOA accepted");
            }
            await _context.SaveChangesAsync();

        }

        private async Task EnableEditingAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(EnrolmentStatusType.Active);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Enabled Editing");
            await _context.SaveChangesAsync();
            await _emailService.SendReminderEmailAsync(enrollee);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, "Email to Enrollee after leaving manual adjudication");
        }

        private async Task LockProfileAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(EnrolmentStatusType.Locked);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Locked");
            await _context.SaveChangesAsync();
            await _emailService.SendReminderEmailAsync(enrollee);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, "Email to Enrollee after leaving manual adjudication");
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
            private readonly Enrollee _enrollee;
            private readonly AsyncPassiveStateMachine<EnrolleeState, SubmissionAction> _machine;
            private readonly SubmissionService _submissionService;

            private async Task HandleSubmit() { await _submissionService.SubmitApplication(_enrollee); }
            private async Task HandleApprove() { await _submissionService.ApproveApplicationAsync(_enrollee); }
            private async Task HandleAcceptToa() { await _submissionService.ProccessToaAsync(_enrollee, true); }
            private async Task HandleDeclineToa() { await _submissionService.ProccessToaAsync(_enrollee, false); }
            private async Task HandleEnableEditing() { await _submissionService.EnableEditingAsync(_enrollee); }
            private async Task HandleLockProfile() { await _submissionService.LockProfileAsync(_enrollee); }

            public SubmissionStateMachine(Enrollee enrollee, SubmissionService submissionService)
            {
                _enrollee = enrollee;
                _submissionService = submissionService;

                var stateMachineBuilder = InitBuilder();
                stateMachineBuilder.WithInitialState(FromEnrollee(enrollee));

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

            private static EnrolleeState FromEnrollee(Enrollee enrollee)
            {
                if (enrollee == null || enrollee.CurrentStatus == null)
                {
                    throw new ArgumentNullException(nameof(enrollee));
                }

                switch (enrollee.CurrentStatus.StatusCode)
                {
                    case (int)EnrolmentStatusType.Active:
                        return EnrolleeState.Active;
                    case (int)EnrolmentStatusType.UnderReview:
                        return EnrolleeState.UnderReview;
                    case (int)EnrolmentStatusType.RequiresToa:
                        return EnrolleeState.RequiresToa;
                    case (int)EnrolmentStatusType.Locked:
                        return EnrolleeState.Locked;
                    default:
                        throw new ArgumentException($"State machine cannot recognize status code {enrollee.CurrentStatus.StatusCode}");
                }
            }
        }
    }
}

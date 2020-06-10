using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.AsyncMachine;

using Prime.Models;
using Prime.ViewModels;
using Prime.Models.Api;

namespace Prime.Services
{
    public class SubmissionService : BaseService, ISubmissionService
    {
        private readonly IAccessTermService _accessTermService;
        private readonly ISubmissionRulesService _submissionRulesService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IEmailService _emailService;
        private readonly IEnrolleeService _enrolleeService;
        private readonly IEnrolleeProfileVersionService _enroleeProfileVersionService;
        private readonly IPrivilegeService _privilegeService;

        public SubmissionService(ApiDbContext context, IHttpContextAccessor httpContext,
            IAccessTermService accessTermService,
            ISubmissionRulesService submissionRulesService,
            IBusinessEventService businessEventService,
            IEmailService emailService,
            IEnrolleeService enrolleeService,
            IEnrolleeProfileVersionService enrolleeProfileVersionService,
            IPrivilegeService privilegeService)
            : base(context, httpContext)
        {
            _accessTermService = accessTermService;
            _submissionRulesService = submissionRulesService;
            _businessEventService = businessEventService;
            _emailService = emailService;
            _enrolleeService = enrolleeService;
            _enroleeProfileVersionService = enrolleeProfileVersionService;
            _privilegeService = privilegeService;
        }

        public async Task SubmitApplicationAsync(int enrolleeId, EnrolleeProfileViewModel updatedProfile)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.MailingAddress)
                .Include(e => e.Certifications)
                .Include(e => e.Jobs)
                .Include(e => e.EnrolleeOrganizationTypes)
                .Include(e => e.AccessTerms)
                    .ThenInclude(at => at.UserClause)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            bool minorUpdate = await _submissionRulesService.QualifiesAsMinorUpdateAsync(enrollee, updatedProfile);
            await _enrolleeService.UpdateEnrolleeAsync(enrolleeId, updatedProfile);

            if (minorUpdate)
            {
                return;
            }

            enrollee.AddEnrolmentStatus(StatusType.UnderReview);
            await _enroleeProfileVersionService.CreateEnrolleeProfileVersionAsync(enrollee);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Submitted");

            await this.ProcessEnrolleeApplicationRules(enrolleeId);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Performs a submission action on an Enrollee.
        /// </summary>
        /// <exception cref="Prime.Services.SubmissionService.InvalidActionException"> Thrown when the action is invalid on the given Enrollee due to current state or admin access </exception>
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

        private async Task ApproveApplicationAsync(Enrollee enrollee)
        {
            var newStatus = enrollee.AddEnrolmentStatus(StatusType.RequiresToa);
            newStatus.AddStatusReason(StatusReasonType.Manual);

            await _accessTermService.CreateEnrolleeAccessTermAsync(enrollee);

            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Manually Approved");
            await _context.SaveChangesAsync();
            await _emailService.SendReminderEmailAsync(enrollee);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, "Notified Enrollee");
        }

        private async Task ProcessToaAsync(Enrollee enrollee, bool accept)
        {
            enrollee.AddEnrolmentStatus(StatusType.Editable);

            if (accept)
            {
                await SetGpid(enrollee);
                await _accessTermService.AcceptCurrentAccessTermAsync(enrollee);
                await _privilegeService.AssignPrivilegesToEnrolleeAsync(enrollee.Id, enrollee);
                await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Accepted TOA");

                if (enrollee.AdjudicatorId != null)
                {
                    await _enrolleeService.UpdateEnrolleeAdjudicator(enrollee.Id);
                    await _businessEventService.CreateAdminClaimEventAsync(enrollee.Id, "Admin disclaimed after TOA accepted");
                }
            }
            else
            {
                await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Declined TOA");
            }
            await _context.SaveChangesAsync();
        }

        private async Task EnableEditingAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(StatusType.Editable);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Enabled Editing");
            await _context.SaveChangesAsync();
            await _emailService.SendReminderEmailAsync(enrollee);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, "Notified Enrollee");
        }

        private async Task LockProfileAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(StatusType.Locked);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Locked");
            await _context.SaveChangesAsync();
            await _emailService.SendReminderEmailAsync(enrollee);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, "Notified Enrollee");
        }

        private async Task DeclineProfileAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(StatusType.Declined);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Declined");
            await _context.SaveChangesAsync();
            await _accessTermService.ExpireCurrentAccessTermAsync(enrollee);
        }

        private async Task EnableProfileAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(StatusType.Editable);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Enabled");
            await _context.SaveChangesAsync();
            await _emailService.SendReminderEmailAsync(enrollee);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, "Email to Enrollee after leaving the declined status");
        }

        private async Task RerunRulesAsync(Enrollee enrollee)
        {
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Adjudicator manually ran the enrollee application rules");
            await this.ProcessEnrolleeApplicationRules(enrollee.Id);
            await _context.SaveChangesAsync();
        }

        private async Task SetGpid(Enrollee enrollee)
        {
            if (string.IsNullOrWhiteSpace(enrollee.GPID))
            {
                do
                {
                    enrollee.GPID = GenerateGpid();
                }
                while (await _enrolleeService.EnrolleeGpidExistsAsync(enrollee.GPID));
            }
        }

        private static string GenerateGpid()
        {
            Random r = new Random();
            int length = 20;
            string characterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.,?!@#$%*";

            IEnumerable<char> chars = Enumerable.Repeat(characterSet, length).Select(s => s[r.Next(s.Length)]);

            return new string(chars.ToArray());
        }

        private async Task ProcessEnrolleeApplicationRules(int enrolleeId)
        {
            // TODO: UpdateEnrollee re-fetches the model, removing the includes we need for the adjudication rules. Fix how this model loading is done.
            var enrollee = await _context.Enrollees
                .Include(e => e.PhysicalAddress)
                .Include(e => e.MailingAddress)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.EnrolmentStatusReasons)
                .Include(e => e.Certifications)
                    .ThenInclude(cer => cer.College)
                .Include(e => e.Certifications)
                    .ThenInclude(c => c.License)
                        .ThenInclude(l => l.DefaultPrivileges)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (await _submissionRulesService.QualifiesForAutomaticAdjudicationAsync(enrollee))
            {
                var newStatus = enrollee.AddEnrolmentStatus(StatusType.RequiresToa);
                newStatus.AddStatusReason(StatusReasonType.Automatic);

                await _accessTermService.CreateEnrolleeAccessTermAsync(enrollee);
                await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Automatically Approved");
            }
        }

        public class InvalidActionException : Exception
        {
            public InvalidActionException() : base() { }
            public InvalidActionException(string message) : base(message) { }
            public InvalidActionException(string message, Exception inner) : base(message, inner) { }
        }

        private class SubmissionStateMachine
        {
            private readonly Enrollee _enrollee;
            private readonly AsyncPassiveStateMachine<EnrolleeState, SubmissionAction> _machine;
            private readonly SubmissionService _submissionService;

            private async Task HandleApprove() { await _submissionService.ApproveApplicationAsync(_enrollee); }
            private async Task HandleAcceptToa() { await _submissionService.ProcessToaAsync(_enrollee, true); }
            private async Task HandleDeclineToa() { await _submissionService.ProcessToaAsync(_enrollee, false); }
            private async Task HandleEnableEditing() { await _submissionService.EnableEditingAsync(_enrollee); }
            private async Task HandleLockProfile() { await _submissionService.LockProfileAsync(_enrollee); }
            private async Task HandleDeclineProfile() { await _submissionService.DeclineProfileAsync(_enrollee); }
            private async Task HandleEnableProfile() { await _submissionService.EnableProfileAsync(_enrollee); }
            private async Task HandleRerunRules() { await _submissionService.EnableProfileAsync(_enrollee); }

            public SubmissionStateMachine(Enrollee enrollee, SubmissionService submissionService)
            {
                _enrollee = enrollee;
                _submissionService = submissionService;

                var stateMachineBuilder = InitBuilder();
                stateMachineBuilder.WithInitialState(FromEnrollee(enrollee));

                _machine = stateMachineBuilder.Build().CreatePassiveStateMachine();
                _machine.TransitionDeclined += (sender, e) => { throw new InvalidActionException(); };

                _machine.Start();
            }

            public async Task PerformAction(SubmissionAction action, bool isAdmin)
            {
                await _machine.Fire(action, isAdmin);
            }

            private enum EnrolleeState
            {
                Editable,
                UnderReview,
                RequiresToa,
                Locked,
                Declined,
            }

            private StateMachineDefinitionBuilder<EnrolleeState, SubmissionAction> InitBuilder()
            {
                var builder = new StateMachineDefinitionBuilder<EnrolleeState, SubmissionAction>();

                builder.In(EnrolleeState.Editable)
                    .On(SubmissionAction.LockProfile).If<bool>(isAdmin => isAdmin).Execute(HandleLockProfile)
                    .On(SubmissionAction.DeclineProfile).If<bool>(isAdmin => isAdmin).Execute(HandleDeclineProfile);

                builder.In(EnrolleeState.UnderReview)
                    .On(SubmissionAction.Approve).If<bool>(isAdmin => isAdmin).Execute(HandleApprove)
                    .On(SubmissionAction.EnableEditing).If<bool>(isAdmin => isAdmin).Execute(HandleEnableEditing)
                    .On(SubmissionAction.LockProfile).If<bool>(isAdmin => isAdmin).Execute(HandleLockProfile)
                    .On(SubmissionAction.DeclineProfile).If<bool>(isAdmin => isAdmin).Execute(HandleDeclineProfile)
                    .On(SubmissionAction.RerunRules).If<bool>(isAdmin => isAdmin).Execute(HandleRerunRules);

                builder.In(EnrolleeState.RequiresToa)
                    .On(SubmissionAction.AcceptToa).If<bool>(isAdmin => !isAdmin).Execute(HandleAcceptToa)
                    .On(SubmissionAction.DeclineToa).If<bool>(isAdmin => !isAdmin).Execute(HandleDeclineToa)
                    .On(SubmissionAction.EnableEditing).If<bool>(isAdmin => isAdmin).Execute(HandleEnableEditing)
                    .On(SubmissionAction.LockProfile).If<bool>(isAdmin => isAdmin).Execute(HandleLockProfile)
                    .On(SubmissionAction.DeclineProfile).If<bool>(isAdmin => isAdmin).Execute(HandleDeclineProfile);

                builder.In(EnrolleeState.Locked)
                    .On(SubmissionAction.EnableEditing).If<bool>(isAdmin => isAdmin).Execute(HandleEnableEditing)
                    .On(SubmissionAction.DeclineProfile).If<bool>(isAdmin => isAdmin).Execute(HandleDeclineProfile);

                builder.In(EnrolleeState.Declined)
                    .On(SubmissionAction.EnableProfile).If<bool>(isAdmin => isAdmin).Execute(HandleEnableEditing);

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
                    case (int)StatusType.Editable:
                        return EnrolleeState.Editable;
                    case (int)StatusType.UnderReview:
                        return EnrolleeState.UnderReview;
                    case (int)StatusType.RequiresToa:
                        return EnrolleeState.RequiresToa;
                    case (int)StatusType.Locked:
                        return EnrolleeState.Locked;
                    case (int)StatusType.Declined:
                        return EnrolleeState.Declined;
                    default:
                        throw new ArgumentException($"State machine cannot recognize status code {enrollee.CurrentStatus.StatusCode}");
                }
            }
        }
    }
}

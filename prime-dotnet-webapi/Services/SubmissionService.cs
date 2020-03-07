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
using Appccelerate.StateMachine.Machine;
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


namespace Prime.Services
{
    public class SubmissionService : BaseService, ISubmissionService
    {
        public SubmissionService(ApiDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        {
        }

        public async Task PerformSubmissionActionAsync(int enrolleeId, SubmissionAction action, bool isAdmin)
        {
            var enrollee = await GetEnrollee(enrolleeId);

            var stateMachine = new SubmissionStateMachine(enrollee);
            stateMachine.PerformAction(action, isAdmin);
        }

        public async Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual)
        {
            var enrollee = await _context.Enrollees
               .SingleAsync(e => e.Id == enrolleeId);

            enrollee.AlwaysManual = alwaysManual;
            await _context.SaveChangesAsync();
        }

        private async Task<Enrollee> GetEnrollee(int enrolleeId)
        {
            return await _context.Enrollees
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
        }

        private class SubmissionStateMachine
        {
            private enum States
            {
                Active,
                UnderReview,
                RequiresToa,
                Locked
            }

            private readonly Enrollee _enrollee;
            private readonly PassiveStateMachine<States, SubmissionAction> _machine;

            public SubmissionStateMachine(Enrollee enrollee)
            {
                if (enrollee == null)
                {
                    throw new ArgumentNullException(nameof(enrollee));
                }

                _enrollee = enrollee;

                var builder = InitBuilder();
                builder.WithInitialState(FromEnrollee(enrollee));

                _machine = builder.Build().CreatePassiveStateMachine();
                _machine.TransitionDeclined += (sender, e) => { throw new InvalidOperationException(); };

                _machine.Start();
            }

            public void PerformAction(SubmissionAction action, bool isAdmin)
            {
                _machine.Fire(action, isAdmin);
            }

            private StateMachineDefinitionBuilder<States, SubmissionAction> InitBuilder()
            {
                var builder = new StateMachineDefinitionBuilder<States, SubmissionAction>();

                builder.In(States.Off)
                    .On(Events.TurnOn).Goto(States.On).Execute(SayHello)
                    .On(Events.TurnSuperOn).If<bool>(admin => admin).Goto(States.SuperOn).Execute(SaySUPERON);

                builder.In(States.On)
                    .On(Events.TurnOff).Goto(States.Off).Execute(SayBye);

                return builder;
            }

            private States FromEnrollee(Enrollee enrollee)
            {
                if (enrollee == null || enrollee.CurrentStatus == null)
                {
                    throw new ArgumentNullException(nameof(enrollee));
                }

                switch (enrollee.CurrentStatus.StatusCode)
                {
                    case Status.ACTIVE_CODE:
                        return States.Active;
                    case Status.UNDER_REVIEW_CODE:
                        return States.UnderReview;
                    case Status.REQUIRES_TOA_CODE:
                        return States.RequiresToa;
                    case Status.LOCKED_CODE:
                        return States.Locked;
                    default:
                        throw new ArgumentException($"State machine cannot recognize enrollee status {enrollee.CurrentStatus.StatusCode}");
                }
            }
        }
    }
}

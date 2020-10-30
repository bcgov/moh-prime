using System;
using System.Threading.Tasks;
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Syntax;
using Appccelerate.StateMachine.Machine;

using Prime.Models;
using Prime.Models.Api;
using Prime.Engines.Internal;

namespace Prime.Engines
{
    public class SubmissionStateEngine
    {
        private bool _actionAllowed;

        public bool AllowableAction(SubmissionAction action, Enrollee enrollee, bool asAdmin)
        {
            _actionAllowed = false;

            var machine = InitBuilder()
                .WithInitialState(FromEnrollee(enrollee))
                .Build()
                .CreatePassiveStateMachine();
            machine.Start();
            machine.Fire(action, asAdmin);

            return _actionAllowed;
        }

        private StateMachineDefinitionBuilder<EnrolleeState, SubmissionAction> InitBuilder()
        {
            var builder = new StateMachineDefinitionBuilder<EnrolleeState, SubmissionAction>();

            builder.In(EnrolleeState.Editable)
                .On(SubmissionAction.LockProfile).IfAdmin(AllowAction)
                .On(SubmissionAction.DeclineProfile).IfAdmin(AllowAction);

            builder.In(EnrolleeState.UnderReview)
                .On(SubmissionAction.Approve).IfAdmin(AllowAction)
                .On(SubmissionAction.EnableEditing).IfAdmin(AllowAction)
                .On(SubmissionAction.LockProfile).IfAdmin(AllowAction)
                .On(SubmissionAction.DeclineProfile).IfAdmin(AllowAction)
                .On(SubmissionAction.RerunRules).IfAdmin(AllowAction);

            builder.In(EnrolleeState.RequiresToa)
                .On(SubmissionAction.AcceptToa).IfEnrollee(AllowAction)
                .On(SubmissionAction.DeclineToa).IfEnrollee(AllowAction)
                .On(SubmissionAction.EnableEditing).IfAdmin(AllowAction)
                .On(SubmissionAction.LockProfile).IfAdmin(AllowAction)
                .On(SubmissionAction.DeclineProfile).IfAdmin(AllowAction);

            builder.In(EnrolleeState.Locked)
                .On(SubmissionAction.EnableEditing).IfAdmin(AllowAction)
                .On(SubmissionAction.DeclineProfile).IfAdmin(AllowAction);

            builder.In(EnrolleeState.Declined)
                .On(SubmissionAction.EnableEditing).IfAdmin(AllowAction);

            return builder;
        }

        private static EnrolleeState FromEnrollee(Enrollee enrollee)
        {
            enrollee.ThrowIfNull(nameof(enrollee));
            if (enrollee.CurrentStatus == null)
            {
                throw new ArgumentException("Enrollee must have a CurrentStatus", nameof(enrollee));
            }

            switch (enrollee.CurrentStatus.GetStatusType())
            {
                case StatusType.Editable:
                    return EnrolleeState.Editable;
                case StatusType.UnderReview:
                    return EnrolleeState.UnderReview;
                case StatusType.RequiresToa:
                    return EnrolleeState.RequiresToa;
                case StatusType.Locked:
                    return EnrolleeState.Locked;
                case StatusType.Declined:
                    return EnrolleeState.Declined;
                default:
                    throw new ArgumentException($"State machine cannot recognize status code {enrollee.CurrentStatus.StatusCode}");
            }
        }

        private void AllowAction()
        {
            _actionAllowed = true;
        }
    }
}

namespace Prime.Engines.Internal
{
    public enum EnrolleeState
    {
        Editable,
        UnderReview,
        RequiresToa,
        Locked,
        Declined,
    }

    public static class StateMachineDefinitionExtensions
    {
        public static IIfOrOtherwiseSyntax<EnrolleeState, SubmissionAction> IfAdmin(this IOnSyntax<EnrolleeState, SubmissionAction> onAction, Action result)
        {
            return onAction.If<bool>(asAdmin => asAdmin).Execute(result);
        }

        public static IIfOrOtherwiseSyntax<EnrolleeState, SubmissionAction> IfEnrollee(this IOnSyntax<EnrolleeState, SubmissionAction> onAction, Action result)
        {
            return onAction.If<bool>(asAdmin => !asAdmin).Execute(result);
        }
    }
}

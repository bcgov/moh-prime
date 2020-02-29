using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Prime.Infrastructure;

namespace Prime.Models.Api
{
    [ModelBinder(BinderType = typeof(EnumEntityBinder<SubmissionAction>))]
    public enum SubmissionAction
    {
        Submit,
        Approve,
        Decline,
        [EnumMember(Value = "accept-toa")]
        AcceptToa,
        [EnumMember(Value = "decline-toa")]
        DeclineToa,
        [EnumMember(Value = "enable-editing")]
        EnableEditing,
        [EnumMember(Value = "lock-profile")]
        LockProfile,
    }
}

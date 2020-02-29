using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Prime.Infrastructure;

namespace Prime.Models.Api
{
    [ModelBinder(BinderType = typeof(EnumEntityBinder<SubmissionAction>))]
    public enum SubmissionAction
    {
        Test,
        [EnumMember(Value = "decline-toa")]
        DeclineToa
    }
}

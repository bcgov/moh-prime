using System.ComponentModel;
using System.Runtime.Serialization;
using Prime.Infrastructure;

namespace Prime.Models.Api
{
    [TypeConverter(typeof(EnumTypeConverter<SubmissionAction>))]
    public enum SubmissionAction
    {
        Approve,
        [EnumMember(Value = "accept-toa")]
        AcceptToa,
        [EnumMember(Value = "decline-toa")]
        DeclineToa,
        [EnumMember(Value = "enable-editing")]
        EnableEditing,
        [EnumMember(Value = "lock-profile")]
        LockProfile,
        [EnumMember(Value = "decline-profile")]
        DeclineProfile,
        [EnumMember(Value = "enable-profile")]
        EnableProfile,

    }
}

namespace Prime.Models
{
    public enum StatusReasonType
    {
        Automatic = 1,
        Manual = 2,
        PharmanetError = 3,
        NotInPharmanet = 4,
        NameDiscrepancy = 5,
        BirthdateDiscrepancy = 6,
        Practicing = 7,
        PumpProvider = 8,
        LicenceClass = 9,
        SelfDeclaration = 10,
        Address = 11,
        AlwaysManual = 12,
        AssuranceLevel = 13,
        IdentityProvider = 14,
        RequestingRemoteAccess = 15,
        NoAssignedAgreement = 16,
        NoVerifiedAddress = 17,
        PaperEnrollee = 18,
        PaperEnrolmentMismatch = 19,
        PossiblePaperEnrolmentMatch = 20,
        UnableToLinkToPpaperEnrolment = 21
    }
}

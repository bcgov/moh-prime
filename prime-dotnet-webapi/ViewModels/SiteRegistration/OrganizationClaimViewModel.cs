using FluentValidation;

namespace Prime.ViewModels
{
    public class OrganizationClaimViewModel
    {
        public int PartyId { get; set; }
        public string PEC { get; set; }
        public string ClaimDetail { get; set; }
    }

    public class OrganizationClaimValidator : AbstractValidator<OrganizationClaimViewModel>
    {
        public OrganizationClaimValidator()
        {
            RuleFor(x => x.PartyId).NotEmpty();
            RuleFor(x => x.PEC).NotEmpty();
            RuleFor(x => x.ClaimDetail).NotEmpty();
        }
    }
}

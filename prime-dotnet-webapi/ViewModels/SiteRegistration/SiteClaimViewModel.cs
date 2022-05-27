using FluentValidation;

namespace Prime.ViewModels
{
    public class SiteClaimViewModel
    {
        public string PEC { get; set; }
        public string ClaimDetail { get; set; }
    }

    public class SiteClaimValidator : AbstractValidator<SiteClaimViewModel>
    {
        public SiteClaimValidator()
        {
            RuleFor(x => x.PEC).NotEmpty();
            RuleFor(x => x.ClaimDetail).NotEmpty();
        }
    }
}

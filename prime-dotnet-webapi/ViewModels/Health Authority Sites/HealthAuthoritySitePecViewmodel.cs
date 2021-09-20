using FluentValidation;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySitePecViewModel
    {
        public string Pec { get; set; }
    }

    public class HealthAuthorityPecTypeValidator : AbstractValidator<HealthAuthoritySitePecViewModel>
    {
        public HealthAuthorityPecTypeValidator()
        {
            RuleFor(x => x.Pec).NotEmpty();
        }
    }
}

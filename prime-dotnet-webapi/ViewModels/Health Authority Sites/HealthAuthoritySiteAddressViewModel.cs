using FluentValidation;
using Prime.Models;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteAddressViewModel
    {
        public PhysicalAddress PhysicalAddress { get; set; }
    }

    public class HealthAuthoritySiteAddressValidator : AbstractValidator<HealthAuthoritySiteAddressViewModel>
    {
        public HealthAuthoritySiteAddressValidator()
        {
            RuleFor(x => x.PhysicalAddress).NotNull();
        }
    }
}

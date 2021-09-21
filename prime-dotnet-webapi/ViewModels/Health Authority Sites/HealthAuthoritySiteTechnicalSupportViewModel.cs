using System;
using FluentValidation;
using Prime.Models.HealthAuthorities;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteTechnicalSupportViewModel
    {
        public int HealthAuthorityTechnicalSupportId { get; set; }
    }

    public class HealthAuthoritySiteTechnicalSupportValidator : AbstractValidator<HealthAuthoritySiteTechnicalSupportViewModel>
    {
        public HealthAuthoritySiteTechnicalSupportValidator()
        {
            RuleFor(x => x.HealthAuthorityTechnicalSupportId).NotNull();
        }
    }
}

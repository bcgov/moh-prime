using System;
using FluentValidation;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteCareTypeViewModel
    {
        public string CareType { get; set; }
    }

    public class HealthAuthoritySiteCareTypeValidator : AbstractValidator<HealthAuthoritySiteCareTypeViewModel>
    {
        public HealthAuthoritySiteCareTypeValidator()
        {
            RuleFor(x => x.CareType).NotNull();
        }
    }
}

using System;
using FluentValidation;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteCareTypeViewModel
    {
        // public int HealthAuthorityCareTypeId { get; set; }
        public string CareType { get; set; }
    }

    public class HealthAuthoritySiteCareTypeValidator : AbstractValidator<HealthAuthoritySiteCareTypeViewModel>
    {
        public HealthAuthoritySiteCareTypeValidator()
        {
            // RuleFor(x => x.HealthAuthorityCareTypeId).NotNull();
            RuleFor(x => x.CareType).NotNull();
        }
    }
}
